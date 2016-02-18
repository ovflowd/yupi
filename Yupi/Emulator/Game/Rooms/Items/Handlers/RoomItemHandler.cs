using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Yupi.Emulator.Core.Io;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items;
using Yupi.Emulator.Game.Items.Datas;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Pathfinding.Vectors;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.Rooms.Chat.Enums;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Rooms.User.Path;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Rooms.Items.Handlers
{
    /// <summary>
    ///     Class RoomItemHandler.
    /// </summary>
    internal class RoomItemHandler
    {
        /// <summary>
        ///     The _roller items moved
        /// </summary>
        private readonly List<uint> _rollerItemsMoved, _rollerUsersMoved;

        /// <summary>
        ///     The _roller messages
        /// </summary>
        private readonly List<SimpleServerMessageBuffer> _rollerMessages;

        /// <summary>
        ///     The _roller speed
        /// </summary>
        private uint _rollerSpeed, _roolerCycle;

        /// <summary>
        ///     The _room
        /// </summary>
        private Room _room;

        /// <summary>
        ///     The _room item update queue
        /// </summary>
        private Queue _roomItemUpdateQueue;

        private List<uint> _updatedItems, _removedItems;

        /// <summary>
        ///     The breeding terrier
        /// </summary>
        internal Dictionary<uint, RoomItem> BreedingTerrier, BreedingBear;

        /// <summary>
        ///     The floor items
        /// </summary>
        internal ConcurrentDictionary<uint, RoomItem> FloorItems, WallItems, Rollers;

        /// <summary>
        ///     The hopper count
        /// </summary>
        public int HopperCount;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomItemHandler" /> class.
        /// </summary>
        /// <param name="room">The room.</param>
        public RoomItemHandler(Room room)
        {
            _room = room;
            _removedItems = new List<uint>();
            _updatedItems = new List<uint>();
            Rollers = new ConcurrentDictionary<uint, RoomItem>();
            WallItems = new ConcurrentDictionary<uint, RoomItem>();
            FloorItems = new ConcurrentDictionary<uint, RoomItem>();
            _roomItemUpdateQueue = new Queue();
            BreedingBear = new Dictionary<uint, RoomItem>();
            BreedingTerrier = new Dictionary<uint, RoomItem>();
            GotRollers = false;
            _roolerCycle = 0;
            _rollerSpeed = 4;
            HopperCount = 0;
            _rollerItemsMoved = new List<uint>();
            _rollerUsersMoved = new List<uint>();
            _rollerMessages = new List<SimpleServerMessageBuffer>();
        }

        public int TotalItems => WallItems.Keys.Count + FloorItems.Keys.Count;

        /// <summary>
        ///     Gets or sets a value indicating whether [got rollers].
        /// </summary>
        /// <value><c>true</c> if [got rollers]; otherwise, <c>false</c>.</value>
        internal bool GotRollers { get; set; }

        internal RoomItem GetItem(uint itemId)
        {
            if (FloorItems.ContainsKey(itemId)) return FloorItems[itemId];
            return WallItems.ContainsKey(itemId) ? WallItems[itemId] : null;
        }

        /// <summary>
        ///     Gets the random breeding bear.
        /// </summary>
        /// <param name="pet">The pet.</param>
        /// <returns>Point.</returns>
        internal Point GetRandomBreedingBear(Pet pet)
        {
            if (!BreedingBear.Any())
                return new Point();
            List<uint> keys = new List<uint>(BreedingBear.Keys);
            int size = BreedingBear.Count;
            Random rand = new Random();
            uint randomKey = keys[rand.Next(size)];

            BreedingBear[randomKey].PetsList.Add(pet);
            pet.WaitingForBreading = BreedingBear[randomKey].Id;
            pet.BreadingTile = BreedingBear[randomKey].Coordinate;

            return BreedingBear[randomKey].Coordinate;
        }

        /// <summary>
        ///     Gets the random breeding terrier.
        /// </summary>
        /// <param name="pet">The pet.</param>
        /// <returns>Point.</returns>
        internal Point GetRandomBreedingTerrier(Pet pet)
        {
            if (!BreedingTerrier.Any())
                return new Point();
            List<uint> keys = new List<uint>(BreedingTerrier.Keys);
            int size = BreedingTerrier.Count;
            Random rand = new Random();
            uint randomKey = keys[rand.Next(size)];

            BreedingTerrier[randomKey].PetsList.Add(pet);
            pet.WaitingForBreading = BreedingTerrier[randomKey].Id;
            pet.BreadingTile = BreedingTerrier[randomKey].Coordinate;

            return BreedingTerrier[randomKey].Coordinate;
        }

        /// <summary>
        ///     Saves the furniture.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        public void SaveFurniture(IQueryAdapter dbClient)
        {
            if (!_updatedItems.Any() && !_removedItems.Any() && _room.GetRoomUserManager()?.PetCount <= 0)
                return;

            foreach (uint itemId in _removedItems)
                dbClient.RunFastQuery(
                    $"UPDATE items_rooms SET room_id = 0, x = 0, y = 0, z = '0', rot = 0 WHERE id = {itemId}");

            foreach (RoomItem roomItem in _updatedItems.Select(GetItem).Where(roomItem => roomItem != null))
            {
                if (roomItem.GetBaseItem() != null && roomItem.GetBaseItem().IsGroupItem)
                {
                    try
                    {
                        string[] gD = roomItem.GroupData.Split(';');

                        roomItem.ExtraData = roomItem.ExtraData + ";" + gD[1] + ";" + gD[2] + ";" + gD[3];
                    }
                    catch
                    {
                        roomItem.ExtraData = string.Empty;
                    }
                }

                if (roomItem.RoomId == 0)
                    continue;

                if (roomItem.GetBaseItem().Name.Contains("wallpaper_single") ||
                    roomItem.GetBaseItem().Name.Contains("floor_single") ||
                    roomItem.GetBaseItem().Name.Contains("landscape_single"))
                {
                    dbClient.RunFastQuery($"DELETE FROM items_rooms WHERE id = {roomItem.Id} LIMIT 1");

                    continue;
                }

                if (roomItem.IsFloorItem)
                {
                    dbClient.SetQuery($"UPDATE items_rooms SET room_id = {roomItem.RoomId}, extra_data = @extraData, x = {roomItem.X}, y = {roomItem.Y}, z = '{roomItem.Z.ToString(CultureInfo.InvariantCulture).Replace(',', '.')}', rot = {roomItem.Rot} WHERE id = {roomItem.Id}");

                    dbClient.AddParameter("extraData", roomItem.ExtraData);

                    dbClient.RunQuery();
                }
                else
                {
                    dbClient.SetQuery($"UPDATE items_rooms SET room_id = {roomItem.RoomId}, extra_data = @extraData, wall_pos = @wallPos WHERE id = {roomItem.Id}");

                    dbClient.AddParameter("extraData", roomItem.ExtraData);
                    dbClient.AddParameter("wallPos", roomItem.WallCoord);

                    dbClient.RunQuery();
                }
            }

            _room.GetRoomUserManager()?.UpdatePetsInDataBase();

            _updatedItems?.Clear();
            _removedItems?.Clear();
        }

        /// <summary>
        ///     Checks the position item.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="rItem">The r item.</param>
        /// <param name="newX">The new x.</param>
        /// <param name="newY">The new y.</param>
        /// <param name="newRot">The new rot.</param>
        /// <param name="newItem">if set to <c>true</c> [new item].</param>
        /// <param name="sendNotify">if set to <c>true</c> [send notify].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckPosItem(GameClient session, RoomItem rItem, int newX, int newY, int newRot, bool newItem,
            bool sendNotify = true)
        {
            try
            {
                Dictionary<int, ThreeDCoord> dictionary = Gamemap.GetAffectedTiles(rItem.GetBaseItem().Length, rItem.GetBaseItem().Width, newX,
                    newY, newRot);

                if (!_room.GetGameMap().ValidTile(newX, newY))
                    return false;

                if (
                    dictionary.Values.Any(
                        coord =>
                            (_room.GetGameMap().Model.DoorX == coord.X) && (_room.GetGameMap().Model.DoorY == coord.Y)))
                    return false;

                if ((_room.GetGameMap().Model.DoorX == newX) && (_room.GetGameMap().Model.DoorY == newY))
                    return false;

                if (dictionary.Values.Any(coord => !_room.GetGameMap().ValidTile(coord.X, coord.Y)))
                    return false;

                double num = _room.GetGameMap().Model.SqFloorHeight[newX][newY];

                if (rItem.Rot == newRot && rItem.X == newX && rItem.Y == newY && rItem.Z != num)
                    return false;

                if (_room.GetGameMap().Model.SqState[newX][newY] != SquareState.Open)
                    return false;

                if (
                    dictionary.Values.Any(
                        coord => _room.GetGameMap().Model.SqState[coord.X][coord.Y] != SquareState.Open))
                    return false;

                if (!rItem.GetBaseItem().IsSeat)
                {
                    if (_room.GetGameMap().SquareHasUsers(newX, newY))
                        return false;

                    if (dictionary.Values.Any(coord => _room.GetGameMap().SquareHasUsers(coord.X, coord.Y)))
                        return false;
                }

                List<RoomItem> furniObjects = GetFurniObjects(newX, newY);
                List<RoomItem> collection = new List<RoomItem>();
                List<RoomItem> list3 = new List<RoomItem>();

                foreach (
                    List<RoomItem> list4 in
                        dictionary.Values.Select(coord => GetFurniObjects(coord.X, coord.Y))
                            .Where(list4 => list4 != null))
                    collection.AddRange(list4);

                if (furniObjects == null)
                    furniObjects = new List<RoomItem>();

                list3.AddRange(furniObjects);
                list3.AddRange(collection);

                return list3.All(item => (item.Id == rItem.Id) || item.GetBaseItem().Stackable);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Queues the room item update.
        /// </summary>
        /// <param name="item">The item.</param>
        internal void QueueRoomItemUpdate(RoomItem item)
        {
            lock (_roomItemUpdateQueue.SyncRoot)
                _roomItemUpdateQueue.Enqueue(item);
        }

        /// <summary>
        ///     Removes all furniture.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>List&lt;RoomItem&gt;.</returns>
        internal List<RoomItem> RemoveAllFurniture(GameClient session)
        {
            List<RoomItem> items = new List<RoomItem>();
            Gamemap roomGamemap = _room.GetGameMap();

            foreach (RoomItem item in FloorItems.Values.ToArray())
            {
                item.Interactor.OnRemove(session, item);

                roomGamemap.RemoveSpecialItem(item);

                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("PickUpFloorItemMessageComposer"));
                simpleServerMessageBuffer.AppendString(item.Id.ToString());
                simpleServerMessageBuffer.AppendBool(false); //expired
                simpleServerMessageBuffer.AppendInteger(item.UserId); //pickerId
                simpleServerMessageBuffer.AppendInteger(0); // delay
                _room.SendMessage(simpleServerMessageBuffer);

                if (item.IsBuilder)
                {
                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE id='{item.Id}'");

                    continue;
                }

                items.Add(item);
            }

            foreach (RoomItem item in WallItems.Values.ToArray())
            {
                item.Interactor.OnRemove(session, item);

                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("PickUpWallItemMessageComposer"));
                simpleServerMessageBuffer.AppendString(item.Id.ToString());
                simpleServerMessageBuffer.AppendInteger(item.UserId);
                _room.SendMessage(simpleServerMessageBuffer);

                if (item.IsBuilder)
                {
                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE id='{item.Id}'");

                    continue;
                }

                items.Add(item);
            }

            _removedItems.Clear();
            _updatedItems.Clear();
            WallItems.Clear();
            FloorItems.Clear();
            Rollers.Clear();

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery($"UPDATE items_rooms SET room_id='0' WHERE room_id='{_room.RoomId}'");

            _room.GetGameMap().GenerateMaps();
            _room.GetRoomUserManager().OnUserUpdateStatus();

            return items;
        }

        /// <summary>
        ///     Remove Items On Room GroupBy Username
        /// </summary>
        /// <param name="roomItemList"></param>
        /// <param name="session"></param>
        internal void RemoveItemsByOwner(ref List<RoomItem> roomItemList, ref GameClient session)
        {
            List<GameClient> toUpdate = new List<GameClient>();

            foreach (RoomItem item in roomItemList)
            {
                if (item.UserId == 0)
                    item.UserId = session.GetHabbo().Id;

                GameClient client = Yupi.GetGame().GetClientManager().GetClientByUserId(item.UserId);

                if (item.GetBaseItem().InteractionType != Interaction.PostIt)
                {
                    if (!toUpdate.Contains(client))
                        toUpdate.Add(client);

                    if (client == null)
                        using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
                            dbClient.RunFastQuery("UPDATE items_rooms SET room_id = '0' WHERE id = " + item.Id);
                    else
                        client.GetHabbo().GetInventoryComponent().AddItem(item);
                }
            }

            foreach (GameClient client in toUpdate)
                client?.GetHabbo().GetInventoryComponent().UpdateItems(true);
        }

        /// <summary>
        ///     Sets the speed.
        /// </summary>
        /// <param name="p">The p.</param>
        internal void SetSpeed(uint p)
        {
            _rollerSpeed = p;
        }

        /// <summary>
        ///     Loads the furniture.
        /// </summary>
        internal void LoadFurniture()
        {
            if (FloorItems == null)
                FloorItems = new ConcurrentDictionary<uint, RoomItem>();
            else
                FloorItems.Clear();

            if (WallItems == null)
                WallItems = new ConcurrentDictionary<uint, RoomItem>();
            else
                WallItems.Clear();

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.RunFastQuery("SELECT items_rooms.*, COALESCE(items_groups.group_id, 0) AS group_id FROM items_rooms LEFT OUTER JOIN items_groups ON items_rooms.id = items_groups.id WHERE items_rooms.room_id = " + _room.RoomId + " LIMIT 5000");

                DataTable table = queryReactor.GetTable();

                if (table.Rows.Count >= 5000)
                {
                    GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(_room.RoomData.OwnerId);

                    clientByUserId?.SendNotif("Your room has more than 5000 items in it. The current limit of items per room is 5000.\nTo view the rest, pick some of these items up!");
                }

                foreach (DataRow dataRow in table.Rows)
                {
                    try
                    {
                        uint id = Convert.ToUInt32(dataRow["id"]);
                        int x = Convert.ToInt32(dataRow["x"]);
                        int y = Convert.ToInt32(dataRow["y"]);
                        double z = Convert.ToDouble(dataRow["z"]);
                        sbyte rot = Convert.ToSByte(dataRow["rot"]);
                        uint ownerId = Convert.ToUInt32(dataRow["user_id"]);
                        string baseItemName = dataRow["item_name"].ToString();

                        Item item = Yupi.GetGame().GetItemManager().GetItemByName(baseItemName);

                        if (item == null)
                            continue;

                        if (ownerId == 0)
                        {
                            queryReactor.RunFastQuery("UPDATE items_rooms SET user_id = " + _room.RoomData.OwnerId + " WHERE id = " + id);
                        }   

                        string locationData = item.Type == 'i' && string.IsNullOrWhiteSpace(dataRow["wall_pos"].ToString()) ? ":w=0,2 l=11,53 l" : dataRow["wall_pos"].ToString();

                        string extraData = DBNull.Value.Equals(dataRow["extra_data"]) ? string.Empty : dataRow["extra_data"].ToString();

                        string songCode = DBNull.Value.Equals(dataRow["songcode"]) ? string.Empty : (string) dataRow["songcode"];

                        uint groupId = Convert.ToUInt32(dataRow["group_id"]);

                        if (item.Type == 'i')
                        {
                            WallCoordinate wallCoord = new WallCoordinate(':' + locationData.Split(':')[1]);

                            RoomItem value = new RoomItem(id, _room.RoomId, baseItemName, extraData, wallCoord, _room,  ownerId, groupId, Yupi.EnumToBool((string) dataRow["builders"]));

                            WallItems.TryAdd(id, value);
                        }
                        else
                        {
                            RoomItem roomItem = new RoomItem(id, _room.RoomId, baseItemName, extraData, x, y, z, rot, _room, ownerId, groupId, songCode, Yupi.EnumToBool((string) dataRow["builders"]));

                            if (!_room.GetGameMap().ValidTile(x, y))
                            {
                                GameClient clientByUserId2 = Yupi.GetGame().GetClientManager().GetClientByUserId(ownerId);

                                if (clientByUserId2 != null)
                                {
                                    clientByUserId2.GetHabbo().GetInventoryComponent().AddNewItem(roomItem.Id, roomItem.BaseName, roomItem.ExtraData, groupId, true, true, 0, 0);

                                    clientByUserId2.GetHabbo().GetInventoryComponent().UpdateItems(true);
                                }

                                queryReactor.RunFastQuery("UPDATE items_rooms SET room_id = 0 WHERE id = " + roomItem.Id);
                            }
                            else
                            {
                                if (item.InteractionType == Interaction.Hopper)
                                    HopperCount++;

                                FloorItems.TryAdd(id, roomItem);
                            }
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            foreach (RoomItem current in FloorItems.Values)
            {
                if (current.IsWired)
                    _room.GetWiredHandler().LoadWired(_room.GetWiredHandler().GenerateNewItem(current));

                if (current.IsRoller)
                    GotRollers = true;

                else if (current.GetBaseItem().InteractionType == Interaction.Dimmer)
                {
                    if (_room.MoodlightData == null)
                        _room.MoodlightData = new MoodlightData(current.Id);
                }
                else if (current.GetBaseItem().InteractionType == Interaction.RoomBg && _room.TonerData == null)
                    _room.TonerData = new TonerData(current.Id);
            }
        }

        /// <summary>
        ///     Removes the furniture.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="wasPicked">if set to <c>true</c> [was picked].</param>
        internal void RemoveFurniture(GameClient session, uint id, bool wasPicked = true)
        {
            RoomItem item = GetItem(id);

            if (item == null)
                return;

            if (item.GetBaseItem().InteractionType == Interaction.FootballGate)
                _room.GetSoccer().UnRegisterGate(item);

            if (item.GetBaseItem().InteractionType != Interaction.Gift)
                item.Interactor.OnRemove(session, item);

            if (item.GetBaseItem().InteractionType == Interaction.Bed ||  item.GetBaseItem().InteractionType == Interaction.PressurePadBed)
                _room.ContainsBeds--;

            RemoveRoomItem(item, wasPicked);
        }

        /// <summary>
        ///     Removes the room item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="wasPicked">if set to <c>true</c> [was picked].</param>
        internal void RemoveRoomItem(RoomItem item, bool wasPicked)
        {
            if (item.IsWallItem)
            {
                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("PickUpWallItemMessageComposer"));
                simpleServerMessageBuffer.AppendString(item.Id.ToString());
                simpleServerMessageBuffer.AppendInteger(wasPicked ? item.UserId : 0);
                _room.SendMessage(simpleServerMessageBuffer);
            }
            else if (item.IsFloorItem)
            {
                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("PickUpFloorItemMessageComposer"));
                simpleServerMessageBuffer.AppendString(item.Id.ToString());
                simpleServerMessageBuffer.AppendBool(false); //expired
                simpleServerMessageBuffer.AppendInteger(wasPicked ? item.UserId : 0); //pickerId
                simpleServerMessageBuffer.AppendInteger(0); // delay
                _room.SendMessage(simpleServerMessageBuffer);
            }

            RoomItem junkItem;
            if (item.IsWallItem)
            {
                WallItems.TryRemove(item.Id, out junkItem);
            }
            else
            {
                FloorItems.TryRemove(item.Id, out junkItem);
                _room.GetGameMap().RemoveFromMap(item);
            }

            RemoveItem(item.Id);
            _room.GetRoomUserManager().OnUserUpdateStatus(item.X, item.Y);
        }

        /// <summary>
        ///     Updates the item on roller.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="nextCoord">The next coord.</param>
        /// <param name="rolledId">The rolled identifier.</param>
        /// <param name="nextZ">The next z.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal SimpleServerMessageBuffer UpdateItemOnRoller(RoomItem item, Point nextCoord, uint rolledId, double nextZ)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer();
            simpleServerMessageBuffer.Init(PacketLibraryManager.OutgoingRequest("ItemAnimationMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(item.X);
            simpleServerMessageBuffer.AppendInteger(item.Y);
            simpleServerMessageBuffer.AppendInteger(nextCoord.X);
            simpleServerMessageBuffer.AppendInteger(nextCoord.Y);
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendInteger(item.Id);
            simpleServerMessageBuffer.AppendString(ServerUserChatTextHandler.GetString(item.Z));
            simpleServerMessageBuffer.AppendString(ServerUserChatTextHandler.GetString(nextZ));
            simpleServerMessageBuffer.AppendInteger(rolledId);
            SetFloorItem(item, nextCoord.X, nextCoord.Y, nextZ);
            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Updates the user on roller.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="nextCoord">The next coord.</param>
        /// <param name="rollerId">The roller identifier.</param>
        /// <param name="nextZ">The next z.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal SimpleServerMessageBuffer UpdateUserOnRoller(RoomUser user, Point nextCoord, uint rollerId, double nextZ)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(0);
            simpleServerMessageBuffer.Init(PacketLibraryManager.OutgoingRequest("ItemAnimationMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(user.X);
            simpleServerMessageBuffer.AppendInteger(user.Y);
            simpleServerMessageBuffer.AppendInteger(nextCoord.X);
            simpleServerMessageBuffer.AppendInteger(nextCoord.Y);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(rollerId);
            simpleServerMessageBuffer.AppendInteger(2);
            simpleServerMessageBuffer.AppendInteger(user.VirtualId);
            simpleServerMessageBuffer.AppendString(ServerUserChatTextHandler.GetString(user.Z));
            simpleServerMessageBuffer.AppendString(ServerUserChatTextHandler.GetString(nextZ));
            _room.GetGameMap()
                .UpdateUserMovement(new Point(user.X, user.Y), new Point(nextCoord.X, nextCoord.Y), user);
            _room.GetGameMap().GameMap[user.X, user.Y] = 1;
            user.X = nextCoord.X;
            user.Y = nextCoord.Y;
            user.Z = nextZ;
            _room.GetGameMap().GameMap[user.X, user.Y] = 0;
            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Sets the floor item.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="item">The item.</param>
        /// <param name="newX">The new x.</param>
        /// <param name="newY">The new y.</param>
        /// <param name="newRot">The new rot.</param>
        /// <param name="newItem">if set to <c>true</c> [new item].</param>
        /// <param name="onRoller">if set to <c>true</c> [on roller].</param>
        /// <param name="sendMessage">if set to <c>true</c> [send message].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool SetFloorItem(GameClient session, RoomItem item, int newX, int newY, int newRot, bool newItem,
            bool onRoller, bool sendMessage)
        {
            return SetFloorItem(session, item, newX, newY, newRot, newItem, onRoller, sendMessage, true, false);
        }

        /// <summary>
        ///     Sets the floor item.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="item">The item.</param>
        /// <param name="newX">The new x.</param>
        /// <param name="newY">The new y.</param>
        /// <param name="newRot">The new rot.</param>
        /// <param name="newItem">if set to <c>true</c> [new item].</param>
        /// <param name="onRoller">if set to <c>true</c> [on roller].</param>
        /// <param name="sendMessage">if set to <c>true</c> [send message].</param>
        /// <param name="updateRoomUserStatuses">if set to <c>true</c> [update room user statuses].</param>
        /// <param name="specialMove"></param>
        /// <param name="customHeight"></param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool SetFloorItem(GameClient session, RoomItem item, int newX, int newY, int newRot, bool newItem, bool onRoller, bool sendMessage, bool updateRoomUserStatuses, bool specialMove, double? customHeight = null)
        {
            bool flag = false;

            if (!newItem)
                flag = _room.GetGameMap().RemoveFromMap(item);

            Dictionary<int, ThreeDCoord> affectedTiles = Gamemap.GetAffectedTiles(item.GetBaseItem().Length,
                item.GetBaseItem().Width, newX, newY, newRot);

            Point oldCoord = item.Coordinate;

            if (!_room.GetGameMap().ValidTile(newX, newY) ||
                (_room.GetGameMap().SquareHasUsers(newX, newY) && !item.GetBaseItem().IsSeat))
            {
                if (!flag)
                    return false;

                AddOrUpdateItem(item.Id);
                _room.GetGameMap().AddToMap(item);

                return false;
            }

            if (
                affectedTiles.Values.Any(
                    current =>
                        !_room.GetGameMap().ValidTile(current.X, current.Y) ||
                        (_room.GetGameMap().SquareHasUsers(current.X, current.Y) && !item.GetBaseItem().IsSeat)))
            {
                if (!flag)
                    return false;

                AddOrUpdateItem(item.Id);
                _room.GetGameMap().AddToMap(item);

                return false;
            }

            double height = customHeight ?? _room.GetGameMap().Model.SqFloorHeight[newX][newY];

            if (!onRoller)
            {
                if (_room.GetGameMap().Model.SqState[newX][newY] != SquareState.Open && !item.GetBaseItem().IsSeat)
                {
                    if (!flag)
                        return false;

                    AddOrUpdateItem(item.Id);

                    return false;
                }

                if (
                    affectedTiles.Values.Any(
                        current2 =>
                            !item.GetBaseItem().IsSeat &&
                            _room.GetGameMap().Model.SqState[current2.X][current2.Y] != SquareState.Open))
                {
                    if (!flag)
                        return false;

                    AddOrUpdateItem(item.Id);
                    _room.GetGameMap().AddToMap(item);

                    return false;
                }

                if (!item.GetBaseItem().IsSeat && !item.IsRoller)
                {
                    if (
                        affectedTiles.Values.Any(
                            current3 => _room.GetGameMap().GetRoomUsers(new Point(current3.X, current3.Y)).Any()))
                    {
                        if (!flag)
                            return false;

                        AddOrUpdateItem(item.Id);
                        _room.GetGameMap().AddToMap(item);

                        return false;
                    }
                }
            }

            List<RoomItem> furniObjects = GetFurniObjects(newX, newY);
            List<RoomItem> list = new List<RoomItem>();
            List<RoomItem> list2 = new List<RoomItem>();

            foreach (
                List<RoomItem> furniObjects2 in
                    affectedTiles.Values.Select(current4 => GetFurniObjects(current4.X, current4.Y))
                        .Where(furniObjects2 => furniObjects2 != null))
                list.AddRange(furniObjects2);

            list2.AddRange(furniObjects);
            list2.AddRange(list);

            RoomItem stackMagic =
                list2.FirstOrDefault(
                    roomItem =>
                        roomItem?.GetBaseItem() != null &&
                        roomItem.GetBaseItem().InteractionType == Interaction.TileStackMagic);

            if (stackMagic != null)
                height = stackMagic.Z;
            else if (!onRoller && item.GetBaseItem().InteractionType != Interaction.TileStackMagic)
            {
                if (
                    list2.Any(
                        current5 =>
                            current5 != null && current5.Id != item.Id && current5.GetBaseItem() != null &&
                            !current5.GetBaseItem().Stackable))
                {
                    if (!flag)
                        return false;

                    AddOrUpdateItem(item.Id);
                    _room.GetGameMap().AddToMap(item);

                    return false;
                }

                if (item.Rot != newRot && item.X == newX && item.Y == newY)
                    height = item.Z;

                foreach (RoomItem current6 in list2)
                    if (current6.Id != item.Id && current6.TotalHeight > height)
                        height = current6.TotalHeight;
            }

            if (item.GetBaseItem().Name == "boutique_mannequin1" || item.GetBaseItem().Name == "gld_wall_tall")
            {
                if (newRot < 0 || newRot > 12)
                {
                    newRot = 0;
                }
            }
            else if (item.GetBaseItem().Name == "pirate_stage2" || item.GetBaseItem().Name == "pirate_stage2_g")
            {
                if (newRot < 0 || newRot > 7)
                    newRot = 0;
            }
            else if (item.GetBaseItem().Name == "gh_div_cor" || item.GetBaseItem().Name == "hblooza14_duckcrn" ||
                     item.GetBaseItem().Name == "hween13_dwarfcrn")
            {
                if (newRot != 1 && newRot != 3 && newRot != 5 && newRot != 7)
                    newRot = 0;
            }
            else if (item.GetBaseItem().Name == "val14_b_roof" || item.GetBaseItem().Name == "val14_g_roof" ||
                     item.GetBaseItem().Name == "val14_y_roof")
            {
                if (newRot != 2 && newRot != 3 && newRot != 4 && newRot != 7)
                    newRot = 0;
            }
            else if (item.GetBaseItem().Name == "val13_div_1")
            {
                if (newRot < 0 || newRot > 6)
                    newRot = 0;
            }
            else if (item.GetBaseItem().Name == "room_info15_shrub1")
            {
                if (newRot != 0 && newRot != 2 && newRot != 3 && newRot != 4 && newRot != 6)
                    newRot = 0;
            }
            else if (item.GetBaseItem().Name == "room_info15_div")
            {
                if (newRot < 0 || newRot > 5)
                    newRot = 0;
            }
            else
            {
                if (newRot != 0 && newRot != 2 && newRot != 4 && newRot != 6 && newRot != 8)
                    newRot = 0;
            }

            item.Rot = newRot;

            item.SetState(newX, newY, height, affectedTiles);

            if (!onRoller && session != null)
                item.Interactor.OnPlace(session, item);

            if (newItem)
            {
                if (FloorItems.ContainsKey(item.Id))
                    return true;

                if (item.IsFloorItem)
                    FloorItems.TryAdd(item.Id, item);
                else if (item.IsWallItem)
                    WallItems.TryAdd(item.Id, item);

                AddOrUpdateItem(item.Id);

                if (sendMessage)
                {
                    SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("AddFloorItemMessageComposer"));
                    item.Serialize(simpleServerMessageBuffer);

                    simpleServerMessageBuffer.AppendString(_room.RoomData.Group != null
                        ? session.GetHabbo().UserName
                        : _room.RoomData.Owner);

                    _room.SendMessage(simpleServerMessageBuffer);
                }
            }
            else
            {
                AddOrUpdateItem(item.Id);
                if (!onRoller && sendMessage)
                {
                    if (specialMove)
                    {
                        SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("ItemAnimationMessageComposer"));
                        messageBuffer.AppendInteger(oldCoord.X);
                        messageBuffer.AppendInteger(oldCoord.Y);
                        messageBuffer.AppendInteger(newX);
                        messageBuffer.AppendInteger(newY);
                        messageBuffer.AppendInteger(1);
                        messageBuffer.AppendInteger(item.Id);
                        messageBuffer.AppendString(ServerUserChatTextHandler.GetString(item.Z));
                        messageBuffer.AppendString(ServerUserChatTextHandler.GetString(item.Z));
                        messageBuffer.AppendInteger(-1);
                        _room.SendMessage(messageBuffer);
                    }
                    else
                    {
                        SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("UpdateRoomItemMessageComposer"));
                        item.Serialize(messageBuffer);
                        _room.SendMessage(messageBuffer);
                    }
                }

                if (item.IsWired)
                    _room.GetWiredHandler().MoveWired(item);
            }

            _room.GetGameMap().AddToMap(item);

            if (item.GetBaseItem().IsSeat)
                updateRoomUserStatuses = true;

            if (updateRoomUserStatuses)
            {
                _room.GetRoomUserManager().OnUserUpdateStatus(oldCoord.X, oldCoord.Y);
                _room.GetRoomUserManager().OnUserUpdateStatus(item.X, item.Y);
            }

            if (newItem)
                OnHeightMapUpdate(affectedTiles);

            return true;
        }

        internal void DeveloperSetFloorItem(GameClient session, RoomItem item)
        {
            if (FloorItems.ContainsKey(item.Id))
                return;

            FloorItems.TryAdd(item.Id, item);

            AddOrUpdateItem(item.Id);

            Dictionary<int, ThreeDCoord> affectedTiles = Gamemap.GetAffectedTiles(item.GetBaseItem().Length, item.GetBaseItem().Width, item.X,
                item.Y, item.Rot);
            item.SetState(item.X, item.Y, item.Z, affectedTiles);

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("AddFloorItemMessageComposer"));
            item.Serialize(simpleServerMessageBuffer);
            simpleServerMessageBuffer.AppendString(_room.RoomData.Group != null ? session.GetHabbo().UserName : _room.RoomData.Owner);
            _room.SendMessage(simpleServerMessageBuffer);

            _room.GetGameMap().AddToMap(item);
        }

        /// <summary>
        ///     Called when [height map update].
        /// </summary>
        /// <param name="affectedTiles">The affected tiles.</param>
        internal void OnHeightMapUpdate(Dictionary<int, ThreeDCoord> affectedTiles)
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("UpdateFurniStackMapMessageComposer"));
            messageBuffer.AppendByte((byte) affectedTiles.Count);

            foreach (ThreeDCoord coord in affectedTiles.Values)
            {
                messageBuffer.AppendByte((byte) coord.X);
                messageBuffer.AppendByte((byte) coord.Y);
                messageBuffer.AppendShort((short) (_room.GetGameMap().SqAbsoluteHeight(coord.X, coord.Y)*256));
            }

            _room.SendMessage(messageBuffer);
        }

        /// <summary>
        ///     Called when [height map update].
        /// </summary>
        /// <param name="affectedTiles">The affected tiles.</param>
        internal void OnHeightMapUpdate(ICollection affectedTiles)
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("UpdateFurniStackMapMessageComposer"));
            messageBuffer.AppendByte((byte) affectedTiles.Count);

            foreach (Point coord in affectedTiles)
            {
                messageBuffer.AppendByte((byte) coord.X);
                messageBuffer.AppendByte((byte) coord.Y);
                messageBuffer.AppendShort((short) (_room.GetGameMap().SqAbsoluteHeight(coord.X, coord.Y)*256));
            }

            _room.SendMessage(messageBuffer);
        }

        /// <summary>
        ///     Called when [height map update].
        /// </summary>
        /// <param name="oldCoords">The old coords.</param>
        /// <param name="newCoords">The new coords.</param>
        internal void OnHeightMapUpdate(List<Point> oldCoords, List<Point> newCoords)
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("UpdateFurniStackMapMessageComposer"));
            messageBuffer.AppendByte((byte) (oldCoords.Count + newCoords.Count));

            foreach (Point coord in oldCoords)
            {
                messageBuffer.AppendByte((byte) coord.X);
                messageBuffer.AppendByte((byte) coord.Y);
                messageBuffer.AppendShort((short) (_room.GetGameMap().SqAbsoluteHeight(coord.X, coord.Y)*256));
            }

            foreach (Point nCoord in newCoords)
            {
                messageBuffer.AppendByte((byte) nCoord.X);
                messageBuffer.AppendByte((byte) nCoord.Y);
                messageBuffer.AppendShort((short) (_room.GetGameMap().SqAbsoluteHeight(nCoord.X, nCoord.Y)*256));
            }

            _room.SendMessage(messageBuffer);
        }

        /// <summary>
        ///     Gets the furni objects.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>List&lt;RoomItem&gt;.</returns>
        internal List<RoomItem> GetFurniObjects(int x, int y) => _room.GetGameMap().GetCoordinatedItems(new Point(x, y));

        /// <summary>
        ///     Sets the floor item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="newX">The new x.</param>
        /// <param name="newY">The new y.</param>
        /// <param name="newZ">The new z.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool SetFloorItem(RoomItem item, int newX, int newY, double newZ)
        {
            _room.GetGameMap().RemoveFromMap(item);

            item.SetState(newX, newY, newZ,
                Gamemap.GetAffectedTiles(item.GetBaseItem().Length, item.GetBaseItem().Width, newX, newY, item.Rot));

            if (item.GetBaseItem().InteractionType == Interaction.RoomBg && _room.TonerData == null)
                _room.TonerData = new TonerData(item.Id);

            AddOrUpdateItem(item.Id);
            _room.GetGameMap().AddItemToMap(item);

            return true;
        }

        /// <summary>
        ///     Sets the floor item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="newX">The new x.</param>
        /// <param name="newY">The new y.</param>
        /// <param name="newZ">The new z.</param>
        /// <param name="rot">The rot.</param>
        /// <param name="sendUpdate">if set to <c>true</c> [sendupdate].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool SetFloorItem(RoomItem item, int newX, int newY, double newZ, int rot, bool sendUpdate)
        {
            _room.GetGameMap().RemoveFromMap(item);

            item.SetState(newX, newY, newZ,
                Gamemap.GetAffectedTiles(item.GetBaseItem().Length, item.GetBaseItem().Width, newX, newY, rot));

            if (item.GetBaseItem().InteractionType == Interaction.RoomBg && _room.TonerData == null)
                _room.TonerData = new TonerData(item.Id);

            AddOrUpdateItem(item.Id);
            _room.GetGameMap().AddItemToMap(item);

            if (!sendUpdate)
                return true;

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("UpdateRoomItemMessageComposer"));
            item.Serialize(messageBuffer);
            _room.SendMessage(messageBuffer);

            return true;
        }

        /// <summary>
        ///     Sets the wall item.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool SetWallItem(GameClient session, RoomItem item)
        {
            if (!item.IsWallItem || WallItems.ContainsKey(item.Id))
                return false;

            if (FloorItems.ContainsKey(item.Id))
                return true;

            item.Interactor.OnPlace(session, item);

            if (item.GetBaseItem().InteractionType == Interaction.Dimmer && _room.MoodlightData == null)
            {
                _room.MoodlightData = new MoodlightData(item.Id);
                item.ExtraData = _room.MoodlightData.GenerateExtraData();
            }

            WallItems.TryAdd(item.Id, item);
            AddOrUpdateItem(item.Id);

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("AddWallItemMessageComposer"));
            item.Serialize(simpleServerMessageBuffer);
            simpleServerMessageBuffer.AppendString(_room.RoomData.Owner);
            _room.SendMessage(simpleServerMessageBuffer);

            return true;
        }

        /// <summary>
        ///     Updates the item.
        /// </summary>
        /// <param name="itemId">The item.</param>
        internal void AddOrUpdateItem(uint itemId)
        {
            if (_removedItems.Contains(itemId))
                _removedItems.Remove(itemId);

            if (_updatedItems.Contains(itemId))
                return;

            _updatedItems.Add(itemId);
        }

        /// <summary>
        ///     Removes the item.
        /// </summary>
        /// <param name="itemId"></param>
        internal void RemoveItem(uint itemId)
        {
            if (_updatedItems.Contains(itemId))
                _updatedItems.Remove(itemId);

            if (!_removedItems.Contains(itemId))
                _removedItems.Add(itemId);

            RoomItem junkItem;
            Rollers.TryRemove(itemId, out junkItem);
        }

        /// <summary>
        ///     Called when [cycle].
        /// </summary>
        internal void OnCycle()
        {
            if (GotRollers)
            {
                try
                {
                    _room.SendMessage(CycleRollers());
                }
                catch (Exception ex)
                {
                    YupiLogManager.LogException(ex, "Registered HabboHotel Thread Exception.", "Yupi.Mobi");

                    GotRollers = false;
                }
            }

            if (_roomItemUpdateQueue.Count > 0)
            {
                List<RoomItem> addItems = new List<RoomItem>();

                lock (_roomItemUpdateQueue.SyncRoot)
                {
                    while (_roomItemUpdateQueue.Count > 0)
                    {
                        RoomItem roomItem = (RoomItem) _roomItemUpdateQueue.Dequeue();
                        roomItem.ProcessUpdates();

                        if (roomItem.IsTrans || roomItem.UpdateCounter > 0)
                            addItems.Add(roomItem);
                    }

                    foreach (RoomItem item in addItems)
                        _roomItemUpdateQueue.Enqueue(item);
                }
            }
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        internal void Destroy()
        {
            FloorItems.Clear();
            WallItems.Clear();
            _removedItems.Clear();
            _updatedItems.Clear();

            lock (_roomItemUpdateQueue.SyncRoot)
            {
                _roomItemUpdateQueue.Clear();
                _roomItemUpdateQueue = null;
            }

            _room = null;
            FloorItems = null;
            WallItems = null;
            _removedItems = null;
            _updatedItems = null;
            BreedingBear.Clear();
            BreedingTerrier.Clear();
            WallItems = null;
            BreedingBear.Clear();
            BreedingTerrier.Clear();
        }

        /// <summary>
        ///     Cycles the rollers.
        /// </summary>
        /// <returns>List&lt;SimpleServerMessageBuffer&gt;.</returns>
        private List<SimpleServerMessageBuffer> CycleRollers()
        {
            if (!GotRollers)
                return new List<SimpleServerMessageBuffer>();

            if (_roolerCycle >= _rollerSpeed || _rollerSpeed == 0)
            {
                _rollerItemsMoved.Clear();
                _rollerUsersMoved.Clear();
                _rollerMessages.Clear();

                foreach (RoomItem current in Rollers.Values)
                {
                    Point squareInFront = current.SquareInFront;
                    List<RoomItem> roomItemForSquare = _room.GetGameMap().GetRoomItemForSquare(current.X, current.Y);
                    RoomUser userForSquare = _room.GetRoomUserManager().GetUserForSquare(current.X, current.Y);

                    if (!roomItemForSquare.Any() && userForSquare == null)
                        continue;

                    List<RoomItem> coordinatedItems = _room.GetGameMap().GetCoordinatedItems(squareInFront);

                    double nextZ = 0.0;
                    int num = 0;
                    bool flag = false;
                    double num2 = 0.0;
                    bool flag2 = true;
                    bool frontHasItem = false;

                    foreach (RoomItem current2 in coordinatedItems.Where(current2 => current2.IsRoller))
                    {
                        flag = true;
                        if (current2.TotalHeight > num2)
                            num2 = current2.TotalHeight;
                    }

                    if (coordinatedItems.Any(item => !item.GetBaseItem().Stackable))
                        frontHasItem = true;

                    if (flag)
                    {
                        using (List<RoomItem>.Enumerator enumerator3 = coordinatedItems.GetEnumerator())
                        {
                            while (enumerator3.MoveNext())
                            {
                                RoomItem current3 = enumerator3.Current;
                                if (current3.TotalHeight > num2)
                                    flag2 = false;
                            }
                            goto IL_192;
                        }
                    }

                    goto IL_17C;
                    IL_192:
                    nextZ = num2;
                    bool flag3 = num > 0 ||
                                _room.GetRoomUserManager().GetUserForSquare(squareInFront.X, squareInFront.Y) != null;

                    foreach (RoomItem current4 in roomItemForSquare)
                    {
                        double num3 = current4.Z - current.TotalHeight;

                        if (_rollerItemsMoved.Contains(current4.Id) || frontHasItem ||
                            !_room.GetGameMap().CanRollItemHere(squareInFront.X, squareInFront.Y) || !flag2 ||
                            !(current.Z < current4.Z) ||
                            _room.GetRoomUserManager().GetUserForSquare(squareInFront.X, squareInFront.Y) != null)
                            continue;

                        _rollerMessages.Add(UpdateItemOnRoller(current4, squareInFront, current.Id, num2 + num3));
                        _rollerItemsMoved.Add(current4.Id);
                    }

                    if (userForSquare != null && !userForSquare.IsWalking && flag2 && !flag3 &&
                        _room.GetGameMap().CanRollItemHere(squareInFront.X, squareInFront.Y) &&
                        _room.GetGameMap().GetFloorStatus(squareInFront) != 0 &&
                        !_rollerUsersMoved.Contains(userForSquare.HabboId))
                    {
                        _room.SendMessage(UpdateUserOnRoller(userForSquare, squareInFront, current.Id, nextZ));
                        _rollerUsersMoved.Add(userForSquare.HabboId);
                        _room.GetRoomUserManager().UpdateUserStatus(userForSquare, true);
                    }

                    continue;

                    IL_17C:
                    num2 += _room.GetGameMap().GetHeightForSquareFromData(squareInFront);

                    goto IL_192;
                }

                _roolerCycle = 0;

                return _rollerMessages;
            }

            {
                _roolerCycle++;
            }

            return new List<SimpleServerMessageBuffer>();
        }

        internal bool HasFurniByItemName(string name)
        {
            IEnumerable<RoomItem> element = FloorItems.Values.Where(i => i.GetBaseItem().Name == name);

            return element.Any();
        }
    }
}
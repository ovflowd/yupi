using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Yupi.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Catalogs;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pets;
using Yupi.Game.Pets.Enums;
using Yupi.Game.RoomBots;
using Yupi.Game.Rooms;
using Yupi.Game.Users.Data.Models;
using Yupi.Messages;
using Yupi.Messages.Enums;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Users.Inventory.Components
{
    /// <summary>
    ///     Class InventoryComponent.
    /// </summary>
    internal class InventoryComponent
    {
        /// <summary>
        ///     The _floor items
        /// </summary>
        private readonly HybridDictionary _floorItems;

        /// <summary>
        ///     The _inventory bots
        /// </summary>
        private readonly HybridDictionary _inventoryBots;

        /// <summary>
        ///     The _inventory pets
        /// </summary>
        private readonly HybridDictionary _inventoryPets;

        /// <summary>
        ///     The _m added items
        /// </summary>
        private readonly HybridDictionary _mAddedItems;

        /// <summary>
        ///     The _m removed items
        /// </summary>
        private readonly HybridDictionary _mRemovedItems;

        /// <summary>
        ///     The _wall items
        /// </summary>
        private readonly HybridDictionary _wallItems;

        /// <summary>
        ///     The _is updated
        /// </summary>
        private bool _isUpdated;

        /// <summary>
        ///     The _m client
        /// </summary>
        private GameClient _mClient;

        /// <summary>
        ///     The _user attatched
        /// </summary>
        private bool _userAttatched;

        /// <summary>
        ///     The user identifier
        /// </summary>
        internal uint UserId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InventoryComponent" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="client">The client.</param>
        /// <param name="userData">The user data.</param>
        internal InventoryComponent(uint userId, GameClient client, UserData userData)
        {
            _mClient = client;
            UserId = userId;
            _floorItems = new HybridDictionary();
            _wallItems = new HybridDictionary();
            SongDisks = new HybridDictionary();

            foreach (UserItem current in userData.Inventory)
            {
                if (current.BaseItem.InteractionType == Interaction.MusicDisc)
                    SongDisks.Add(current.Id, current);
                if (current.IsWallItem)
                    _wallItems.Add(current.Id, current);
                else
                    _floorItems.Add(current.Id, current);
            }

            _inventoryPets = new HybridDictionary();
            _inventoryBots = new HybridDictionary();
            _mAddedItems = new HybridDictionary();
            _mRemovedItems = new HybridDictionary();
            _isUpdated = false;

            foreach (KeyValuePair<uint, RoomBot> bot in userData.Bots)
                AddBot(bot.Value);

            foreach (KeyValuePair<uint, Pet> pets in userData.Pets)
                AddPets(pets.Value);
        }

        public int TotalItems => _floorItems.Count + _wallItems.Count + SongDisks.Count;

        /// <summary>
        ///     Gets a value indicating whether this instance is inactive.
        /// </summary>
        /// <value><c>true</c> if this instance is inactive; otherwise, <c>false</c>.</value>
        public bool IsInactive => !_userAttatched;

        /// <summary>
        ///     Gets a value indicating whether [needs update].
        /// </summary>
        /// <value><c>true</c> if [needs update]; otherwise, <c>false</c>.</value>
        internal bool NeedsUpdate => !_userAttatched && !_isUpdated;

        /// <summary>
        ///     Gets the song disks.
        /// </summary>
        /// <value>The song disks.</value>
        internal HybridDictionary SongDisks { get; }

        /// <summary>
        ///     Clears the items.
        /// </summary>
        internal void ClearItems()
        {
            UpdateItems(true);

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE room_id='0' AND user_id = {UserId}");

            _mAddedItems.Clear();
            _mRemovedItems.Clear();
            _floorItems.Clear();
            _wallItems.Clear();
            SongDisks.Clear();
            _inventoryPets.Clear();
            _isUpdated = true;

            _mClient.GetMessageHandler()
                .GetResponse()
                .Init(LibraryParser.OutgoingRequest("UpdateInventoryMessageComposer"));

            GetClient().GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Redeemcreditses the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        internal void Redeemcredits(GameClient session)
        {
            Room currentRoom = session.GetHabbo().CurrentRoom;

            if (currentRoom == null)
                return;

            DataTable table;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"SELECT id FROM items_rooms WHERE user_id={session.GetHabbo().Id} AND room_id='0'");
                table = commitableQueryReactor.GetTable();
            }

            foreach (DataRow dataRow in table.Rows)
            {
                UserItem item = GetItem(Convert.ToUInt32(dataRow[0]));

                if (item == null || (!item.BaseItem.Name.StartsWith("CF_") && !item.BaseItem.Name.StartsWith("CFC_")))
                    continue;

                string[] array = item.BaseItem.Name.Split('_');
                uint num = uint.Parse(array[1]);

                using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryreactor2.RunFastQuery($"DELETE FROM items_rooms WHERE id={item.Id} LIMIT 1");

                currentRoom.GetRoomItemHandler().RemoveItem(item.Id);

                RemoveItem(item.Id, false);

                if (num <= 0)
                    continue;

                session.GetHabbo().Credits += num;
                session.GetHabbo().UpdateCreditsBalance();
            }
        }

        /// <summary>
        ///     Sets the state of the active.
        /// </summary>
        /// <param name="client">The client.</param>
        internal void SetActiveState(GameClient client)
        {
            _mClient = client;
            _userAttatched = true;
        }

        /// <summary>
        ///     Sets the state of the idle.
        /// </summary>
        internal void SetIdleState()
        {
            _userAttatched = false;
            _mClient = null;
        }

        /// <summary>
        ///     Gets the pet.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Pet.</returns>
        internal Pet GetPet(uint id)
        {
            return _inventoryPets.Contains(id) ? _inventoryPets[id] as Pet : null;
        }

        /// <summary>
        ///     Removes the pet.
        /// </summary>
        /// <param name="petId">The pet identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool RemovePet(uint petId)
        {
            _isUpdated = false;
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RemovePetFromInventoryComposer"));
            serverMessage.AppendInteger(petId);
            GetClient().SendMessage(serverMessage);
            _inventoryPets.Remove(petId);
            return true;
        }

        /// <summary>
        ///     Moves the pet to room.
        /// </summary>
        /// <param name="petId">The pet identifier.</param>
        internal void MovePetToRoom(uint petId)
        {
            _isUpdated = false;
            RemovePet(petId);
        }

        /// <summary>
        ///     Adds the pet.
        /// </summary>
        /// <param name="pet">The pet.</param>
        internal void AddPet(Pet pet)
        {
            _isUpdated = false;

            if (pet == null || _inventoryPets.Contains(pet.PetId))
                return;

            pet.PlacedInRoom = false;
            pet.RoomId = 0u;

            _inventoryPets.Add(pet.PetId, pet);

            SerializePetInventory();
        }

        /// <summary>
        ///     Loads the inventory.
        /// </summary>
        internal void LoadInventory()
        {
            _floorItems.Clear();
            _wallItems.Clear();

            DataTable table;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "SELECT * FROM items_rooms WHERE user_id=@userid AND room_id='0' LIMIT 8000;");
                commitableQueryReactor.AddParameter("userid", (int) UserId);

                table = commitableQueryReactor.GetTable();
            }

            foreach (DataRow dataRow in table.Rows)
            {
                uint id = Convert.ToUInt32(dataRow["id"]);
                string itemName = dataRow["item_name"].ToString();

                if (!Yupi.GetGame().GetItemManager().ContainsItemByName(itemName))
                    continue;

                string extraData;

                if (!DBNull.Value.Equals(dataRow[4]))
                    extraData = (string) dataRow[4];
                else
                    extraData = string.Empty;

                uint group = Convert.ToUInt32(dataRow["group_id"]);

                string songCode;

                if (!DBNull.Value.Equals(dataRow["songcode"]))
                    songCode = (string) dataRow["songcode"];
                else
                    songCode = string.Empty;

                UserItem userItem = new UserItem(id, itemName, extraData, group, songCode);

                if (userItem.BaseItem.InteractionType == Interaction.MusicDisc && !SongDisks.Contains(id))
                    SongDisks.Add(id, userItem);

                if (userItem.IsWallItem)
                {
                    if (!_wallItems.Contains(id))
                        _wallItems.Add(id, userItem);
                }
                else if (!_floorItems.Contains(id))
                    _floorItems.Add(id, userItem);
            }

            //SongDisks.Clear();
            _inventoryPets.Clear();
            _inventoryBots.Clear();

            using (IQueryAdapter commitableQueryReactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor2.SetQuery($"SELECT * FROM bots_data WHERE user_id = {UserId} AND room_id = 0");

                DataTable table2 = commitableQueryReactor2.GetTable();

                if (table2 == null)
                    return;

                foreach (DataRow botRow in table2.Rows)
                {
                    if ((string) botRow["ai_type"] == "generic")
                        AddBot(BotManager.GenerateBotFromRow(botRow));
                }

                commitableQueryReactor2.SetQuery($"SELECT * FROM pets_data WHERE user_id = {UserId} AND room_id = 0");

                DataTable table3 = commitableQueryReactor2.GetTable();

                if (table3 == null)
                    return;

                foreach (DataRow petRow in table3.Rows)
                {
                    if ((string) petRow["ai_type"] == "pet")
                    {
                        Pet pet = CatalogManager.GeneratePetFromRow(petRow);

                        if (_inventoryPets.Contains(pet.PetId))
                            _inventoryPets.Remove(pet.PetId);

                        _inventoryPets.Add(pet.PetId, pet);
                    }
                }
            }
        }

        /// <summary>
        ///     Updates the items.
        /// </summary>
        /// <param name="fromDatabase">if set to <c>true</c> [from database].</param>
        internal void UpdateItems(bool fromDatabase)
        {
            if (fromDatabase)
            {
                RunDbUpdate();
                LoadInventory();
            }

            _mClient.GetMessageHandler()
                .GetResponse()
                .Init(LibraryParser.OutgoingRequest("UpdateInventoryMessageComposer"));

            _mClient.GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Gets the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>UserItem.</returns>
        internal UserItem GetItem(uint id)
        {
            _isUpdated = false;

            if (_floorItems.Contains(id))
                return (UserItem) _floorItems[id];

            if (_wallItems.Contains(id))
                return (UserItem) _wallItems[id];

            return null;
        }

        internal bool HasBaseItem(uint id)
        {
            return
                _floorItems.Values.Cast<UserItem>().Any(item => item?.BaseItem != null && item.BaseItem.ItemId == id) ||
                _wallItems.Values.Cast<UserItem>().Any(item => item?.BaseItem != null && item.BaseItem.ItemId == id);
        }

        /// <summary>
        ///     Adds the bot.
        /// </summary>
        /// <param name="bot">The bot.</param>
        internal void AddBot(RoomBot bot)
        {
            _isUpdated = false;

            if (bot == null || _inventoryBots.Contains(bot.BotId))
                return;

            bot.RoomId = 0u;

            _inventoryBots.Add(bot.BotId, bot);
        }

        internal void AddPets(Pet bot)
        {
            _isUpdated = false;

            if (bot == null || _inventoryPets.Contains(bot.PetId))
                return;

            bot.RoomId = 0u;

            _inventoryPets.Add(bot.PetId, bot);
        }

        /// <summary>
        ///     Gets the bot.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>RoomBot.</returns>
        internal RoomBot GetBot(uint id)
        {
            return _inventoryBots.Contains(id) ? _inventoryBots[id] as RoomBot : null;
        }

        /// <summary>
        ///     Removes the bot.
        /// </summary>
        /// <param name="petId">The pet identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool RemoveBot(uint petId)
        {
            _isUpdated = false;

            if (_inventoryBots.Contains(petId))
                _inventoryBots.Remove(petId);

            return true;
        }

        /// <summary>
        ///     Moves the bot to room.
        /// </summary>
        /// <param name="petId">The pet identifier.</param>
        internal void MoveBotToRoom(uint petId)
        {
            _isUpdated = false;
            RemoveBot(petId);
        }

        /// <summary>
        ///     Adds the new item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseName">The base item.</param>
        /// <param name="extraData">The extra data.</param>
        /// <param name="thGroup">The thGroup.</param>
        /// <param name="insert">if set to <c>true</c> [insert].</param>
        /// <param name="fromRoom">if set to <c>true</c> [from room].</param>
        /// <param name="limno">The limno.</param>
        /// <param name="limtot">The limtot.</param>
        /// <param name="songCode">The song code.</param>
        /// <returns>UserItem.</returns>
        internal UserItem AddNewItem(uint id, string baseName, string extraData, uint thGroup, bool insert, bool fromRoom, uint limno, uint limtot, string songCode = "")
        {
            _isUpdated = false;

            if (insert)
            {
                if (fromRoom)
                {
                    using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        commitableQueryReactor.RunFastQuery("UPDATE items_rooms SET user_id = '" + UserId +
                                                            "', room_id= '0' WHERE (id='" + id + "')");
                }
                else
                {
                    using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        commitableQueryReactor.SetQuery(
                            $"INSERT INTO items_rooms (item_name, user_id, group_id) VALUES ('{baseName}', '{UserId}', '{thGroup}');");

                        if (id == 0)
                            id = (uint) commitableQueryReactor.InsertQuery();

                        SendNewItems(id);

                        if (!string.IsNullOrEmpty(extraData))
                        {
                            commitableQueryReactor.SetQuery(
                                "UPDATE items_rooms SET extra_data = @extraData WHERE id = " + id);
                            commitableQueryReactor.AddParameter("extraData", extraData);
                            commitableQueryReactor.RunQuery();
                        }

                        if (limno > 0)
                            commitableQueryReactor.RunFastQuery(
                                $"INSERT INTO items_limited VALUES ('{id}', '{limno}', '{limtot}');");

                        if (!string.IsNullOrEmpty(songCode))
                        {
                            commitableQueryReactor.SetQuery(
                                $"UPDATE items_rooms SET songcode='{songCode}' WHERE id='{id}' LIMIT 1");
                            commitableQueryReactor.RunQuery();
                        }
                    }
                }
            }

            if (id == 0)
                return null;

            UserItem userItem = new UserItem(id, baseName, extraData, thGroup, songCode);

            if (UserHoldsItem(id))
                RemoveItem(id, false);

            if (userItem.BaseItem.InteractionType == Interaction.MusicDisc)
                SongDisks.Add(userItem.Id, userItem);

            if (userItem.IsWallItem)
                _wallItems.Add(userItem.Id, userItem);
            else
                _floorItems.Add(userItem.Id, userItem);

            if (_mRemovedItems.Contains(id))
                _mRemovedItems.Remove(id);

            if (!_mAddedItems.Contains(id))
                _mAddedItems.Add(id, userItem);

            return userItem;
        }

        /// <summary>
        ///     Removes the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="placedInroom">if set to <c>true</c> [placed inroom].</param>
        internal void RemoveItem(uint id, bool placedInroom)
        {
            if (GetClient() == null || GetClient().GetHabbo() == null ||
                GetClient().GetHabbo().GetInventoryComponent() == null)
                GetClient().Disconnect("user null RemoveItem");

            _isUpdated = false;
            GetClient()
                .GetMessageHandler()
                .GetResponse()
                .Init(LibraryParser.OutgoingRequest("RemoveInventoryObjectMessageComposer"));

            GetClient().GetMessageHandler().GetResponse().AppendInteger(id);
            //this.GetClient().GetMessageHandler().GetResponse().AppendInt32(Convert.ToInt32(this.GetClient().GetHabbo().Id));

            GetClient().GetMessageHandler().SendResponse();
            if (_mAddedItems.Contains(id))
                _mAddedItems.Remove(id);

            if (_mRemovedItems.Contains(id))
                return;

            UserItem item = GetClient().GetHabbo().GetInventoryComponent().GetItem(id);

            SongDisks.Remove(id);
            _floorItems.Remove(id);
            _wallItems.Remove(id);
            _mRemovedItems.Add(id, item);
        }

        /// <summary>
        ///     Serializes the floor item inventory.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeFloorItemInventory()
        {
            int i = _floorItems.Count + SongDisks.Count + _wallItems.Count;

            if (i > 2800)
                _mClient.SendMessage(StaticMessage.AdviceMaxItems);

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("LoadInventoryMessageComposer"));
            serverMessage.AppendInteger(1);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(i > 2800 ? 2800 : i);

            int inc = 0;

            foreach (UserItem userItem in _floorItems.Values)
            {
                if (inc == 2800)
                    return serverMessage;

                inc++;

                userItem.SerializeFloor(serverMessage, true);
            }

            foreach (UserItem userItem in _wallItems.Values)
            {
                if (inc == 2800)
                    return serverMessage;

                inc++;

                userItem.SerializeWall(serverMessage, true);
            }

            foreach (UserItem userItem in SongDisks.Values)
            {
                if (inc == 2800)
                    return serverMessage;

                inc++;

                userItem.SerializeFloor(serverMessage, true);
            }

            return serverMessage;
        }

        /// <summary>
        ///     Serializes the wall item inventory.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeWallItemInventory()
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("LoadInventoryMessageComposer"));
            serverMessage.AppendString("I");
            serverMessage.AppendInteger(1);
            serverMessage.AppendInteger(1);
            serverMessage.AppendInteger(_wallItems.Count);
            foreach (UserItem userItem in _wallItems.Values)
                userItem.SerializeWall(serverMessage, true);
            return serverMessage;
        }

        /// <summary>
        ///     Serializes the pet inventory.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializePetInventory()
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("PetInventoryMessageComposer"));
            serverMessage.AppendInteger(1);
            serverMessage.AppendInteger(1);
            serverMessage.AppendInteger(_inventoryPets.Count);

            foreach (Pet current in _inventoryPets.Values)
                current.SerializeInventory(serverMessage);

            return serverMessage;
        }

        /// <summary>
        ///     Serializes the bot inventory.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeBotInventory()
        {
            ServerMessage serverMessage = new ServerMessage();
            serverMessage.Init(LibraryParser.OutgoingRequest("BotInventoryMessageComposer"));

            serverMessage.AppendInteger(_inventoryBots.Count);
            foreach (RoomBot current in _inventoryBots.Values)
            {
                serverMessage.AppendInteger(current.BotId);
                serverMessage.AppendString(current.Name);
                serverMessage.AppendString(current.Motto);
                serverMessage.AppendString("m");
                serverMessage.AppendString(current.Look);
            }
            return serverMessage;
        }

        /// <summary>
        ///     Adds the item array.
        /// </summary>
        /// <param name="roomItemList">The room item list.</param>
        internal void AddItemArray(List<RoomItem> roomItemList)
        {
            foreach (RoomItem current in roomItemList)
                AddItem(current);
        }

        /// <summary>
        ///     Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        internal void AddItem(RoomItem item)
        {
            AddNewItem(item.Id, item.BaseName, item.ExtraData, item.GroupId, true, true, 0, 0, item.SongCode);
        }

        /// <summary>
        ///     Runs the cycle update.
        /// </summary>
        internal void RunCycleUpdate()
        {
            _isUpdated = true;
            RunDbUpdate();
        }

        /// <summary>
        ///     Runs the database update.
        /// </summary>
        internal void RunDbUpdate()
        {
            if (_mRemovedItems.Count <= 0 && _mAddedItems.Count <= 0 && _inventoryPets.Count <= 0)
                return;

            if (_mAddedItems.Count > 0)
            {
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    foreach (UserItem userItem in _mAddedItems.Values)
                        commitableQueryReactor.RunFastQuery($"UPDATE items_rooms SET user_id='{UserId}', room_id=0 WHERE id='{userItem.Id}'");
                }

                _mAddedItems.Clear();
            }

            if (_mRemovedItems.Count > 0)
            {
                foreach (UserItem userItem2 in _mRemovedItems.Values)
                {
                    using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        GetClient().GetHabbo().CurrentRoom.GetRoomItemHandler().SaveFurniture(commitableQueryReactor);

                    if (SongDisks.Contains(userItem2.Id))
                        SongDisks.Remove(userItem2.Id);
                }

                _mRemovedItems.Clear();
            }
        }

        /// <summary>
        ///     Serializes the music discs.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeMusicDiscs()
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("SongsLibraryMessageComposer"));

            serverMessage.AppendInteger(SongDisks.Count);

            foreach (
                UserItem current in
                    from x in _floorItems.Values.OfType<UserItem>()
                    where x.BaseItem.InteractionType == Interaction.MusicDisc
                    select x)
            {
                uint i;

                uint.TryParse(current.ExtraData, out i);

                serverMessage.AppendInteger(current.Id);
                serverMessage.AppendInteger(i);
            }

            return serverMessage;
        }

        /// <summary>
        ///     Gets the pets.
        /// </summary>
        /// <returns>List&lt;Pet&gt;.</returns>
        internal List<Pet> GetPets()
        {
            return _inventoryPets.Values.Cast<Pet>().ToList();
        }

        /// <summary>
        ///     Sends the floor inventory update.
        /// </summary>
        internal void SendFloorInventoryUpdate()
        {
            _mClient.SendMessage(SerializeFloorItemInventory());
        }

        /// <summary>
        ///     Sends the new items.
        /// </summary>
        /// <param name="id">The identifier.</param>
        internal void SendNewItems(uint id)
        {
            ServerMessage serverMessage = new ServerMessage();
            serverMessage.Init(LibraryParser.OutgoingRequest("NewInventoryObjectMessageComposer"));
            serverMessage.AppendInteger(1);
            serverMessage.AppendInteger(1);
            serverMessage.AppendInteger(1);
            serverMessage.AppendInteger(id);
            _mClient.SendMessage(serverMessage);
        }

        /// <summary>
        ///     Users the holds item.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool UserHoldsItem(uint itemId)
        {
            return SongDisks.Contains(itemId) || _floorItems.Contains(itemId) || _wallItems.Contains(itemId);
        }

        /// <summary>
        ///     Gets the client.
        /// </summary>
        /// <returns>GameClient.</returns>
        private GameClient GetClient()
        {
            return Yupi.GetGame().GetClientManager().GetClientByUserId(UserId);
        }
    }
}
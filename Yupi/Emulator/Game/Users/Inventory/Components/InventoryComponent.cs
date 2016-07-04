using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Catalogs;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.RoomBots;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Users.Data.Models;


namespace Yupi.Emulator.Game.Users.Inventory.Components
{
    /// <summary>
    ///     Class InventoryComponent.
    /// </summary>
     public class InventoryComponent
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
     public uint UserId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InventoryComponent" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="client">The client.</param>
        /// <param name="userData">The user data.</param>
     public InventoryComponent(uint userId, GameClient client, UserData userData)
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
     public bool NeedsUpdate => !_userAttatched && !_isUpdated;

        /// <summary>
        ///     Gets the song disks.
        /// </summary>
        /// <value>The song disks.</value>
     public HybridDictionary SongDisks { get; }

        /// <summary>
        ///     Clears the items.
        /// </summary>
     public void ClearItems()
        {
            UpdateItems(true);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE room_id='0' AND user_id = {UserId}");

            _mAddedItems.Clear();
            _mRemovedItems.Clear();
            _floorItems.Clear();
            _wallItems.Clear();
            SongDisks.Clear();
            _inventoryPets.Clear();
            _isUpdated = true;

            _mClient.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.OutgoingHandler("UpdateInventoryMessageComposer"));

            GetClient().GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Redeemcreditses the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
     public void Redeemcredits(GameClient session)
        {
            Room currentRoom = session.GetHabbo().CurrentRoom;

            if (currentRoom == null)
                return;

            DataTable table;
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    $"SELECT id FROM items_rooms WHERE user_id={session.GetHabbo().Id} AND room_id='0'");
                table = queryReactor.GetTable();
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
     public void SetActiveState(GameClient client)
        {
            _mClient = client;
            _userAttatched = true;
        }

        /// <summary>
        ///     Sets the state of the idle.
        /// </summary>
     public void SetIdleState()
        {
            _userAttatched = false;
            _mClient = null;
        }

        /// <summary>
        ///     Gets the pet.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Pet.</returns>
     public Pet GetPet(uint id)
        {
            return _inventoryPets.Contains(id) ? _inventoryPets[id] as Pet : null;
        }

        /// <summary>
        ///     Removes the pet.
        /// </summary>
        /// <param name="petId">The pet identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
     public bool RemovePet(uint petId)
        {
            _isUpdated = false;
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RemovePetFromInventoryComposer"));
            simpleServerMessageBuffer.AppendInteger(petId);
            GetClient().SendMessage(simpleServerMessageBuffer);
            _inventoryPets.Remove(petId);
            return true;
        }

        /// <summary>
        ///     Moves the pet to room.
        /// </summary>
        /// <param name="petId">The pet identifier.</param>
     public void MovePetToRoom(uint petId)
        {
            _isUpdated = false;
            RemovePet(petId);
        }

        /// <summary>
        ///     Adds the pet.
        /// </summary>
        /// <param name="pet">The pet.</param>
     public void AddPet(Pet pet)
        {
            _isUpdated = false;

            if (pet == null || _inventoryPets.Contains(pet.PetId))
                return;

            pet.PlacedInRoom = false;
            pet.RoomId = 0u;

            _inventoryPets.Add(pet.PetId, pet);
        }

        /// <summary>
        ///     Loads the inventory.
        /// </summary>
     public void LoadInventory()
        {
            _floorItems.Clear();
            _wallItems.Clear();

            DataTable table;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    "SELECT * FROM items_rooms WHERE user_id=@userid AND room_id='0' LIMIT 8000;");
                queryReactor.AddParameter("userid", (int) UserId);

                table = queryReactor.GetTable();
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

            using (IQueryAdapter queryReactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor2.SetQuery($"SELECT * FROM bots_data WHERE user_id = {UserId} AND room_id = 0");

                DataTable table2 = queryReactor2.GetTable();

                if (table2 == null)
                    return;

                foreach (DataRow botRow in table2.Rows)
                {
                    if ((string) botRow["ai_type"] == "generic")
                        AddBot(BotManager.GenerateBotFromRow(botRow));
                }

                queryReactor2.SetQuery($"SELECT * FROM pets_data WHERE user_id = {UserId} AND room_id = 0");

                DataTable table3 = queryReactor2.GetTable();

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
     public void UpdateItems(bool fromDatabase)
        {
            if (fromDatabase)
            {
                RunDbUpdate();
                LoadInventory();
            }

            _mClient.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.OutgoingHandler("UpdateInventoryMessageComposer"));

            _mClient.GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Gets the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>UserItem.</returns>
     public UserItem GetItem(uint id)
        {
            _isUpdated = false;

            if (_floorItems.Contains(id))
                return (UserItem) _floorItems[id];

            if (_wallItems.Contains(id))
                return (UserItem) _wallItems[id];

            return null;
        }

     public bool HasBaseItem(uint id)
        {
            return
                _floorItems.Values.Cast<UserItem>().Any(item => item?.BaseItem != null && item.BaseItem.ItemId == id) ||
                _wallItems.Values.Cast<UserItem>().Any(item => item?.BaseItem != null && item.BaseItem.ItemId == id);
        }

        /// <summary>
        ///     Adds the bot.
        /// </summary>
        /// <param name="bot">The bot.</param>
     public void AddBot(RoomBot bot)
        {
            _isUpdated = false;

            if (bot == null || _inventoryBots.Contains(bot.BotId))
                return;

            bot.RoomId = 0u;

            _inventoryBots.Add(bot.BotId, bot);
        }

     public void AddPets(Pet bot)
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
     public RoomBot GetBot(uint id)
        {
            return _inventoryBots.Contains(id) ? _inventoryBots[id] as RoomBot : null;
        }

        /// <summary>
        ///     Removes the bot.
        /// </summary>
        /// <param name="petId">The pet identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
     public bool RemoveBot(uint petId)
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
     public void MoveBotToRoom(uint petId)
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
     public UserItem AddNewItem(uint id, string baseName, string extraData, uint thGroup, bool insert, bool fromRoom, uint limno, uint limtot, string songCode = "")
        {
            _isUpdated = false;

            if (insert)
            {
                if (fromRoom)
                {
                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        queryReactor.RunFastQuery("UPDATE items_rooms SET user_id = '" + UserId + "', room_id= '0' WHERE (id='" + id + "')");
                }
                else
                {
                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery($"INSERT INTO items_rooms (item_name, user_id, group_id) VALUES ('{baseName}', '{UserId}', '{thGroup}');");

                        if (id == 0)
                            id = (uint) queryReactor.InsertQuery();

                        SendNewItems(id);

                        if (!string.IsNullOrEmpty(extraData))
                        {
                            queryReactor.SetQuery("UPDATE items_rooms SET extra_data = @extraData WHERE id = " + id);
                            queryReactor.AddParameter("extraData", extraData);

                            queryReactor.RunQuery();
                        }

                        if (limno > 0)
                            queryReactor.RunFastQuery($"INSERT INTO items_limited VALUES ('{id}', '{limno}', '{limtot}');");

                        if (!string.IsNullOrEmpty(songCode))
                            queryReactor.RunFastQuery($"UPDATE items_rooms SET songcode='{songCode}' WHERE id='{id}' LIMIT 1");
                    }
                }
            }

            if (id == 0)
                return null;

            UserItem userItem = new UserItem(id, baseName, extraData, thGroup, songCode);

            if (UserHoldsItem(id))
                RemoveItem(id, false);

            if (userItem.BaseItem.InteractionType == Interaction.MusicDisc && !SongDisks.Contains(userItem.Id))
                SongDisks.Add(userItem.Id, userItem);

            if (userItem.IsWallItem && !_wallItems.Contains(userItem.Id))
                _wallItems.Add(userItem.Id, userItem);
            else if(!userItem.IsWallItem && !_floorItems.Contains(userItem.Id))
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
     public void RemoveItem(uint id, bool placedInroom)
        {
            if (GetClient() == null || GetClient().GetHabbo() == null || GetClient().GetHabbo().GetInventoryComponent() == null) GetClient().Disconnect("Probably Packet Logger User.", true);

            _isUpdated = false;

            GetClient().GetMessageHandler().GetResponse().Init(PacketLibraryManager.OutgoingHandler("RemoveInventoryObjectMessageComposer"));

            GetClient().GetMessageHandler().GetResponse().AppendInteger(id);
            //this.GetClientByAddress().GetMessageHandler().GetResponse().AppendInt32(Convert.ToInt32(this.GetClientByAddress().GetHabbo().Id));

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
        ///     Adds the item array.
        /// </summary>
        /// <param name="roomItemList">The room item list.</param>
     public void AddItemArray(List<RoomItem> roomItemList)
        {
            foreach (RoomItem current in roomItemList)
                AddItem(current);
        }

        /// <summary>
        ///     Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
     public void AddItem(RoomItem item)
        {
            AddNewItem(item.Id, item.BaseName, item.ExtraData, item.GroupId, true, true, 0, 0, item.SongCode);
        }

        /// <summary>
        ///     Runs the cycle update.
        /// </summary>
     public void RunCycleUpdate()
        {
            _isUpdated = true;
            RunDbUpdate();
        }

        /// <summary>
        ///     Runs the database update.
        /// </summary>
     public void RunDbUpdate()
        {
            if (_mRemovedItems?.Count <= 0 && _mAddedItems?.Count <= 0 && _inventoryPets?.Count <= 0)
                return;

            if (_mAddedItems?.Count > 0)
            {
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    foreach (UserItem userItem in _mAddedItems?.Values)
                        queryReactor.RunFastQuery(
                            $"UPDATE items_rooms SET user_id = {UserId}, room_id = 0 WHERE id = {userItem.Id}");
                }

                _mAddedItems?.Clear();
            }
        }

        /// <summary>
        ///     Gets the pets.
        /// </summary>
        /// <returns>List&lt;Pet&gt;.</returns>
     public List<Pet> GetPets()
        {
            return _inventoryPets.Values.Cast<Pet>().ToList();
        }

        /// <summary>
        ///     Sends the new items.
        /// </summary>
        /// <param name="id">The identifier.</param>
     public void SendNewItems(uint id)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer();
            simpleServerMessageBuffer.Init(PacketLibraryManager.OutgoingHandler("NewInventoryObjectMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendInteger(id);
            _mClient.SendMessage(simpleServerMessageBuffer);
        }

        /// <summary>
        ///     Users the holds item.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool UserHoldsItem(uint itemId) => SongDisks.Contains(itemId) || _floorItems.Contains(itemId) || _wallItems.Contains(itemId);

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
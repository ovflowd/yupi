using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Yupi.Core.Io;
using Yupi.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Browser.Models;
using Yupi.Game.Events;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Rooms
{
    /// <summary>
    ///     Class RoomManager.
    /// </summary>
    internal class RoomManager
    {
        /// <summary>
        ///     The _active rooms
        /// </summary>
        private readonly Dictionary<RoomData, uint> _activeRooms;

        /// <summary>
        ///     The _active rooms add queue
        /// </summary>
        private readonly Queue _activeRoomsAddQueue;

        /// <summary>
        ///     The _active rooms update queue
        /// </summary>
        private readonly Queue _activeRoomsUpdateQueue;

        /// <summary>
        ///     The _event manager
        /// </summary>
        private readonly EventManager _eventManager;

        /// <summary>
        ///     The _room models
        /// </summary>
        private readonly HybridDictionary _roomModels;

        /// <summary>
        ///     The _voted rooms
        /// </summary>
        private readonly Dictionary<RoomData, int> _votedRooms;

        /// <summary>
        ///     The _voted rooms add queue
        /// </summary>
        private readonly Queue _votedRoomsAddQueue;

        /// <summary>
        ///     The _voted rooms remove queue
        /// </summary>
        private readonly Queue _votedRoomsRemoveQueue;

        internal readonly ConcurrentDictionary<uint, RoomData> LoadedRoomData;

        private RoomCompetitionManager _competitionManager;

        /// <summary>
        ///     The _ordered active rooms
        /// </summary>
        private IEnumerable<KeyValuePair<RoomData, uint>> _orderedActiveRooms;

        /// <summary>
        ///     The _ordered voted rooms
        /// </summary>
        private IEnumerable<KeyValuePair<RoomData, int>> _orderedVotedRooms;

        /// <summary>
        ///     The active rooms remove queue
        /// </summary>
        public Queue ActiveRoomsRemoveQueue;

        /// <summary>
        ///     The loaded rooms
        /// </summary>
        internal ConcurrentDictionary<uint, Room> LoadedRooms;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomManager" /> class.
        /// </summary>
        internal RoomManager()
        {
            LoadedRooms = new ConcurrentDictionary<uint, Room>();
            _roomModels = new HybridDictionary();
            LoadedRoomData = new ConcurrentDictionary<uint, RoomData>();
            _votedRooms = new Dictionary<RoomData, int>();
            _activeRooms = new Dictionary<RoomData, uint>();
            _votedRoomsRemoveQueue = new Queue();
            _votedRoomsAddQueue = new Queue();
            ActiveRoomsRemoveQueue = new Queue();
            _activeRoomsUpdateQueue = new Queue();
            _activeRoomsAddQueue = new Queue();
            _eventManager = new EventManager();
        }

        /// <summary>
        ///     Gets the loaded rooms count.
        /// </summary>
        /// <value>The loaded rooms count.</value>
        internal int LoadedRoomsCount => LoadedRooms.Count;

        internal RoomCompetitionManager GetCompetitionManager() => _competitionManager;

        internal void LoadCompetitionManager() => _competitionManager = new RoomCompetitionManager();

        /// <summary>
        ///     Gets the active rooms.
        /// </summary>
        /// <returns>KeyValuePair&lt;RoomData, System.UInt32&gt;[].</returns>
        internal KeyValuePair<RoomData, uint>[] GetActiveRooms() => _orderedActiveRooms?.ToArray();

        /// <summary>
        ///     Gets the voted rooms.
        /// </summary>
        /// <returns>KeyValuePair&lt;RoomData, System.Int32&gt;[].</returns>
        internal KeyValuePair<RoomData, int>[] GetVotedRooms() => _orderedVotedRooms?.ToArray();

        /// <summary>
        ///     Gets the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <returns>RoomModel.</returns>
        internal RoomModel GetModel(string model, uint roomId)
        {
            if (model == "custom" && _roomModels.Contains($"custom_{roomId}"))
                return (RoomModel) _roomModels[$"custom_{roomId}"];
            if (_roomModels.Contains(model))
                return (RoomModel) _roomModels[model];
            return null;
        }

        /// <summary>
        ///     Generates the nullable room data.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <returns>RoomData.</returns>
        internal RoomData GenerateNullableRoomData(uint roomId)
        {
            if (GenerateRoomData(roomId) != null)
                return GenerateRoomData(roomId);
            RoomData roomData = new RoomData();
            roomData.FillNull(roomId);
            return roomData;
        }

        private bool IsRoomLoaded(uint roomId)
        {
            return LoadedRooms.ContainsKey(roomId);
        }

        /// <summary>
        ///     Generates the room data.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <returns>RoomData.</returns>
        internal RoomData GenerateRoomData(uint roomId)
        {
            if (LoadedRoomData.ContainsKey(roomId))
            {
                LoadedRoomData[roomId].LastUsed = DateTime.Now;
                return LoadedRoomData[roomId];
            }

            if (IsRoomLoaded(roomId))
                return GetRoom(roomId).RoomData;

            RoomData roomData = new RoomData();
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery($"SELECT * FROM rooms_data WHERE id = {roomId} LIMIT 1");

                DataRow dataRow = commitableQueryReactor.GetRow();
                if (dataRow == null)
                    return null;

                roomData.Fill(dataRow);
                LoadedRoomData.TryAdd(roomId, roomData);
            }

            return roomData;
        }

        /// <summary>
        ///     Gets the event rooms.
        /// </summary>
        /// <returns>KeyValuePair&lt;RoomData, System.UInt32&gt;[].</returns>
        internal KeyValuePair<RoomData, uint>[] GetEventRooms()
        {
            return _eventManager.GetRooms();
        }

        /// <summary>
        ///     Loads the room.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="forceLoad"></param>
        /// <returns>Room.</returns>
        internal Room LoadRoom(uint id, bool forceLoad = false)
        {
            if (IsRoomLoaded(id))
                return GetRoom(id);

            RoomData roomData = GenerateRoomData(id);

            if (roomData == null)
                return null;

            Room room = new Room();

            LoadedRooms.AddOrUpdate(id, room, (key, value) => room);

            room.Start(roomData, forceLoad);

            Writer.WriteLine($"Room #{id} was loaded", "Yupi.Rooms", ConsoleColor.DarkCyan);

            room.InitBots();
            room.InitPets();
            return room;
        }

        internal void RemoveRoomData(uint id)
        {
            RoomData dataJunk;
            LoadedRoomData.TryRemove(id, out dataJunk);
        }

        /// <summary>
        ///     Fetches the room data.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="dRow">The d row.</param>
        /// <returns>RoomData.</returns>
        internal RoomData FetchRoomData(uint roomId, DataRow dRow)
        {
            if (LoadedRoomData.ContainsKey(roomId))
            {
                LoadedRoomData[roomId].LastUsed = DateTime.Now;

                return LoadedRoomData[roomId];
            }

            RoomData roomData = new RoomData();
            roomData.Fill(dRow);
            LoadedRoomData.TryAdd(roomId, roomData);

            return roomData;
        }

        /// <summary>
        ///     Gets the room.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <returns>Room.</returns>
        internal Room GetRoom(uint roomId)
        {
            Room result;

            return LoadedRooms.TryGetValue(roomId, out result) ? result : null;
        }

        /// <summary>
        ///     Creates the room.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="name">The name.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="model">The model.</param>
        /// <param name="category">The category.</param>
        /// <param name="maxVisitors">The maximum visitors.</param>
        /// <param name="tradeState">State of the trade.</param>
        /// <returns>RoomData.</returns>
        internal RoomData CreateRoom(GameClient session, string name, string desc, string model, int category,
            int maxVisitors, int tradeState)
        {
            if (!_roomModels.Contains(model))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("user_room_model_error"));

                return null;
            }

            uint roomId;

            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("INSERT INTO rooms_data (roomtype,caption,description,owner,model_name,category,users_max,trade_state) VALUES ('private',@caption,@desc,@UserId,@model,@cat,@usmax,@tstate)");
                dbClient.AddParameter("caption", name);
                dbClient.AddParameter("desc", desc);
                dbClient.AddParameter("UserId", session.GetHabbo().Id);
                dbClient.AddParameter("model", model);
                dbClient.AddParameter("cat", category);
                dbClient.AddParameter("usmax", maxVisitors);
                dbClient.AddParameter("tstate", tradeState.ToString());
                roomId = (uint) dbClient.InsertQuery();
            }

            RoomData data = GenerateRoomData(roomId);

            if (data == null)
                return null;

            session.GetHabbo().UsersRooms.Add(data);

            return data;
        }

        /// <summary>
        ///     Initializes the voted rooms.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void InitVotedRooms(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT * FROM rooms_data WHERE score > 0 AND roomtype = 'private' ORDER BY score DESC LIMIT 40");
            DataTable table = dbClient.GetTable();

            foreach (
                RoomData data in
                    from DataRow dataRow in table.Rows select FetchRoomData(Convert.ToUInt32(dataRow["id"]), dataRow))
                QueueVoteAdd(data);
        }

        /// <summary>
        ///     Loads the models.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        /// <param name="loadedModel">The loaded model.</param>
        internal void LoadModels(IQueryAdapter dbClient, out uint loadedModel)
        {
            LoadModels(dbClient);
            loadedModel = (uint) _roomModels.Count;
        }

        /// <summary>
        ///     Loads the models.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void LoadModels(IQueryAdapter dbClient)
        {
            _roomModels.Clear();

            dbClient.SetQuery("SELECT * FROM rooms_models");
            DataTable table = dbClient.GetTable();

            if (table == null)
                return;

            foreach (DataRow dataRow in table.Rows)
            {
                string key = (string) dataRow["id"];

                if (key.StartsWith("model_floorplan_"))
                    continue;

                string staticFurniMap = (string) dataRow["public_items"];

                _roomModels.Add(key,
                    new RoomModel((int) dataRow["door_x"], (int) dataRow["door_y"], (double) dataRow["door_z"],
                        (int) dataRow["door_dir"], (string) dataRow["heightmap"], staticFurniMap,
                        Yupi.EnumToBool(dataRow["club_only"].ToString()), (string) dataRow["poolmap"]));
            }

            dbClient.SetQuery("SELECT * FROM rooms_models_customs");

            DataTable dataCustom = dbClient.GetTable();

            if (dataCustom == null) return;

            foreach (DataRow row in dataCustom.Rows)
            {
                string modelName = $"custom_{row["roomid"]}";
                _roomModels.Add(modelName,
                    new RoomModel((int) row["door_x"], (int) row["door_y"], (double) row["door_z"],
                        (int) row["door_dir"],
                        (string) row["heightmap"], "", false, ""));
            }
        }

        /// <summary>
        ///     Update the existent model.
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="modelData"></param>
        internal void UpdateCustomModel(uint roomId, RoomModel modelData)
        {
            string modelId = $"custom_{roomId}";

            if (_roomModels.Contains(modelId))
                _roomModels[modelId] = modelData;
            else
                _roomModels.Add(modelId, modelData);
        }

        /// <summary>
        ///     Called when [cycle].
        /// </summary>
        internal void OnCycle()
        {
            try
            {
                bool flag = WorkActiveRoomsAddQueue();
                bool flag2 = WorkActiveRoomsRemoveQueue();
                bool flag3 = WorkActiveRoomsUpdateQueue();
                if (flag || flag2 || flag3) SortActiveRooms();
                bool flag4 = WorkVotedRoomsAddQueue();
                bool flag5 = WorkVotedRoomsRemoveQueue();
                if (flag4 || flag5) SortVotedRooms();

                Yupi.GetGame().RoomManagerCycleEnded = true;
            }
            catch (Exception ex)
            {
                ServerLogManager.LogThreadException(ex.ToString(), "RoomManager.OnCycle Exception --> Not inclusive");
            }
        }

        /// <summary>
        ///     Queues the vote add.
        /// </summary>
        /// <param name="data">The data.</param>
        internal void QueueVoteAdd(RoomData data)
        {
            lock (_votedRoomsAddQueue.SyncRoot)
            {
                _votedRoomsAddQueue.Enqueue(data);
            }
        }

        /// <summary>
        ///     Queues the vote remove.
        /// </summary>
        /// <param name="data">The data.</param>
        internal void QueueVoteRemove(RoomData data)
        {
            lock (_votedRoomsRemoveQueue.SyncRoot)
            {
                _votedRoomsRemoveQueue.Enqueue(data);
            }
        }

        /// <summary>
        ///     Queues the active room update.
        /// </summary>
        /// <param name="data">The data.</param>
        internal void QueueActiveRoomUpdate(RoomData data)
        {
            lock (_activeRoomsUpdateQueue.SyncRoot)
            {
                _activeRoomsUpdateQueue.Enqueue(data);
            }
        }

        /// <summary>
        ///     Queues the active room add.
        /// </summary>
        /// <param name="data">The data.</param>
        internal void QueueActiveRoomAdd(RoomData data)
        {
            lock (_activeRoomsAddQueue.SyncRoot)
            {
                _activeRoomsAddQueue.Enqueue(data);
            }
        }

        /// <summary>
        ///     Queues the active room remove.
        /// </summary>
        /// <param name="data">The data.</param>
        internal void QueueActiveRoomRemove(RoomData data)
        {
            lock (ActiveRoomsRemoveQueue.SyncRoot)
            {
                ActiveRoomsRemoveQueue.Enqueue(data);
            }
        }

        /// <summary>
        ///     Removes all rooms.
        /// </summary>
        internal void RemoveAllRooms()
        {
            foreach (Room current in LoadedRooms.Values)
                Yupi.GetGame().GetRoomManager().UnloadRoom(current, "RemoveAllRooms void called");

            Writer.WriteLine("RoomManager Destroyed", "Yupi.Rooms", ConsoleColor.DarkYellow);
        }

        /// <summary>
        ///     Unloads the room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="reason">The reason.</param>
        internal void UnloadRoom(Room room, string reason)
        {
            if (room?.RoomData == null || room.Disposed)
                return;

            room.Disposed = true;

            if (Yupi.GetGame().GetNavigator().PrivateCategories.Contains(room.RoomData.Category))
                ((PublicCategory) Yupi.GetGame().GetNavigator().PrivateCategories[room.RoomData.Category]).UsersNow -= room.UserCount;

            room.RoomData.UsersNow = 0;
            string state = "open";

            if (room.RoomData.State == 1)
                state = "locked";
            else if (room.RoomData.State > 1)
                state = "password";

            uint roomId = room.RoomId;

            try
            {
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    commitableQueryReactor.SetQuery(
                        "UPDATE rooms_data SET caption = @caption, description = @description, password = @password, category = " +
                        room.RoomData.Category + ", state = '" + state +
                        "', tags = @tags, users_now = '0', users_max = " +
                        room.RoomData.UsersMax + ", allow_pets = '" + Yupi.BoolToEnum(room.RoomData.AllowPets) +
                        "', allow_pets_eat = '" +
                        Yupi.BoolToEnum(room.RoomData.AllowPetsEating) + "', allow_walkthrough = '" +
                        Yupi.BoolToEnum(room.RoomData.AllowWalkThrough) +
                        "', hidewall = '" + Yupi.BoolToEnum(room.RoomData.HideWall) + "', floorthick = " +
                        room.RoomData.FloorThickness +
                        ", wallthick = " + room.RoomData.WallThickness + ", mute_settings='" + room.RoomData.WhoCanMute +
                        "', kick_settings='" + room.RoomData.WhoCanKick + "',ban_settings='" + room.RoomData.WhoCanBan +
                        "', walls_height = '" + room.RoomData.WallHeight +
                        "', chat_type = @chat_t,chat_balloon = @chat_b,chat_speed = @chat_s,chat_max_distance = @chat_m,chat_flood_protection = @chat_f, trade_state = '" +
                        room.RoomData.TradeState + "' WHERE id = " + roomId);
                    commitableQueryReactor.AddParameter("caption", room.RoomData.Name);
                    commitableQueryReactor.AddParameter("description", room.RoomData.Description);
                    commitableQueryReactor.AddParameter("password", room.RoomData.PassWord);
                    commitableQueryReactor.AddParameter("tags", string.Join(",", room.RoomData.Tags));
                    commitableQueryReactor.AddParameter("chat_t", room.RoomData.ChatType);
                    commitableQueryReactor.AddParameter("chat_b", room.RoomData.ChatBalloon);
                    commitableQueryReactor.AddParameter("chat_s", room.RoomData.ChatSpeed);
                    commitableQueryReactor.AddParameter("chat_m", room.RoomData.ChatMaxDistance);
                    commitableQueryReactor.AddParameter("chat_f", room.RoomData.ChatFloodProtection);
                    commitableQueryReactor.RunQuery();
                }
            }
            catch (Exception e)
            {
                ServerLogManager.LogException(e.ToString());
            }

            if (room.GetRoomUserManager() != null && room.GetRoomUserManager().UserList != null)
            {
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    foreach (RoomUser current in room.GetRoomUserManager().UserList.Values.Where(current => current != null))
                    {
                        if (current.IsPet)
                        {
                            if (current.PetData == null)
                                continue;

                            commitableQueryReactor.SetQuery("UPDATE pets_data SET x=@x, y=@y, z=@z WHERE id=@id LIMIT 1");
                            commitableQueryReactor.AddParameter("x", current.X);
                            commitableQueryReactor.AddParameter("y", current.Y);
                            commitableQueryReactor.AddParameter("z", current.Z);
                            commitableQueryReactor.AddParameter("id", current.PetData.PetId);
                            commitableQueryReactor.RunQuery();

                            if (current.BotAi == null)
                                continue;

                            current.BotAi.Dispose();
                        }
                        else if (current.IsBot)
                        {
                            if (current.BotData == null)
                                continue;

                            commitableQueryReactor.SetQuery("UPDATE bots_data SET x=@x, y=@y, z=@z, name=@name, motto=@motto, look=@look, rotation=@rotation, dance=@dance WHERE id=@id LIMIT 1");
                            commitableQueryReactor.AddParameter("name", current.BotData.Name);
                            commitableQueryReactor.AddParameter("motto", current.BotData.Motto);
                            commitableQueryReactor.AddParameter("look", current.BotData.Look);
                            commitableQueryReactor.AddParameter("rotation", current.BotData.Rot);
                            commitableQueryReactor.AddParameter("dance", current.BotData.DanceId);
                            commitableQueryReactor.AddParameter("x", current.X);
                            commitableQueryReactor.AddParameter("y", current.Y);
                            commitableQueryReactor.AddParameter("z", current.Z);
                            commitableQueryReactor.AddParameter("id", current.BotData.BotId);
                            commitableQueryReactor.RunQuery();

                            current.BotAi?.Dispose();
                        }
                        else
                        {
                            if (current.GetClient() != null)
                            {
                                room.GetRoomUserManager().RemoveUserFromRoom(current.GetClient(), true, false);
                                current.GetClient().CurrentRoomUserId = -1;
                            }
                        }
                    }
                }
            }

            room.SaveRoomChatlog();

            Room junkRoom;
            LoadedRooms.TryRemove(room.RoomId, out junkRoom);

            Writer.WriteLine(string.Format("Room #{0} was unloaded, reason: " + reason, room.RoomId),
                "Yupi.Rooms", ConsoleColor.DarkGray);

            room.Destroy();
        }

        /// <summary>
        ///     Sorts the active rooms.
        /// </summary>
        private void SortActiveRooms()
        {
            _orderedActiveRooms = _activeRooms.OrderByDescending(t => t.Value).Take(40);
        }

        /// <summary>
        ///     Sorts the voted rooms.
        /// </summary>
        private void SortVotedRooms()
        {
            _orderedVotedRooms = _votedRooms.OrderByDescending(t => t.Value).Take(40);
        }

        /// <summary>
        ///     Works the active rooms update queue.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool WorkActiveRoomsUpdateQueue()
        {
            if (_activeRoomsUpdateQueue.Count <= 0) return false;
            lock (_activeRoomsUpdateQueue.SyncRoot)
            {
                while (_activeRoomsUpdateQueue.Count > 0)
                {
                    RoomData roomData = (RoomData) _activeRoomsUpdateQueue.Dequeue();
                    if (roomData == null || roomData.ModelName.Contains("snowwar")) continue;
                    if (!_activeRooms.ContainsKey(roomData)) _activeRooms.Add(roomData, roomData.UsersNow);
                    else _activeRooms[roomData] = roomData.UsersNow;
                }
            }
            return true;
        }

        /// <summary>
        ///     Works the active rooms add queue.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool WorkActiveRoomsAddQueue()
        {
            if (_activeRoomsAddQueue.Count <= 0) return false;
            lock (_activeRoomsAddQueue.SyncRoot)
            {
                while (_activeRoomsAddQueue.Count > 0)
                {
                    RoomData roomData = (RoomData) _activeRoomsAddQueue.Dequeue();
                    if (!_activeRooms.ContainsKey(roomData) && !roomData.ModelName.Contains("snowwar"))
                        _activeRooms.Add(roomData, roomData.UsersNow);
                }
            }
            return true;
        }

        /// <summary>
        ///     Works the active rooms remove queue.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool WorkActiveRoomsRemoveQueue()
        {
            if (ActiveRoomsRemoveQueue.Count <= 0) return false;
            lock (ActiveRoomsRemoveQueue.SyncRoot)
            {
                while (ActiveRoomsRemoveQueue.Count > 0)
                {
                    RoomData key = (RoomData) ActiveRoomsRemoveQueue.Dequeue();
                    _activeRooms.Remove(key);
                }
            }
            return true;
        }

        /// <summary>
        ///     Works the voted rooms add queue.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool WorkVotedRoomsAddQueue()
        {
            if (_votedRoomsAddQueue.Count <= 0) return false;
            lock (_votedRoomsAddQueue.SyncRoot)
            {
                while (_votedRoomsAddQueue.Count > 0)
                {
                    RoomData roomData = (RoomData) _votedRoomsAddQueue.Dequeue();
                    if (!_votedRooms.ContainsKey(roomData)) _votedRooms.Add(roomData, roomData.Score);
                    else _votedRooms[roomData] = roomData.Score;
                }
            }
            return true;
        }

        /// <summary>
        ///     Works the voted rooms remove queue.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool WorkVotedRoomsRemoveQueue()
        {
            if (_votedRoomsRemoveQueue.Count <= 0) return false;
            lock (_votedRoomsRemoveQueue.SyncRoot)
            {
                while (_votedRoomsRemoveQueue.Count > 0)
                {
                    RoomData key = (RoomData) _votedRoomsRemoveQueue.Dequeue();
                    _votedRooms.Remove(key);
                }
            }
            return true;
        }
    }
}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Yupi.Core.Io;
using Yupi.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Browser.Models;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pathfinding;
using Yupi.Game.Pathfinding.Vectors;
using Yupi.Game.Pets;
using Yupi.Game.Pets.Enums;
using Yupi.Game.RoomBots;
using Yupi.Game.RoomBots.Enumerators;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Rooms.Items;
using Yupi.Game.Rooms.Items.Enums;
using Yupi.Game.Rooms.Items.Games.Teams;
using Yupi.Game.Rooms.Items.Games.Teams.Enums;
using Yupi.Game.Rooms.Items.Games.Types.Freeze;
using Yupi.Game.Rooms.User.Path;
using Yupi.Game.Users.Inventory.Components;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Rooms.User
{
    /// <summary>
    ///     Class RoomUserManager.
    /// </summary>
    internal class RoomUserManager
    {
        /// <summary>
        ///     The _to remove
        /// </summary>
        private readonly List<RoomUser> _removeUsers;

        /// <summary>
        ///     The _bots
        /// </summary>
        private HybridDictionary _bots;

        /// <summary>
        ///     The _pets
        /// </summary>
        private HybridDictionary _pets;

        /// <summary>
        ///     The _primary private user identifier
        /// </summary>
        private int _primaryPrivateUserId;

        /// <summary>
        ///     The _user count
        /// </summary>
        private uint _roomUserCount;

        /// <summary>
        ///     The _secondary private user identifier
        /// </summary>
        private int _secondaryPrivateUserId;

        /// <summary>
        ///     The _room
        /// </summary>
        private Room _userRoom;

        /// <summary>
        ///     To set
        /// </summary>
        internal Dictionary<Point, RoomUser> ToSet;

        /// <summary>
        ///     The users by user identifier
        /// </summary>
        internal HybridDictionary UsersByUserId;

        /// <summary>
        ///     The users by user name
        /// </summary>
        internal HybridDictionary UsersByUserName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomUserManager" /> class.
        /// </summary>
        /// <param name="room">The room.</param>
        public RoomUserManager(Room room)
        {
            _userRoom = room;
            UserList = new ConcurrentDictionary<int, RoomUser>();
            _pets = new HybridDictionary();
            _bots = new HybridDictionary();
            UsersByUserName = new HybridDictionary();
            UsersByUserId = new HybridDictionary();
            _primaryPrivateUserId = 0;
            _secondaryPrivateUserId = 0;
            _removeUsers = new List<RoomUser>((int) room.RoomData.UsersMax);
            ToSet = new Dictionary<Point, RoomUser>();
            PetCount = 0;
            _roomUserCount = 0;
        }

        /// <summary>
        ///     Gets the pet count.
        /// </summary>
        /// <value>The pet count.</value>
        internal int PetCount { get; private set; }

        /// <summary>
        ///     Gets the user list.
        /// </summary>
        /// <value>The user list.</value>
        internal ConcurrentDictionary<int, RoomUser> UserList { get; private set; }

        internal event RoomEventDelegate OnUserEnter;

        /// <summary>
        ///     Gets the room user by habbo.
        /// </summary>
        /// <param name="pId">The p identifier.</param>
        /// <returns>RoomUser.</returns>
        public RoomUser GetRoomUserByHabbo(uint pId)
        {
            return UsersByUserId.Contains(pId) ? (RoomUser) UsersByUserId[pId] : null;
        }

        /// <summary>
        ///     Gets the room user count.
        /// </summary>
        /// <returns>System.Int32.</returns>
        internal int GetRoomUserCount()
        {
            return UserList.Count - _bots.Count - _pets.Count;
        }

        /// <summary>
        ///     Deploys the bot.
        /// </summary>
        /// <param name="bot">The bot.</param>
        /// <param name="petData">The pet data.</param>
        /// <returns>RoomUser.</returns>
        internal RoomUser DeployBot(RoomBot bot, Pet petData)
        {
            int virtualId = _primaryPrivateUserId++;

            RoomUser roomUser = new RoomUser(0u, _userRoom.RoomId, virtualId, _userRoom, false);

            int num = _secondaryPrivateUserId++;

            roomUser.InternalRoomId = num;
            UserList.TryAdd(num, roomUser);
            OnUserAdd(roomUser);

            DynamicRoomModel model = _userRoom.GetGameMap().Model;
            Point coord = new Point(bot.X, bot.Y);

            if ((bot.X > 0) && (bot.Y >= 0) && (bot.X < model.MapSizeX) && (bot.Y < model.MapSizeY))
            {
                _userRoom.GetGameMap().AddUserToMap(roomUser, coord);
                roomUser.SetPos(bot.X, bot.Y, bot.Z);
                roomUser.SetRot(bot.Rot, false);
            }
            else
            {
                bot.X = model.DoorX;
                bot.Y = model.DoorY;
                roomUser.SetPos(model.DoorX, model.DoorY, model.DoorZ);
                roomUser.SetRot(model.DoorOrientation, false);
            }

            bot.RoomUser = roomUser;
            roomUser.BotData = bot;

            roomUser.BotAi = bot.GenerateBotAi(roomUser.VirtualId, (int) bot.BotId);

            if (roomUser.IsPet)
            {
                roomUser.BotAi.Init(bot.BotId, roomUser.VirtualId, _userRoom.RoomId, roomUser, _userRoom);
                roomUser.PetData = petData;
                roomUser.PetData.VirtualId = roomUser.VirtualId;
            }
            else
                roomUser.BotAi.Init(bot.BotId, roomUser.VirtualId, _userRoom.RoomId, roomUser, _userRoom);

            UpdateUserStatus(roomUser, false);
            roomUser.UpdateNeeded = true;

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("SetRoomUserMessageComposer"));

            serverMessage.AppendInteger(1);
            roomUser.Serialize(serverMessage, _userRoom.GetGameMap().GotPublicPool);

            _userRoom.SendMessage(serverMessage);

            roomUser.BotAi.OnSelfEnterRoom();

            if (roomUser.IsPet)
            {
                if (_pets.Contains(roomUser.PetData.PetId))
                    _pets[roomUser.PetData.PetId] = roomUser;
                else
                    _pets.Add(roomUser.PetData.PetId, roomUser);

                PetCount++;
            }

            roomUser.BotAi.Modified();

            if (roomUser.BotData.AiType != AiType.Generic)
                return roomUser;

            if (_bots.Contains(roomUser.BotData.BotId))
                _bots[roomUser.BotData.BotId] = roomUser;
            else
                _bots.Add(roomUser.BotData.BotId, roomUser);

            serverMessage.Init(LibraryParser.OutgoingRequest("DanceStatusMessageComposer"));
            serverMessage.AppendInteger(roomUser.VirtualId);
            serverMessage.AppendInteger(roomUser.BotData.DanceId);
            _userRoom.SendMessage(serverMessage);
            PetCount++;

            return roomUser;
        }

        /// <summary>
        ///     Updates the bot.
        /// </summary>
        /// <param name="virtualId">The virtual identifier.</param>
        /// <param name="roomUser">The room user.</param>
        /// <param name="name">The name.</param>
        /// <param name="motto">The motto.</param>
        /// <param name="look">The look.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="speech">The speech.</param>
        /// <param name="responses">The responses.</param>
        /// <param name="speak">if set to <c>true</c> [speak].</param>
        /// <param name="speechDelay">The speech delay.</param>
        /// <param name="mix">if set to <c>true</c> [mix].</param>
        internal void UpdateBot(int virtualId, RoomUser roomUser, string name, string motto, string look, string gender,
            List<string> speech, List<string> responses, bool speak, uint speechDelay, bool mix)
        {
            RoomUser bot = GetRoomUserByVirtualId(virtualId);
            if (bot == null || !bot.IsBot) return;

            RoomBot rBot = bot.BotData;

            rBot.Name = name;
            rBot.Motto = motto;
            rBot.Look = look;
            rBot.Gender = gender;
            rBot.RandomSpeech = speech;
            rBot.Responses = responses;
            rBot.AutomaticChat = speak;
            rBot.SpeechInterval = speechDelay;
            rBot.RoomUser = roomUser;
            rBot.MixPhrases = mix;

            rBot.RoomUser?.BotAi?.Modified();
        }

        /// <summary>
        ///     Removes the bot.
        /// </summary>
        /// <param name="virtualId">The virtual identifier.</param>
        /// <param name="kicked">if set to <c>true</c> [kicked].</param>
        internal void RemoveBot(int virtualId, bool kicked)
        {
            RoomUser roomUserByVirtualId = GetRoomUserByVirtualId(virtualId);
            if (roomUserByVirtualId == null || !roomUserByVirtualId.IsBot) return;

            if (roomUserByVirtualId.IsPet)
            {
                _pets.Remove(roomUserByVirtualId.PetData.PetId);
                PetCount--;
            }
            roomUserByVirtualId.BotAi.OnSelfLeaveRoom(kicked);
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("UserLeftRoomMessageComposer"));
            serverMessage.AppendString(roomUserByVirtualId.VirtualId.ToString());
            _userRoom.SendMessage(serverMessage);

            RoomUser roomUser;
            UserList.TryRemove(roomUserByVirtualId.InternalRoomId, out roomUser);
        }

        /// <summary>
        ///     Gets the user for square.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>RoomUser.</returns>
        internal RoomUser GetUserForSquare(int x, int y)
        {
            return _userRoom.GetGameMap().GetRoomUsers(new Point(x, y)).FirstOrDefault();
        }

        /// <summary>
        ///     Adds the user to room.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="spectator">if set to <c>true</c> [spectator].</param>
        /// <param name="snow">if set to <c>true</c> [snow].</param>
        internal void AddUserToRoom(GameClient session, bool spectator, bool snow = false)
        {
            if (session == null || session.GetHabbo() == null)
                return;
            RoomUser roomUser = new RoomUser(session.GetHabbo().Id, _userRoom.RoomId, _primaryPrivateUserId++, _userRoom,
                spectator);
            if (roomUser.GetClient() == null || roomUser.GetClient().GetHabbo() == null)
                return;

            roomUser.UserId = session.GetHabbo().Id;
            string userName = session.GetHabbo().UserName;
            uint userId = roomUser.UserId;
            if (UsersByUserName.Contains(userName.ToLower()))
                UsersByUserName.Remove(userName.ToLower());
            if (UsersByUserId.Contains(userId))
                UsersByUserId.Remove(userId);
            UsersByUserName.Add(session.GetHabbo().UserName.ToLower(), roomUser);
            UsersByUserId.Add(session.GetHabbo().Id, roomUser);
            int num = _secondaryPrivateUserId++;
            roomUser.InternalRoomId = num;
            session.CurrentRoomUserId = num;
            session.GetHabbo().CurrentRoomId = _userRoom.RoomId;
            UserList.TryAdd(num, roomUser);
            OnUserAdd(roomUser);

            session.GetHabbo().LoadingRoom = 0;

            if (Yupi.GetGame().GetNavigator().PrivateCategories.Contains(_userRoom.RoomData.Category))
                ((PublicCategory) Yupi.GetGame().GetNavigator().PrivateCategories[_userRoom.RoomData.Category]).UsersNow
                    ++;
        }

        /// <summary>
        ///     Updates the user.
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        internal void UpdateUser(string oldName, string newName)
        {
            if (oldName == newName)
                return;

            if (!UsersByUserName.Contains(oldName))
                return;
            UsersByUserName.Add(newName, UsersByUserName[oldName]);
            UsersByUserName.Remove(oldName);
            //
            Yupi.GetGame().GetClientManager().UpdateClient(oldName, newName);
        }

        /// <summary>
        ///     Removes the user from room.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="notifyClient">if set to <c>true</c> [notify client].</param>
        /// <param name="notifyKick">if set to <c>true</c> [notify kick].</param>
        internal void RemoveUserFromRoom(GameClient session, bool notifyClient, bool notifyKick)
        {
            try
            {
                if (session == null || session.GetHabbo() == null || _userRoom == null)
                    return;
                uint userId = session.GetHabbo().Id;

                session.GetHabbo().GetAvatarEffectsInventoryComponent().OnRoomExit();
                //using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                //    queryReactor.RunFastQuery("UPDATE users_rooms_visits SET exit_timestamp = '" + Yupi.GetUnixTimeStamp() + "' WHERE room_id = '" + _room.RoomId + "' AND user_id = '" + session.GetHabbo().Id + "' ORDER BY entry_timestamp DESC LIMIT 1");

                RoomUser roomUserByHabbo = GetRoomUserByHabbo(userId);
                if (roomUserByHabbo == null)
                    return;
                if (notifyKick)
                {
                    Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomUserByHabbo.RoomId);
                    DynamicRoomModel model = room.GetGameMap().Model;
                    roomUserByHabbo.MoveTo(model.DoorX, model.DoorY);
                    roomUserByHabbo.CanWalk = false;
                    session.GetMessageHandler()
                        .GetResponse()
                        .Init(LibraryParser.OutgoingRequest("RoomErrorMessageComposer"));
                    session.GetMessageHandler().GetResponse().AppendInteger(4008);
                    session.GetMessageHandler().SendResponse();

                    session.GetMessageHandler()
                        .GetResponse()
                        .Init(LibraryParser.OutgoingRequest("OutOfRoomMessageComposer"));
                    session.GetMessageHandler().GetResponse().AppendShort(2);
                    session.GetMessageHandler().SendResponse();
                }
                else if (notifyClient)
                {
                    ServerMessage serverMessage =
                        new ServerMessage(LibraryParser.OutgoingRequest("UserIsPlayingFreezeMessageComposer"));
                    serverMessage.AppendBool(roomUserByHabbo.Team != Team.None);
                    roomUserByHabbo.GetClient().SendMessage(serverMessage);
                    session.GetMessageHandler()
                        .GetResponse()
                        .Init(LibraryParser.OutgoingRequest("OutOfRoomMessageComposer"));
                    session.GetMessageHandler().GetResponse().AppendShort(2);
                    session.GetMessageHandler().SendResponse();
                }
                if (roomUserByHabbo.Team != Team.None)
                {
                    _userRoom.GetTeamManagerForBanzai().OnUserLeave(roomUserByHabbo);
                    _userRoom.GetTeamManagerForFreeze().OnUserLeave(roomUserByHabbo);
                }
                if (roomUserByHabbo.RidingHorse)
                {
                    roomUserByHabbo.RidingHorse = false;
                    RoomUser horse = GetRoomUserByVirtualId((int) roomUserByHabbo.HorseId);
                    if (horse != null)
                    {
                        horse.RidingHorse = false;
                        horse.HorseId = 0u;
                    }
                }
                if (roomUserByHabbo.IsLyingDown || roomUserByHabbo.IsSitting)
                {
                    roomUserByHabbo.IsSitting = false;
                    roomUserByHabbo.IsLyingDown = false;
                }
                RemoveRoomUser(roomUserByHabbo);
                if (session.GetHabbo() != null && !roomUserByHabbo.IsSpectator)
                {
                    if (roomUserByHabbo.CurrentItemEffect != ItemEffectType.None)
                        roomUserByHabbo.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().CurrentEffect = -1;
                    if (session.GetHabbo() != null)
                    {
                        if (_userRoom.HasActiveTrade(session.GetHabbo().Id))
                            _userRoom.TryStopTrade(session.GetHabbo().Id);
                        session.GetHabbo().CurrentRoomId = 0;
                        if (session.GetHabbo().GetMessenger() != null)
                            session.GetHabbo().GetMessenger().OnStatusChanged(true);
                    }

                    using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                        if (session.GetHabbo() != null)
                            queryreactor2.RunFastQuery(string.Concat(
                                "UPDATE users_rooms_visits SET exit_timestamp = '", Yupi.GetUnixTimeStamp(),
                                "' WHERE room_id = '", _userRoom.RoomId, "' AND user_id = '", userId,
                                "' ORDER BY exit_timestamp DESC LIMIT 1"));
                }
                UsersByUserId.Remove(roomUserByHabbo.UserId);
                if (session.GetHabbo() != null)
                    UsersByUserName.Remove(session.GetHabbo().UserName.ToLower());
                roomUserByHabbo.Dispose();
            }
            catch (Exception ex)
            {
                ServerLogManager.LogCriticalException($"Error during removing user from room:{ex}");
            }
        }

        /// <summary>
        ///     Removes the room user.
        /// </summary>
        /// <param name="user">The user.</param>
        internal void RemoveRoomUser(RoomUser user)
        {
            RoomUser junk;
            if (!UserList.TryRemove(user.InternalRoomId, out junk)) return;

            user.InternalRoomId = -1;
            _userRoom.GetGameMap().GameMap[user.X, user.Y] = user.SqState;
            _userRoom.GetGameMap().RemoveUserFromMap(user, new Point(user.X, user.Y));

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("UserLeftRoomMessageComposer"));
            serverMessage.AppendString(user.VirtualId.ToString());
            _userRoom.SendMessage(serverMessage);

            OnRemove(junk);
        }

        /// <summary>
        ///     Gets the pet.
        /// </summary>
        /// <param name="petId">The pet identifier.</param>
        /// <returns>RoomUser.</returns>
        internal RoomUser GetPet(uint petId)
        {
            if (_pets.Contains(petId))
                return (RoomUser) _pets[petId];
            return null;
        }

        /// <summary>
        ///     Gets the bot.
        /// </summary>
        /// <param name="botId">The bot identifier.</param>
        /// <returns>RoomUser.</returns>
        internal RoomUser GetBot(uint botId)
        {
            if (_bots.Contains(botId))
                return (RoomUser) _bots[botId];
            return null;
        }

        internal RoomUser GetBotByName(string name)
        {
            RoomUser roomUser = UserList.Values.FirstOrDefault(b => b.BotData != null && b.BotData.Name == name);
            return roomUser;
        }

        /// <summary>
        ///     Updates the user count.
        /// </summary>
        /// <param name="count">The count.</param>
        internal void UpdateUserCount(uint count)
        {
            _roomUserCount = count;
            _userRoom.RoomData.UsersNow = count;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery("UPDATE rooms_data SET users_now = " + count + " WHERE id = " +
                                                    _userRoom.RoomId + " LIMIT 1");
            Yupi.GetGame().GetRoomManager().QueueActiveRoomUpdate(_userRoom.RoomData);
        }

        /// <summary>
        ///     Gets the room user by virtual identifier.
        /// </summary>
        /// <param name="virtualId">The virtual identifier.</param>
        /// <returns>RoomUser.</returns>
        internal RoomUser GetRoomUserByVirtualId(int virtualId)
        {
            return UserList.ContainsKey(virtualId) ? UserList[virtualId] : null;
        }

        /// <summary>
        ///     Gets the users in camping tent.
        /// </summary>
        /// <returns>List&lt;RoomUser&gt;.</returns>
        internal List<RoomUser> GetUsersInCampingTent()
        {
            return GetRoomUsers().Where(x => x.OnCampingTent).ToList();
        }

        /// <summary>
        ///     Gets the room users.
        /// </summary>
        /// <returns>HashSet&lt;RoomUser&gt;.</returns>
        internal HashSet<RoomUser> GetRoomUsers()
        {
            return new HashSet<RoomUser>(UserList.Values.Where(x => x.IsBot == false));
        }

        /// <summary>
        ///     Gets the room user by rank.
        /// </summary>
        /// <param name="minRank">The minimum rank.</param>
        /// <returns>List&lt;RoomUser&gt;.</returns>
        internal List<RoomUser> GetRoomUserByRank(int minRank)
        {
            return
                UserList.Values.Where(
                    current =>
                        !current.IsBot && current.GetClient() != null && current.GetClient().GetHabbo() != null &&
                        current.GetClient().GetHabbo().Rank > (ulong) minRank).ToList();
        }

        /// <summary>
        ///     Gets the room user by habbo.
        /// </summary>
        /// <param name="pName">Name of the p.</param>
        /// <returns>RoomUser.</returns>
        internal RoomUser GetRoomUserByHabbo(string pName)
        {
            if (UsersByUserName.Contains(pName.ToLower()))
                return (RoomUser) UsersByUserName[pName.ToLower()];
            return null;
        }

        /// <summary>
        ///     Saves the pets.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void SavePets(IQueryAdapter dbClient)
        {
            if (GetPets().Any())
                AppendPetsUpdateString(dbClient);
        }

        /// <summary>
        ///     Appends the pets update string.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void AppendPetsUpdateString(IQueryAdapter dbClient)
        {
            foreach (Pet current in GetPets())
            {
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    commitableQueryReactor.SetQuery("UPDATE pets_data SET " +
                                                    $"name = @petName, race_id = @petRace, pet_type = @petType, experience = {current.Experience}, " +
                                                    $"energy = {current.Energy}, nutrition = {current.Nutrition}, respect = {current.Respect}, " +
                                                    $"createstamp = '{current.CreationStamp}', color = @petColor WHERE id = {current.PetId}");

                    commitableQueryReactor.AddParameter("petName", current.Name);
                    commitableQueryReactor.AddParameter("petRace", current.Race);
                    commitableQueryReactor.AddParameter("petType", current.Type);
                    commitableQueryReactor.AddParameter("petColor", current.Color);

                    commitableQueryReactor.RunQuery();
                }

                current.DbState = DatabaseUpdateState.Updated;
            }
        }

        /// <summary>
        ///     Gets the pets.
        /// </summary>
        /// <returns>List&lt;Pet&gt;.</returns>
        internal List<Pet> GetPets()
        {
            List<KeyValuePair<int, RoomUser>> list = UserList.ToList();

            return (from current in list select current.Value into value where value.IsPet select value.PetData).ToList();
        }

        /// <summary>
        ///     Serializes the status updates.
        /// </summary>
        /// <param name="all">if set to <c>true</c> [all].</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeStatusUpdates(bool all)
        {
            List<RoomUser> list = new List<RoomUser>();

            foreach (RoomUser current in UserList.Values)
            {
                if (!all)
                {
                    if (!current.UpdateNeeded)
                        continue;

                    current.UpdateNeeded = false;
                }

                list.Add(current);
            }

            if (!list.Any())
                return null;

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("UpdateUserStatusMessageComposer"));

            serverMessage.AppendInteger(list.Count);

            foreach (RoomUser current2 in list)
                current2.SerializeStatus(serverMessage);

            return serverMessage;
        }

        /// <summary>
        ///     Backups the counters.
        /// </summary>
        /// <param name="primaryCounter">The primary counter.</param>
        /// <param name="secondaryCounter">The secondary counter.</param>
        internal void BackupCounters(ref int primaryCounter, ref int secondaryCounter)
        {
            primaryCounter = _primaryPrivateUserId;
            secondaryCounter = _secondaryPrivateUserId;
        }

        /// <summary>
        ///     Updates the user status.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cycleGameItems">if set to <c>true</c> [cyclegameitems].</param>
        internal void UpdateUserStatus(RoomUser user, bool cycleGameItems)
        {
            if (user == null) return;

            if (user.Statusses.ContainsKey("lay") || user.Statusses.ContainsKey("sit"))
            {
                user.Statusses.Remove("lay");
                user.Statusses.Remove("sit");
                user.UpdateNeeded = true;
            }

            bool isBot = user.IsBot;
            if (isBot) cycleGameItems = false;

            try
            {
                Gamemap roomMap = _userRoom.GetGameMap();
                Point userPoint = new Point(user.X, user.Y);
                RoomItem[] allRoomItemForSquare = roomMap.GetCoordinatedHeighestItems(userPoint).ToArray();
                List<RoomItem> itemsOnSquare = roomMap.GetCoordinatedItems(userPoint);

                double newZ = _userRoom.GetGameMap().SqAbsoluteHeight(user.X, user.Y, itemsOnSquare) +
                           (user.RidingHorse && user.IsPet == false ? 1 : 0);

                if (Math.Abs(newZ - user.Z) > 0)
                {
                    user.Z = newZ;
                    user.UpdateNeeded = true;
                }
                foreach (RoomItem item in allRoomItemForSquare)
                {
                    if (cycleGameItems)
                    {
                        item.UserWalksOnFurni(user);
                    }

                    if (item.GetBaseItem().IsSeat)
                    {
                        if (!user.Statusses.ContainsKey("sit"))
                        {
                            if (item.GetBaseItem().StackMultipler && !string.IsNullOrWhiteSpace(item.ExtraData))
                                if (item.ExtraData != "0")
                                {
                                    int num2 = Convert.ToInt32(item.ExtraData);
                                    if (!user.Statusses.ContainsKey("sit"))
                                        user.Statusses.Add("sit",
                                            item.GetBaseItem().ToggleHeight[num2].ToString(CultureInfo.InvariantCulture)
                                                .Replace(',', '.'));
                                }
                                else
                                {
                                    if (!user.Statusses.ContainsKey("sit"))
                                        user.Statusses.Add("sit",
                                            Convert.ToString(item.GetBaseItem().Height, CultureInfo.InvariantCulture));
                                }
                            else
                            {
                                if (!user.Statusses.ContainsKey("sit"))
                                    user.Statusses.Add("sit",
                                        Convert.ToString(item.GetBaseItem().Height, CultureInfo.InvariantCulture));
                            }
                        }

                        if (Math.Abs(user.Z - item.Z) > 0 || user.RotBody != item.Rot)
                        {
                            user.Z = item.Z;
                            user.RotHead = item.Rot;
                            user.RotBody = item.Rot;
                            user.UpdateNeeded = true;
                        }
                    }

                    Interaction interactionType = item.GetBaseItem().InteractionType;

                    switch (interactionType)
                    {
                        case Interaction.QuickTeleport:
                        case Interaction.GuildGate:
                        case Interaction.WalkInternalLink:
                        {
                            item.Interactor.OnUserWalk(user.GetClient(), item, user);
                            break;
                        }
                        case Interaction.None:
                            break;

                        case Interaction.PressurePadBed:
                        case Interaction.Bed:
                        {
                            if (!user.Statusses.ContainsKey("lay"))
                                user.Statusses.Add("lay", ServerUserChatTextHandler.GetString(item.GetBaseItem().Height));
                            else
                            {
                                if (user.Statusses["lay"] !=
                                    ServerUserChatTextHandler.GetString(item.GetBaseItem().Height))
                                    user.Statusses["lay"] =
                                        ServerUserChatTextHandler.GetString(item.GetBaseItem().Height);
                            }

                            user.Z = item.Z;
                            user.RotHead = item.Rot;
                            user.RotBody = item.Rot;
                            user.UpdateNeeded = true;

                            if (item.GetBaseItem().InteractionType == Interaction.PressurePadBed)
                            {
                                item.ExtraData = "1";
                                item.UpdateState();
                            }
                            break;
                        }

                        case Interaction.Guillotine:
                        {
                            if (!user.Statusses.ContainsKey("lay"))
                                user.Statusses.Add("lay", ServerUserChatTextHandler.GetString(item.GetBaseItem().Height));
                            else if (user.Statusses["lay"] !=
                                     ServerUserChatTextHandler.GetString(item.GetBaseItem().Height))
                                user.Statusses["lay"] =
                                    ServerUserChatTextHandler.GetString(item.GetBaseItem().Height);

                            user.Z = item.Z;
                            user.RotBody = item.Rot;

                            item.ExtraData = "1";
                            item.UpdateState();
                            AvatarEffectComponent avatarEffectsInventoryComponent =
                                user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent();

                            avatarEffectsInventoryComponent.ActivateCustomEffect(133);
                            break;
                        }

                        case Interaction.FootballGate:
                            break;

                        case Interaction.BanzaiGateBlue:
                        case Interaction.BanzaiGateRed:
                        case Interaction.BanzaiGateYellow:
                        case Interaction.BanzaiGateGreen:
                        {
                            int effect = (int) item.Team + 32;
                            TeamManager teamManagerForBanzai =
                                user.GetClient().GetHabbo().CurrentRoom.GetTeamManagerForBanzai();
                            AvatarEffectComponent avatarEffectsInventoryComponent =
                                user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent();
                            if (user.Team == Team.None)
                            {
                                if (!teamManagerForBanzai.CanEnterOnTeam(item.Team)) break;
                                if (user.Team != Team.None) teamManagerForBanzai.OnUserLeave(user);
                                user.Team = item.Team;
                                teamManagerForBanzai.AddUser(user);
                                if (avatarEffectsInventoryComponent.CurrentEffect != effect)
                                    avatarEffectsInventoryComponent.ActivateCustomEffect(effect);
                                break;
                            }
                            if (user.Team != Team.None && user.Team != item.Team)
                            {
                                teamManagerForBanzai.OnUserLeave(user);
                                user.Team = Team.None;
                                avatarEffectsInventoryComponent.ActivateCustomEffect(0);
                                break;
                            }
                            teamManagerForBanzai.OnUserLeave(user);

                            if (avatarEffectsInventoryComponent.CurrentEffect == effect)
                                avatarEffectsInventoryComponent.ActivateCustomEffect(0);
                            user.Team = Team.None;
                            break;
                        }

                        case Interaction.Jump:
                            break;

                        case Interaction.Pinata:
                        {
                            if (!user.IsWalking || item.ExtraData.Length <= 0) break;
                            int num5 = int.Parse(item.ExtraData);
                            if (num5 >= 100 || user.CurrentEffect != 158) break;
                            int num6 = num5 + 1;
                            item.ExtraData = num6.ToString();
                            item.UpdateState();
                            Yupi.GetGame()
                                .GetAchievementManager()
                                .ProgressUserAchievement(user.GetClient(), "ACH_PinataWhacker", 1);
                            if (num6 == 100)
                            {
                                Yupi.GetGame().GetPinataHandler().DeliverRandomPinataItem(user, _userRoom, item);
                                Yupi.GetGame()
                                    .GetAchievementManager()
                                    .ProgressUserAchievement(user.GetClient(), "ACH_PinataBreaker", 1);
                            }
                            break;
                        }
                        case Interaction.TileStackMagic:
                        case Interaction.Poster:
                            break;

                        case Interaction.Tent:
                        case Interaction.BedTent:
                            if (user.LastItem == item.Id) break;
                            if (!user.IsBot && !user.OnCampingTent)
                            {
                                ServerMessage serverMessage22 = new ServerMessage();
                                serverMessage22.Init(
                                    LibraryParser.OutgoingRequest("UpdateFloorItemExtraDataMessageComposer"));
                                serverMessage22.AppendString(item.Id.ToString());
                                serverMessage22.AppendInteger(0);
                                serverMessage22.AppendString("1");
                                user.GetClient().SendMessage(serverMessage22);
                                user.OnCampingTent = true;
                                user.LastItem = item.Id;
                            }
                            break;

                        case Interaction.RunWaySage:
                        {
                            int num7 = new Random().Next(1, 4);
                            item.ExtraData = num7.ToString();
                            item.UpdateState();
                            break;
                        }
                        case Interaction.Shower:
                        case Interaction.ChairState:
                        case Interaction.PressurePad:
                        {
                            item.ExtraData = "1";
                            item.UpdateState();
                            break;
                        }
                        case Interaction.BanzaiTele:
                        {
                            if (user.IsWalking)
                                _userRoom.GetGameItemHandler().OnTeleportRoomUserEnter(user, item);
                            break;
                        }
                        case Interaction.FreezeYellowGate:
                        case Interaction.FreezeRedGate:
                        case Interaction.FreezeGreenGate:
                        case Interaction.FreezeBlueGate:
                        {
                            if (cycleGameItems)
                            {
                                int num4 = (int) (item.Team + 39);
                                TeamManager teamManagerForFreeze =
                                    user.GetClient().GetHabbo().CurrentRoom.GetTeamManagerForFreeze();
                                AvatarEffectComponent avatarEffectsInventoryComponent2 =
                                    user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent();
                                if (user.Team != item.Team)
                                {
                                    if (teamManagerForFreeze.CanEnterOnTeam(item.Team))
                                    {
                                        if (user.Team != Team.None) teamManagerForFreeze.OnUserLeave(user);
                                        user.Team = item.Team;
                                        teamManagerForFreeze.AddUser(user);
                                        if (avatarEffectsInventoryComponent2.CurrentEffect != num4)
                                            avatarEffectsInventoryComponent2.ActivateCustomEffect(num4);
                                    }
                                }
                                else
                                {
                                    teamManagerForFreeze.OnUserLeave(user);
                                    if (avatarEffectsInventoryComponent2.CurrentEffect == num4)
                                        avatarEffectsInventoryComponent2.ActivateCustomEffect(0);
                                    user.Team = Team.None;
                                }
                                ServerMessage serverMessage33 =
                                    new ServerMessage(
                                        LibraryParser.OutgoingRequest("UserIsPlayingFreezeMessageComposer"));
                                serverMessage33.AppendBool(user.Team != Team.None);
                                user.GetClient().SendMessage(serverMessage33);
                            }
                            break;
                        }
                    }

                    if (item.GetBaseItem().InteractionType == Interaction.BedTent)
                        user.OnCampingTent = true;

                    user.LastItem = item.Id;
                }

                if (user.IsSitting && user.TeleportEnabled)
                {
                    user.Z -= 0.35;
                    user.UpdateNeeded = true;
                }
                if (cycleGameItems)
                {
                    _userRoom.GetSoccer().OnUserWalk(user);
                    _userRoom.GetBanzai().OnUserWalk(user);
                    _userRoom.GetFreeze().OnUserWalk(user);
                }
            }
            catch (Exception e)
            {
                ServerLogManager.LogException(e, "Yupi.Game.Rooms.User.RoomUserManager.UpdateUserStatus");
            }
        }

        /// <summary>
        ///     Turns the heads.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="senderId">The sender identifier.</param>
        internal void TurnHeads(int x, int y, uint senderId)
        {
            foreach (
                RoomUser current in
                    UserList.Values.Where(
                        current => current.HabboId != senderId && !current.RidingHorse && !current.IsPet))
                current.SetRot(PathFinder.CalculateRotation(current.X, current.Y, x, y), true);
        }

        internal void UserRoomTimeCycles(RoomUser roomUsers)
        {
            if (!roomUsers.IsAsleep && (roomUsers.IdleTime >= 600) && !roomUsers.IsBot && !roomUsers.IsPet)
            {
                roomUsers.IsAsleep = true;

                ServerMessage sleepEffectMessage = new ServerMessage(LibraryParser.OutgoingRequest("RoomUserIdleMessageComposer"));
                sleepEffectMessage.AppendInteger(roomUsers.VirtualId);
                sleepEffectMessage.AppendBool(true);
                _userRoom.SendMessage(sleepEffectMessage);
            }

            if (!roomUsers.IsOwner() && (roomUsers.IdleTime >= 300) && !roomUsers.IsBot && !roomUsers.IsPet)
            {
                try
                {
                    GameClient ownerAchievementMessage =
                        Yupi.GetGame().GetClientManager().GetClientByUserId(_userRoom.RoomData.OwnerId);

                    if (ownerAchievementMessage != null)
                        Yupi.GetGame()
                            .GetAchievementManager()
                            .ProgressUserAchievement(ownerAchievementMessage, "ACH_RoomDecoHosting", 1, true);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        internal void RoomUserBreedInteraction(RoomUser roomUsers)
        {
            if (roomUsers.IsPet && ((roomUsers.PetData.Type == "pet_terrier") || (roomUsers.PetData.Type == "pet_bear")) &&
                (roomUsers.PetData.WaitingForBreading > 0) && (roomUsers.PetData.BreadingTile.X == roomUsers.X) &&
                (roomUsers.PetData.BreadingTile.Y == roomUsers.Y))
            {
                roomUsers.Freezed = true;
                _userRoom.GetGameMap().RemoveUserFromMap(roomUsers, roomUsers.Coordinate);

                switch (roomUsers.PetData.Type)
                {
                    case "pet_terrier":
                        if (
                            _userRoom.GetRoomItemHandler().BreedingTerrier[roomUsers.PetData.WaitingForBreading]
                                .PetsList.Count == 2)
                        {
                            GameClient petBreedOwner =
                                Yupi.GetGame().GetClientManager().GetClientByUserId(roomUsers.PetData.OwnerId);

                            petBreedOwner?.SendMessage(PetBreeding.GetMessage(roomUsers.PetData.WaitingForBreading,
                                _userRoom.GetRoomItemHandler().BreedingTerrier[roomUsers.PetData.WaitingForBreading]
                                    .PetsList[0],
                                _userRoom.GetRoomItemHandler().BreedingTerrier[roomUsers.PetData.WaitingForBreading]
                                    .PetsList[1]));
                        }
                        break;

                    case "pet_bear":
                        if (
                            _userRoom.GetRoomItemHandler().BreedingBear[roomUsers.PetData.WaitingForBreading].PetsList
                                .Count == 2)
                        {
                            GameClient petBreedOwner =
                                Yupi.GetGame().GetClientManager().GetClientByUserId(roomUsers.PetData.OwnerId);

                            petBreedOwner?.SendMessage(PetBreeding.GetMessage(roomUsers.PetData.WaitingForBreading,
                                _userRoom.GetRoomItemHandler().BreedingBear[roomUsers.PetData.WaitingForBreading]
                                    .PetsList[0],
                                _userRoom.GetRoomItemHandler().BreedingBear[roomUsers.PetData.WaitingForBreading]
                                    .PetsList[1]));
                        }
                        break;
                }

                UpdateUserStatus(roomUsers, false);
            }
            else if (roomUsers.IsPet &&
                     ((roomUsers.PetData.Type == "pet_terrier") || (roomUsers.PetData.Type == "pet_bear")) &&
                     (roomUsers.PetData.WaitingForBreading > 0) && (roomUsers.PetData.BreadingTile.X != roomUsers.X) &&
                     (roomUsers.PetData.BreadingTile.Y != roomUsers.Y))
            {
                roomUsers.Freezed = false;
                roomUsers.PetData.WaitingForBreading = 0;
                roomUsers.PetData.BreadingTile = new Point();
                UpdateUserStatus(roomUsers, false);
            }
        }

        internal void UserSetPositionData(RoomUser roomUsers, Vector2D nextStep)
        {
            // Check if the User is in a Horse or Not..
            if (roomUsers.RidingHorse && !roomUsers.IsPet)
            {
                RoomUser horseRidingPet = GetRoomUserByVirtualId(Convert.ToInt32(roomUsers.HorseId));

                // If exists a Horse and is Ridding.. Let's Create data for they..
                if (horseRidingPet != null)
                {
                    roomUsers.RotBody = horseRidingPet.RotBody;
                    roomUsers.RotHead = horseRidingPet.RotHead;
                    roomUsers.SetStep = true;

                    roomUsers.SetX = nextStep.X;
                    roomUsers.SetY = nextStep.Y;
                    roomUsers.SetZ = _userRoom.GetGameMap().SqAbsoluteHeight(nextStep.X, nextStep.Y) + 1;

                    horseRidingPet.SetX = roomUsers.SetX;
                    horseRidingPet.SetY = roomUsers.SetY;
                    horseRidingPet.SetZ = _userRoom.GetGameMap().SqAbsoluteHeight(roomUsers.SetX, roomUsers.SetY);
                }
            }
            else
            {
                // Is a Normal User, Let's Create Data for They.
                roomUsers.RotBody = Rotation.Calculate(roomUsers.X, roomUsers.Y, nextStep.X, nextStep.Y,
                    roomUsers.IsMoonwalking);
                roomUsers.RotHead = Rotation.Calculate(roomUsers.X, roomUsers.Y, nextStep.X, nextStep.Y,
                    roomUsers.IsMoonwalking);
                roomUsers.SetStep = true;

                roomUsers.SetX = nextStep.X;
                roomUsers.SetY = nextStep.Y;
                roomUsers.SetZ = _userRoom.GetGameMap().SqAbsoluteHeight(nextStep.X, nextStep.Y);
            }
        }

        internal void CheckUserSittableLayable(RoomUser roomUsers)
        {
            // Check if User Is ina  Special Action..

            // User is Laying Down..
            if (roomUsers.Statusses.ContainsKey("lay") || roomUsers.IsLyingDown)
            {
                roomUsers.Statusses.Remove("lay");
                roomUsers.IsLyingDown = false;
                roomUsers.UpdateNeeded = true;
            }

            // User is Sitting Down..
            if ((roomUsers.Statusses.ContainsKey("sit") || roomUsers.IsSitting) && !roomUsers.RidingHorse)
            {
                roomUsers.Statusses.Remove("sit");
                roomUsers.IsSitting = false;
                roomUsers.UpdateNeeded = true;
            }
        }

        internal void UserGoToTile(RoomUser roomUsers, bool invalidStep)
        {
            // If The Tile that the user want to Walk is Invalid!
            if (invalidStep || (roomUsers.PathStep >= roomUsers.Path.Count) ||
                ((roomUsers.GoalX == roomUsers.X) && (roomUsers.GoalY == roomUsers.Y)))
            {
                // Erase all Movement Data..
                roomUsers.IsWalking = false;
                roomUsers.ClearMovement();
                roomUsers.HandelingBallStatus = 0;
                RoomUserBreedInteraction(roomUsers);

                // Check if he is in a Horse, and if if Erase Horse and User Movement Data
                if (roomUsers.RidingHorse && !roomUsers.IsPet)
                {
                    RoomUser horseStopWalkRidingPet = GetRoomUserByVirtualId(Convert.ToInt32(roomUsers.HorseId));

                    if (horseStopWalkRidingPet != null)
                    {
                        ServerMessage horseStopWalkRidingPetMessage =
                            new ServerMessage(LibraryParser.OutgoingRequest("UpdateUserStatusMessageComposer"));
                        horseStopWalkRidingPetMessage.AppendInteger(1);
                        horseStopWalkRidingPet.SerializeStatus(horseStopWalkRidingPetMessage, "");
                        _userRoom.SendMessage(horseStopWalkRidingPetMessage);

                        horseStopWalkRidingPet.IsWalking = false;
                        horseStopWalkRidingPet.ClearMovement();
                    }
                }

                // Finally Update User Status
                UpdateUserStatus(roomUsers, false);
                return;
            }

            // Ins't a Invalid Step.. Continuing.
            // Region Set Variables
            int pathDataCount = roomUsers.Path.Count - roomUsers.PathStep - 1;
            Vector2D nextStep = roomUsers.Path[pathDataCount];

            // Increase Step Data...
            roomUsers.PathStep++;

            // Check Against if is a Valid Step...
            if (_userRoom.GetGameMap()
                .IsValidStep3(roomUsers, new Vector2D(roomUsers.X, roomUsers.Y), new Vector2D(nextStep.X, nextStep.Y),
                    (roomUsers.GoalX == nextStep.X) && (roomUsers.GoalY == nextStep.Y), roomUsers.AllowOverride,
                    roomUsers.GetClient()))
            {
                // If is a PET Must Give the Time Tick In Syncrony with User..
                if (roomUsers.RidingHorse && !roomUsers.IsPet)
                {
                    RoomUser horsePetAi = GetRoomUserByVirtualId(Convert.ToInt32(roomUsers.HorseId));

                    horsePetAi?.BotAi.OnTimerTick();
                }

                // Horse Ridding need be Updated First
                if (roomUsers.RidingHorse)
                {
                    // Set User Position Data
                    UserSetPositionData(roomUsers, nextStep);
                    CheckUserSittableLayable(roomUsers);

                    // Add Status of Walking
                    roomUsers.AddStatus("mv",
                        +roomUsers.SetX + "," + roomUsers.SetY + "," +
                        ServerUserChatTextHandler.GetString(roomUsers.SetZ));
                }

                // Check if User is Ridding in Horse, if if Let's Update Ride Data.
                if (roomUsers.RidingHorse && !roomUsers.IsPet)
                {
                    RoomUser horseRidingPet = GetRoomUserByVirtualId(Convert.ToInt32(roomUsers.HorseId));

                    if (horseRidingPet != null)
                    {
                        string theUser = "mv " + roomUsers.SetX + "," + roomUsers.SetY + "," +
                                      ServerUserChatTextHandler.GetString(roomUsers.SetZ);
                        string thePet = "mv " + roomUsers.SetX + "," + roomUsers.SetY + "," +
                                     ServerUserChatTextHandler.GetString(horseRidingPet.SetZ);

                        ServerMessage horseRidingPetMessage =
                            new ServerMessage(LibraryParser.OutgoingRequest("UpdateUserStatusMessageComposer"));
                        horseRidingPetMessage.AppendInteger(2);
                        roomUsers.SerializeStatus(horseRidingPetMessage, theUser);
                        horseRidingPet.SerializeStatus(horseRidingPetMessage, thePet);
                        _userRoom.SendMessage(horseRidingPetMessage);

                        horseRidingPet.RotBody = roomUsers.RotBody;
                        horseRidingPet.RotHead = roomUsers.RotBody;
                        horseRidingPet.SetX = roomUsers.SetX;
                        horseRidingPet.SetY = roomUsers.SetY;
                        horseRidingPet.SetZ = roomUsers.SetZ - 1;
                        horseRidingPet.SetStep = true;

                        UpdateUserEffect(horseRidingPet, horseRidingPet.SetX, horseRidingPet.SetY);
                        UpdateUserStatus(horseRidingPet, false);
                    }
                }

                // If is not Ridding Horse doesn't Need Update Effect
                if (!roomUsers.RidingHorse)
                {
                    // Set User Position Data
                    UserSetPositionData(roomUsers, nextStep);
                    CheckUserSittableLayable(roomUsers);

                    // Add Status of Walking
                    roomUsers.AddStatus("mv",
                        +roomUsers.SetX + "," + roomUsers.SetY + "," +
                        ServerUserChatTextHandler.GetString(roomUsers.SetZ));
                }

                // Region Update User Effect And Status
                UpdateUserEffect(roomUsers, roomUsers.SetX, roomUsers.SetY);

                // Update Effect if is Ridding
                if (roomUsers.RidingHorse)
                    UpdateUserStatus(roomUsers, false);

                // Region Update User Map Data
                _userRoom.GetGameMap().GameMap[roomUsers.X, roomUsers.Y] = roomUsers.SqState;
                roomUsers.SqState = _userRoom.GetGameMap().GameMap[roomUsers.SetX, roomUsers.SetY];

                // If user is in soccer proccess.
                if (_userRoom.GotSoccer())
                    _userRoom.GetSoccer().OnUserWalk(roomUsers);

                return;
            }

            // Isn't a Valid Step! And he Can Go? Erase Imediatile Effect
            if (roomUsers.Statusses.ContainsKey("mv"))
                roomUsers.ClearMovement();

            // If user isn't pet and Bot, we have serious Problems. Let Recalculate Path!
            if (!roomUsers.IsPet && !roomUsers.IsBot)
                roomUsers.PathRecalcNeeded = true;
        }

        internal bool UserCanWalkInTile(RoomUser roomUsers)
        {
            // Check if User CanWalk...
            if (_userRoom.GetGameMap().CanWalk(roomUsers.SetX, roomUsers.SetY, roomUsers.AllowOverride) ||
                roomUsers.RidingHorse)
            {
                // Let's Update his Movement...
                _userRoom.GetGameMap()
                    .UpdateUserMovement(new Point(roomUsers.Coordinate.X, roomUsers.Coordinate.Y),
                        new Point(roomUsers.SetX, roomUsers.SetY), roomUsers);
                List<RoomItem> hasItemInPlace = _userRoom.GetGameMap().GetCoordinatedItems(new Point(roomUsers.X, roomUsers.Y));

                // Set His Actual X,Y,Z Position...
                roomUsers.X = roomUsers.SetX;
                roomUsers.Y = roomUsers.SetY;
                roomUsers.Z = roomUsers.SetZ;

                // Check Sub Items Interactionables
                foreach (RoomItem roomItem in hasItemInPlace.ToArray())
                {
                    roomItem.UserWalksOffFurni(roomUsers);
                    switch (roomItem.GetBaseItem().InteractionType)
                    {
                        case Interaction.RunWaySage:
                        case Interaction.ChairState:
                        case Interaction.Shower:
                        case Interaction.PressurePad:
                        case Interaction.PressurePadBed:
                        case Interaction.Guillotine:
                            roomItem.ExtraData = "0";
                            roomItem.UpdateState();
                            break;

                        case Interaction.Tent:
                        case Interaction.BedTent:
                            if (!roomUsers.IsBot && roomUsers.OnCampingTent)
                            {
                                ServerMessage serverMessage = new ServerMessage();
                                serverMessage.Init(
                                    LibraryParser.OutgoingRequest("UpdateFloorItemExtraDataMessageComposer"));
                                serverMessage.AppendString(roomItem.Id.ToString());
                                serverMessage.AppendInteger(0);
                                serverMessage.AppendString("0");
                                roomUsers.GetClient().SendMessage(serverMessage);
                                roomUsers.OnCampingTent = false;
                            }
                            break;

                        case Interaction.None:
                            break;
                    }
                }

                // Let's Update user Status..
                UpdateUserStatus(roomUsers, true);
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Turns the user thread
        /// </summary>
        /// <param name="roomUsers"></param>
        internal void UserCycleOnRoom(RoomUser roomUsers)
        {
            // Region Check User Elegibility
            if (!IsValid(roomUsers))
            {
                if (roomUsers.GetClient() != null)
                    RemoveUserFromRoom(roomUsers.GetClient(), false, false);
                else
                    RemoveRoomUser(roomUsers);
            }

            // Region Check User Remove Unlocking
            lock (_removeUsers)
            {
                if (roomUsers.NeedsAutokick && !_removeUsers.Contains(roomUsers))
                {
                    _removeUsers.Add(roomUsers);
                    return;
                }
            }

            // Region Idle and Room Tiem Check
            roomUsers.IdleTime++;

            // Region User Achievement of Room
            UserRoomTimeCycles(roomUsers);

            // Carry Item Hand Checking
            if (roomUsers.CarryItemId > 0)
            {
                roomUsers.CarryTimer--;

                // If The Carry Timer is 0.. Remove CarryItem.
                if (roomUsers.CarryTimer <= 0)
                    roomUsers.CarryItem(0);
            }

            // Region Check User Got Freezed
            if (_userRoom.GotFreeze())
                Freeze.CycleUser(roomUsers);

            // Region Variable Registering
            bool invalidStep = false;
            // Region Check User Tile Selection
            if (roomUsers.SetStep)
            {
                // Check if User is Going to the Door.
                lock (_removeUsers)
                {
                    if ((roomUsers.SetX == _userRoom.GetGameMap().Model.DoorX) &&
                        (roomUsers.SetY == _userRoom.GetGameMap().Model.DoorY) && !_removeUsers.Contains(roomUsers) &&
                        !roomUsers.IsBot && !roomUsers.IsPet)
                    {
                        _removeUsers.Add(roomUsers);
                        return;
                    }
                }

                // Check Elegibility of Walk In Tile
                invalidStep = UserCanWalkInTile(roomUsers);

                // User isn't Anymore Set a Tile to Walk
                roomUsers.SetStep = false;
            }

            // Pet Must Stop Too!
            if ((roomUsers.GoalX == roomUsers.X) && (roomUsers.GoalY == roomUsers.Y) && roomUsers.RidingHorse &&
                !roomUsers.IsPet)
            {
                RoomUser horseStopWalkRidingPet = GetRoomUserByVirtualId(Convert.ToInt32(roomUsers.HorseId));

                if (horseStopWalkRidingPet != null)
                {
                    ServerMessage horseStopWalkRidingPetMessage =
                        new ServerMessage(LibraryParser.OutgoingRequest("UpdateUserStatusMessageComposer"));
                    horseStopWalkRidingPetMessage.AppendInteger(1);
                    horseStopWalkRidingPet.SerializeStatus(horseStopWalkRidingPetMessage, "");
                    _userRoom.SendMessage(horseStopWalkRidingPetMessage);

                    horseStopWalkRidingPet.IsWalking = false;
                    horseStopWalkRidingPet.ClearMovement();
                }
            }

            // User Reached Goal Need Stop.
            if (((roomUsers.GoalX == roomUsers.X) && (roomUsers.GoalY == roomUsers.Y)) || roomUsers.Freezed)
            {
                roomUsers.IsWalking = false;
                roomUsers.ClearMovement();
                roomUsers.SetStep = false;
                UpdateUserStatus(roomUsers, false);
            }

            // Check if Proably the Pathfinder is with Some Errors..
            if (roomUsers.PathRecalcNeeded)
            {
                roomUsers.Path.Clear();
                roomUsers.Path = PathFinder.FindPath(roomUsers, _userRoom.GetGameMap().DiagonalEnabled,
                    _userRoom.GetGameMap(), new Vector2D(roomUsers.X, roomUsers.Y),
                    new Vector2D(roomUsers.GoalX, roomUsers.GoalY));

                if (roomUsers.Path.Count > 1)
                {
                    roomUsers.PathStep = 1;
                    roomUsers.IsWalking = true;
                    roomUsers.PathRecalcNeeded = false;
                }
                else
                {
                    roomUsers.PathRecalcNeeded = false;
                    roomUsers.Path.Clear();
                }
            }

            // If user Isn't Walking, Let's go Back..
            if (!roomUsers.IsWalking || roomUsers.Freezed)
            {
                if (roomUsers.Statusses.ContainsKey("mv"))
                    roomUsers.ClearMovement();
            }
            else
            {
                // If he Want's to Walk.. Let's Continue!..

                // Let's go to The Tile! And Walk :D
                UserGoToTile(roomUsers, invalidStep);

                // If User isn't Riding, Must Update Statusses...
                if (!roomUsers.RidingHorse)
                    roomUsers.UpdateNeeded = true;
            }

            // If is a Bot.. Let's Tick the Time Count of Bot..
            if (roomUsers.IsBot)
                roomUsers.BotAi.OnTimerTick();

            UpdateUserEffect(roomUsers, roomUsers.X, roomUsers.Y);
        }

        /// <summary>
        ///     Called when [cycle].
        /// </summary>
        /// <param name="idleCount">The idle count.</param>
        internal void OnCycle(ref int idleCount)
        {
            // User in Room Count for Foreach..
            uint userInRoomCount = 0;

            // Clear RemoveUser's List.
            lock (_removeUsers)
                _removeUsers.Clear();

            try
            {
                // Check Disco Procedure...
                if ((_userRoom != null) && _userRoom.DiscoMode && (_userRoom.TonerData != null) &&
                    (_userRoom.TonerData.Enabled == 1))
                {
                    RoomItem tonerItem = _userRoom.GetRoomItemHandler().GetItem(_userRoom.TonerData.ItemId);

                    if (tonerItem != null)
                    {
                        _userRoom.TonerData.Data1 = Yupi.GetRandomNumber(0, 255);
                        _userRoom.TonerData.Data2 = Yupi.GetRandomNumber(0, 255);
                        _userRoom.TonerData.Data3 = Yupi.GetRandomNumber(0, 255);

                        ServerMessage tonerComposingMessage =
                            new ServerMessage(LibraryParser.OutgoingRequest("UpdateRoomItemMessageComposer"));

                        tonerItem.Serialize(tonerComposingMessage);
                        _userRoom.SendMessage(tonerComposingMessage);
                    }
                }
            }
            catch (Exception e)
            {
                ServerLogManager.LogException("Disco mode: " + e);
            }

            // Region: Main User Procedure... Really Main..
            foreach (RoomUser roomUsers in UserList.Values)
            {
                // User Main OnCycle
                UserCycleOnRoom(roomUsers);

                // If is a Valid user, We must increase the User Count..
                if (!roomUsers.IsPet && !roomUsers.IsBot)
                    userInRoomCount++;
            }

            // Region: Check Removable Users and Users in Room Count
            lock (_removeUsers)
            {
                // Check Users to be Removed from Room
                foreach (RoomUser userToRemove in _removeUsers)
                {
                    GameClient userRemovableClient = Yupi.GetGame().GetClientManager().GetClientByUserId(userToRemove.HabboId);

                    // Remove User from Room..
                    if (userRemovableClient != null)
                        RemoveUserFromRoom(userRemovableClient, true, false);
                    else
                        RemoveRoomUser(userToRemove);
                }

                if (userInRoomCount == 0)
                    idleCount++;

                if (_roomUserCount != userInRoomCount)
                    UpdateUserCount(userInRoomCount);
            }
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        internal void Destroy()
        {
            _userRoom = null;
            UsersByUserName.Clear();
            UsersByUserName = null;
            UsersByUserId.Clear();
            UsersByUserId = null;
            OnUserEnter = null;
            _pets.Clear();
            _bots.Clear();
            _pets = null;
            _bots = null;
            UserList = null;
        }

        /// <summary>
        ///     Updates the user effect.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        private void UpdateUserEffect(RoomUser user, int x, int y)
        {
            if (user.IsBot)
                return;
            try
            {
                byte b = _userRoom.GetGameMap().EffectMap[x, y];
                if (b > 0)
                {
                    if (user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().CurrentEffect == 0)
                        user.CurrentItemEffect = ItemEffectType.None;
                    ItemEffectType itemEffectType = ByteToItemEffectEnum.Parse(b);
                    if (itemEffectType == user.CurrentItemEffect)
                        return;
                    switch (itemEffectType)
                    {
                        case ItemEffectType.None:
                            user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(-1);
                            user.CurrentItemEffect = itemEffectType;
                            break;

                        case ItemEffectType.Swim:
                            user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(28);
                            user.CurrentItemEffect = itemEffectType;
                            break;

                        case ItemEffectType.SwimLow:
                            user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(30);
                            user.CurrentItemEffect = itemEffectType;
                            break;

                        case ItemEffectType.SwimHalloween:
                            user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(37);
                            user.CurrentItemEffect = itemEffectType;
                            break;

                        case ItemEffectType.Iceskates:
                            user.GetClient()
                                .GetHabbo()
                                .GetAvatarEffectsInventoryComponent()
                                .ActivateCustomEffect(user.GetClient().GetHabbo().Gender.ToUpper() == "M" ? 38 : 39);
                            user.CurrentItemEffect = ItemEffectType.Iceskates;
                            break;

                        case ItemEffectType.Normalskates:
                            user.GetClient()
                                .GetHabbo()
                                .GetAvatarEffectsInventoryComponent()
                                .ActivateCustomEffect(user.GetClient().GetHabbo().Gender.ToUpper() == "M" ? 55 : 56);
                            user.CurrentItemEffect = itemEffectType;
                            break;

                        case ItemEffectType.SnowBoard:
                        {
                            user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(97);
                            user.CurrentItemEffect = itemEffectType;
                        }
                            break;
                    }
                }
                else
                {
                    if (user.CurrentItemEffect == ItemEffectType.None || b != 0)
                        return;
                    user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(-1);
                    user.CurrentItemEffect = ItemEffectType.None;
                }
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     Handles the <see cref="E:UserAdd" /> event.
        /// </summary>
        /// <param name="user"></param>
        private void OnUserAdd(RoomUser user)
        {
            try
            {
                if (user?.GetClient() == null || user.GetClient().GetHabbo() == null)
                    return;

                GameClient client = user.GetClient();

                if (client?.GetHabbo() == null || _userRoom == null)
                    return;

                if (!user.IsSpectator)
                {
                    DynamicRoomModel model = _userRoom.GetGameMap().Model;

                    if (model == null)
                        return;

                    user.SetPos(model.DoorX, model.DoorY, model.DoorZ);
                    user.SetRot(model.DoorOrientation, false);

                    user.AddStatus(_userRoom.CheckRights(client, true) ? "flatctrl 4" : "flatctrl 1", string.Empty);

                    user.CurrentItemEffect = ItemEffectType.None;

                    if (!user.IsBot && client.GetHabbo().IsTeleporting)
                    {
                        client.GetHabbo().IsTeleporting = false;
                        client.GetHabbo().TeleportingRoomId = 0;

                        RoomItem item = _userRoom.GetRoomItemHandler().GetItem(client.GetHabbo().TeleporterId);

                        if (item != null)
                        {
                            item.ExtraData = "2";
                            item.UpdateState(false, true);
                            user.SetPos(item.X, item.Y, item.Z);
                            user.SetRot(item.Rot, false);
                            item.InteractingUser2 = client.GetHabbo().Id;
                            item.ExtraData = "0";
                            item.UpdateState(false, true);
                        }
                    }

                    if (!user.IsBot && client.GetHabbo().IsHopping)
                    {
                        client.GetHabbo().IsHopping = false;
                        client.GetHabbo().HopperId = 0;

                        RoomItem item2 = _userRoom.GetRoomItemHandler().GetItem(client.GetHabbo().HopperId);

                        if (item2 != null)
                        {
                            item2.ExtraData = "1";
                            item2.UpdateState(false, true);
                            user.SetPos(item2.X, item2.Y, item2.Z);
                            user.SetRot(item2.Rot, false);
                            user.AllowOverride = false;
                            item2.InteractingUser2 = client.GetHabbo().Id;
                            item2.ExtraData = "2";
                            item2.UpdateState(false, true);
                        }
                    }

                    if (!user.IsSpectator)
                    {
                        ServerMessage serverMessage =
                            new ServerMessage(LibraryParser.OutgoingRequest("SetRoomUserMessageComposer"));
                        serverMessage.AppendInteger(1);
                        user.Serialize(serverMessage, _userRoom.GetGameMap().GotPublicPool);
                        _userRoom.SendMessage(serverMessage);
                    }

                    if (!user.IsBot && !user.IsPet)
                    {
                        ServerMessage serverMessage2 = new ServerMessage();
                        serverMessage2.Init(LibraryParser.OutgoingRequest("UpdateUserDataMessageComposer"));
                        serverMessage2.AppendInteger(user.VirtualId);
                        serverMessage2.AppendString(client.GetHabbo().Look);
                        serverMessage2.AppendString(client.GetHabbo().Gender.ToLower());
                        serverMessage2.AppendString(client.GetHabbo().Motto);
                        serverMessage2.AppendInteger(client.GetHabbo().AchievementPoints);
                        _userRoom.SendMessage(serverMessage2);
                    }

                    if (_userRoom.RoomData.Owner != client.GetHabbo().UserName)
                        Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(client, "ACH_RoomEntry", 1);
                }

                if (client.GetHabbo().GetMessenger() != null)
                    client.GetHabbo().GetMessenger().OnStatusChanged(true);

                client.GetMessageHandler().OnRoomUserAdd();

                //if (client.GetHabbo().HasFuse("fuse_mod")) client.GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(102);
                //if (client.GetHabbo().Rank == Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"])) client.GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(178);

                OnUserEnter?.Invoke(user, null);

                if (_userRoom.GotMusicController() && _userRoom.GotMusicController())
                    _userRoom.GetRoomMusicController().OnNewUserEnter(user);

                _userRoom.OnUserEnter(user);
            }
            catch (Exception ex)
            {
                ServerLogManager.LogCriticalException(ex.ToString());
            }
        }

        /// <summary>
        ///     Handles the <see cref="E:Remove" /> event.
        /// </summary>
        /// <param name="user"></param>
        private void OnRemove(RoomUser user)
        {
            try
            {
                if (user == null || user.GetClient() == null) return;
                GameClient client = user.GetClient();
                IEnumerable<RoomUser> list = UserList.Values.Where(current => current.IsBot && !current.IsPet && current.BotAi != null);
                List<RoomUser> list2 = new List<RoomUser>();

                List<RoomItem> userOnCurrentItem = _userRoom.GetGameMap().GetCoordinatedItems(new Point(user.X, user.Y));
                foreach (RoomItem roomItem in userOnCurrentItem.ToArray())
                {
                    switch (roomItem.GetBaseItem().InteractionType)
                    {
                        case Interaction.RunWaySage:
                        case Interaction.ChairState:
                        case Interaction.Shower:
                        case Interaction.PressurePad:
                        case Interaction.PressurePadBed:
                        case Interaction.Guillotine:
                            roomItem.ExtraData = "0";
                            roomItem.UpdateState();
                            break;
                    }
                }

                foreach (RoomUser bot in list)
                {
                    bot.BotAi.OnUserLeaveRoom(client);
                    if (bot.IsPet && bot.PetData.OwnerId == user.UserId &&
                        !_userRoom.CheckRights(client, true))
                        list2.Add(bot);
                }
                foreach (
                    RoomUser current3 in
                        list2.Where(
                            current3 =>
                                user.GetClient() != null && user.GetClient().GetHabbo() != null &&
                                user.GetClient().GetHabbo().GetInventoryComponent() != null))
                {
                    user.GetClient().GetHabbo().GetInventoryComponent().AddPet(current3.PetData);
                    RemoveBot(current3.VirtualId, false);
                }
                _userRoom.GetGameMap().RemoveUserFromMap(user, new Point(user.X, user.Y));
            }
            catch (Exception ex)
            {
                ServerLogManager.LogCriticalException(ex.ToString());
            }
        }

        /// <summary>
        ///     Called when [user update status].
        /// </summary>
        public void OnUserUpdateStatus()
        {
            foreach (RoomUser current in UserList.Values)
                UpdateUserStatus(current, false);
        }

        /// <summary>
        ///     Called when [user update status].
        ///     <param name="x">x position</param>
        ///     <param name="y">y position</param>
        /// </summary>
        public void OnUserUpdateStatus(int x, int y)
        {
            foreach (RoomUser current in UserList.Values.Where(current => current.X == x && current.Y == y))
                UpdateUserStatus(current, false);
        }

        /// <summary>
        ///     Determines whether the specified user is valid.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if the specified user is valid; otherwise, <c>false</c>.</returns>
        private bool IsValid(RoomUser user)
        {
            return user != null && (user.IsBot ||
                                    (user.GetClient() != null && user.GetClient().GetHabbo() != null &&
                                     user.GetClient().GetHabbo().CurrentRoomId == _userRoom.RoomId));
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using Yupi.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Catalogs;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Datas;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired;
using Yupi.Game.Pets;
using Yupi.Game.RoomBots;
using Yupi.Game.RoomBots.Enumerators;
using Yupi.Game.Rooms.Chat;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Rooms.Items.Games;
using Yupi.Game.Rooms.Items.Games.Handlers;
using Yupi.Game.Rooms.Items.Games.Teams;
using Yupi.Game.Rooms.Items.Games.Types.Banzai;
using Yupi.Game.Rooms.Items.Games.Types.Freeze;
using Yupi.Game.Rooms.Items.Games.Types.Soccer;
using Yupi.Game.Rooms.Items.Handlers;
using Yupi.Game.Rooms.RoomInvokedItems;
using Yupi.Game.Rooms.User;
using Yupi.Game.Rooms.User.Path;
using Yupi.Game.Rooms.User.Trade;
using Yupi.Game.SoundMachine;
using Yupi.Game.SoundMachine.Songs;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Rooms
{
    /// <summary>
    ///     Class Room.
    /// </summary>
    public class Room
    {
        /// <summary>
        ///     The _banzai
        /// </summary>
        private BattleBanzai _banzai;

        /// <summary>
        ///     The _freeze
        /// </summary>
        private Freeze _freeze;

        /// <summary>
        ///     The _game
        /// </summary>
        private GameManager _game;

        /// <summary>
        ///     The _game item handler
        /// </summary>
        private GameItemHandler _gameItemHandler;

        /// <summary>
        ///     The _game map
        /// </summary>
        private Gamemap _gameMap;

        /// <summary>
        ///     The _idle time
        /// </summary>
        private int _idleTime;

        /// <summary>
        ///     The _is crashed
        /// </summary>
        private bool _isCrashed;

        /// <summary>
        ///     The _m cycle ended
        /// </summary>
        private bool _mCycleEnded;

        /// <summary>
        ///     The _music controller
        /// </summary>
        private SoundMachineManager _musicController;

        /// <summary>
        ///     The _process timer
        /// </summary>
        private Timer _processTimer;

        /// <summary>
        ///     The _room item handling
        /// </summary>
        private RoomItemHandler _roomItemHandler;

        /// <summary>
        ///     The _room kick
        /// </summary>
        private Queue _roomKick;

        /// <summary>
        ///     The _room thread
        /// </summary>
        private Thread _roomThread;

        /// <summary>
        ///     The _room user manager
        /// </summary>
        private RoomUserManager _roomUserManager;

        /// <summary>
        ///     The _soccer
        /// </summary>
        private Soccer _soccer;

        /// <summary>
        ///     The _wired handler
        /// </summary>
        private WiredHandler _wiredHandler;

        /// <summary>
        ///     The active trades
        /// </summary>
        internal ArrayList ActiveTrades;

        /// <summary>
        ///     The bans
        /// </summary>
        internal Dictionary<long, double> Bans;

        /// <summary>
        ///     The _containsBeds count of bed on room
        /// </summary>
        internal int ContainsBeds;

        /// <summary>
        ///     The _m disposed
        /// </summary>
        public bool Disposed;

        /// <summary>
        ///     The everyone got rights
        /// </summary>
        internal bool EveryoneGotRights, RoomMuted;

        /// <summary>
        ///     The just loaded
        /// </summary>
        public bool JustLoaded = true;

        internal DateTime LastTimerReset;

        /// <summary>
        ///     The loaded groups
        /// </summary>
        internal Dictionary<uint, string> LoadedGroups;

        /// <summary>
        ///     The moodlight data
        /// </summary>
        internal MoodlightData MoodlightData;

        /// <summary>
        ///     The muted bots
        /// </summary>
        internal bool MutedBots, DiscoMode, MutedPets;

        /// <summary>
        ///     The muted users
        /// </summary>
        internal Dictionary<uint, uint> MutedUsers;

        /// <summary>
        ///     The team banzai
        /// </summary>
        internal TeamManager TeamBanzai;

        /// <summary>
        ///     The team freeze
        /// </summary>
        internal TeamManager TeamFreeze;

        /// <summary>
        ///     The toner data
        /// </summary>
        internal TonerData TonerData;

        /// <summary>
        ///     The users with rights
        /// </summary>
        internal List<uint> UsersWithRights;

        /// <summary>
        ///     The word filter
        /// </summary>
        internal List<string> WordFilter;

        /// <summary>
        ///     Gets the user count.
        /// </summary>
        /// <value>The user count.</value>
        internal int UserCount => _roomUserManager?.GetRoomUserCount() ?? 0;

        /// <summary>
        ///     Gets the tag count.
        /// </summary>
        /// <value>The tag count.</value>
        internal int TagCount => RoomData.Tags.Count;

        /// <summary>
        ///     Gets the room identifier.
        /// </summary>
        /// <value>The room identifier.</value>
        internal uint RoomId { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance can trade in room.
        /// </summary>
        /// <value><c>true</c> if this instance can trade in room; otherwise, <c>false</c>.</value>
        internal bool CanTradeInRoom => true;

        /// <summary>
        ///     Gets the room data.
        /// </summary>
        /// <value>The room data.</value>
        internal RoomData RoomData { get; private set; }

        internal void Start(RoomData data, bool forceLoad = false)
        {
            InitializeFromRoomData(data, forceLoad);
            GetRoomItemHandler().LoadFurniture();
            GetGameMap().GenerateMaps();
        }

        /// <summary>
        ///     Gets the wired handler.
        /// </summary>
        /// <returns>WiredHandler.</returns>
        public WiredHandler GetWiredHandler()
        {
            return _wiredHandler ?? (_wiredHandler = new WiredHandler(this));
        }

        /// <summary>
        ///     Gets the game map.
        /// </summary>
        /// <returns>Gamemap.</returns>
        internal Gamemap GetGameMap()
        {
            return _gameMap;
        }

        /// <summary>
        ///     Gets the room item handler.
        /// </summary>
        /// <returns>RoomItemHandler.</returns>
        internal RoomItemHandler GetRoomItemHandler()
        {
            return _roomItemHandler;
        }

        /// <summary>
        ///     Gets the room user manager.
        /// </summary>
        /// <returns>RoomUserManager.</returns>
        internal RoomUserManager GetRoomUserManager()
        {
            return _roomUserManager;
        }

        /// <summary>
        ///     Gets the soccer.
        /// </summary>
        /// <returns>Soccer.</returns>
        internal Soccer GetSoccer()
        {
            return _soccer ?? (_soccer = new Soccer(this));
        }

        /// <summary>
        ///     Gets the team manager for banzai.
        /// </summary>
        /// <returns>TeamManager.</returns>
        internal TeamManager GetTeamManagerForBanzai()
        {
            return TeamBanzai ?? (TeamBanzai = TeamManager.CreateTeamforGame("banzai"));
        }

        /// <summary>
        ///     Gets the team manager for freeze.
        /// </summary>
        /// <returns>TeamManager.</returns>
        internal TeamManager GetTeamManagerForFreeze()
        {
            return TeamFreeze ?? (TeamFreeze = TeamManager.CreateTeamforGame("freeze"));
        }

        /// <summary>
        ///     Gets the banzai.
        /// </summary>
        /// <returns>BattleBanzai.</returns>
        internal BattleBanzai GetBanzai()
        {
            return _banzai ?? (_banzai = new BattleBanzai(this));
        }

        /// <summary>
        ///     Gets the freeze.
        /// </summary>
        /// <returns>Freeze.</returns>
        internal Freeze GetFreeze()
        {
            return _freeze ?? (_freeze = new Freeze(this));
        }

        /// <summary>
        ///     Gets the game manager.
        /// </summary>
        /// <returns>GameManager.</returns>
        internal GameManager GetGameManager()
        {
            return _game ?? (_game = new GameManager(this));
        }

        /// <summary>
        ///     Gets the game item handler.
        /// </summary>
        /// <returns>GameItemHandler.</returns>
        internal GameItemHandler GetGameItemHandler()
        {
            return _gameItemHandler ?? (_gameItemHandler = new GameItemHandler(this));
        }

        /// <summary>
        ///     Gets the room music controller.
        /// </summary>
        /// <returns>RoomMusicController.</returns>
        internal SoundMachineManager GetRoomMusicController()
        {
            return _musicController ?? (_musicController = new SoundMachineManager());
        }

        /// <summary>
        ///     Gots the music controller.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool GotMusicController()
        {
            return _musicController != null;
        }

        /// <summary>
        ///     Gots the soccer.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool GotSoccer()
        {
            return _soccer != null;
        }

        /// <summary>
        ///     Gots the banzai.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool GotBanzai()
        {
            return _banzai != null;
        }

        /// <summary>
        ///     Gots the freeze.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool GotFreeze()
        {
            return _freeze != null;
        }

        /// <summary>
        ///     Starts the room processing.
        /// </summary>
        internal void StartRoomProcessing()
        {
            _processTimer = new Timer(ProcessRoom, null, 500, 500);
        }

        /// <summary>
        ///     Initializes the user bots.
        /// </summary>
        internal void InitUserBots()
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"SELECT * FROM bots_data WHERE room_id = {RoomId} AND ai_type = 'generic'");

                DataTable table = commitableQueryReactor.GetTable();

                if (table == null)
                    return;

                foreach (RoomBot roomBot in from DataRow dataRow in table.Rows select BotManager.GenerateBotFromRow(dataRow)
                    )
                    _roomUserManager.DeployBot(roomBot, null);
            }
        }

        /// <summary>
        ///     Clears the tags.
        /// </summary>
        internal void ClearTags()
        {
            RoomData.Tags.Clear();
        }

        /// <summary>
        ///     Adds the tag range.
        /// </summary>
        /// <param name="tags">The tags.</param>
        internal void AddTagRange(List<string> tags)
        {
            RoomData.Tags.AddRange(tags);
        }

        /// <summary>
        ///     Initializes the bots.
        /// </summary>
        internal void InitBots()
        {
            List<RoomBot> botsForRoom = Yupi.GetGame().GetBotManager().GetBotsForRoom(RoomId);
            foreach (RoomBot current in botsForRoom.Where(current => !current.IsPet))
                DeployBot(current);
        }

        /// <summary>
        ///     Initializes the pets.
        /// </summary>
        internal void InitPets()
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery($"SELECT * FROM pets_data WHERE room_id = {RoomId}");

                DataTable table = commitableQueryReactor.GetTable();

                if (table == null)
                    return;

                foreach (DataRow dataRow in table.Rows)
                {
                    Pet pet = CatalogManager.GeneratePetFromRow(dataRow);

                    RoomBot bot = new RoomBot(pet.PetId, Convert.ToUInt32(RoomData.OwnerId), AiType.Pet, string.Empty);

                    bot.Update(RoomId, "freeroam", pet.Name, string.Empty, pet.Look, pet.X, pet.Y, (int) pet.Z, 4, 0, 0,
                        0, 0,
                        null, null, string.Empty, 0, 0, false, false);

                    _roomUserManager.DeployBot(bot, pet);
                }
            }
        }

        /// <summary>
        ///     Deploys the bot.
        /// </summary>
        /// <param name="bot">The bot.</param>
        /// <returns>RoomUser.</returns>
        internal RoomUser DeployBot(RoomBot bot)
        {
            return _roomUserManager.DeployBot(bot, null);
        }

        /// <summary>
        ///     Queues the room kick.
        /// </summary>
        /// <param name="kick">The kick.</param>
        internal void QueueRoomKick(RoomKick kick)
        {
            lock (_roomKick.SyncRoot)
            {
                _roomKick.Enqueue(kick);
            }
        }

        /// <summary>
        ///     Called when [room kick].
        /// </summary>
        internal void OnRoomKick()
        {
            List<RoomUser> list = _roomUserManager.UserList.Values.Where(
                current => !current.IsBot && current.GetClient().GetHabbo().Rank < 4u).ToList();

            {
                foreach (RoomUser t in list)
                {
                    GetRoomUserManager().RemoveUserFromRoom(t.GetClient(), true, false);
                    t.GetClient().CurrentRoomUserId = -1;
                }
            }
        }

        /// <summary>
        ///     Called when [user enter].
        /// </summary>
        /// <param name="user">The user.</param>
        internal void OnUserEnter(RoomUser user)
        {
            GetWiredHandler().ExecuteWired(Interaction.TriggerRoomEnter, user);

            //int count = 0;

            //todo: why counting?

            foreach (RoomUser current in _roomUserManager.UserList.Values)
            {
                if (current.IsBot || current.IsPet)
                {
                    current.BotAi.OnUserEnterRoom(user);
                    //count++;
                }

                //if (count >= 3)
                //    break;
            }
        }

        /// <summary>
        ///     Called when [user say].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        /// <param name="shout">if set to <c>true</c> [shout].</param>
        internal void OnUserSay(RoomUser user, string message, bool shout)
        {
            foreach (RoomUser current in _roomUserManager.UserList.Values)
            {
                try
                {
                    if (!current.IsBot && !current.IsPet)
                        continue;

                    if (!current.IsPet && message.ToLower().StartsWith(current.BotData.Name.ToLower()))
                    {
                        message = message.Substring(current.BotData.Name.Length);

                        if (shout)
                            current.BotAi.OnUserShout(user, message);
                        else
                            current.BotAi.OnUserSay(user, message);
                    }
                    // @issue #80
                    else if (!current.IsPet && !current.BotAi.GetBotData().AutomaticChat)
                    {
                        current.BotAi.OnChatTick();
                    }
                    else if (current.IsPet && message.StartsWith(current.PetData.Name) &&
                             current.PetData.Type != "pet_monster")
                    {
                        message = message.Substring(current.PetData.Name.Length);
                        current.BotAi.OnUserSay(user, message);
                    }
                }
                catch (Exception)
                {
                    //return?
                    break;
                }
            }
        }

        /// <summary>
        ///     Loads the music.
        /// </summary>
        internal void LoadMusic()
        {
            DataTable table;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"SELECT items_songs.songid,items_rooms.id,items_rooms.item_name FROM items_songs LEFT JOIN items_rooms ON items_rooms.id = items_songs.itemid WHERE items_songs.roomid = {RoomId}");

                table = commitableQueryReactor.GetTable();
            }

            if (table == null)
                return;

            foreach (DataRow dataRow in table.Rows)
            {
                uint songId = (uint) dataRow["songid"];
                uint num = Convert.ToUInt32(dataRow["id"]);
                string baseName = dataRow["item_name"].ToString();
                string songCode = string.Empty;
                string extraData = string.Empty;

                using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryreactor2.SetQuery($"SELECT extra_data,songcode FROM items_rooms WHERE id = {num}");

                    DataRow row = queryreactor2.GetRow();

                    if (row != null)
                    {
                        extraData = (string) row["extra_data"];
                        songCode = (string) row["songcode"];
                    }
                }

                SongItem diskItem = new SongItem(num, songId, baseName, extraData, songCode);

                GetRoomMusicController().AddDisk(diskItem);
            }
        }

        /// <summary>
        ///     Loads the rights.
        /// </summary>
        internal void LoadRights()
        {
            UsersWithRights = new List<uint>();
            DataTable dataTable;
            if (RoomData.Group != null)
                return;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"SELECT rooms_rights.user_id FROM rooms_rights WHERE room_id = {RoomId}");
                dataTable = commitableQueryReactor.GetTable();
            }
            if (dataTable == null)
                return;
            foreach (DataRow dataRow in dataTable.Rows)
                UsersWithRights.Add(Convert.ToUInt32(dataRow["user_id"]));
        }

        /// <summary>
        ///     Loads the bans.
        /// </summary>
        internal void LoadBans()
        {
            Bans = new Dictionary<long, double>();
            DataTable table;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery($"SELECT user_id, expire FROM rooms_bans WHERE room_id = {RoomId}");
                table = commitableQueryReactor.GetTable();
            }
            if (table == null)
                return;
            foreach (DataRow dataRow in table.Rows)
                Bans.Add((uint) dataRow[0], Convert.ToDouble(dataRow[1]));
        }

        /// <summary>
        ///     Checks the rights.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool CheckRights(GameClient session)
        {
            return CheckRights(session, false);
        }

        /// <summary>
        ///     Checks the rights.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="requireOwnerShip">if set to <c>true</c> [require ownership].</param>
        /// <param name="checkForGroups">if set to <c>true</c> [check for groups].</param>
        /// <param name="groupMembers"></param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool CheckRights(GameClient session, bool requireOwnerShip = false, bool checkForGroups = false,
            bool groupMembers = false)
        {
            try
            {
                if (session == null || session.GetHabbo() == null) return false;
                if (session.GetHabbo().UserName == RoomData.Owner && RoomData.Type == "private") return true;
                if (session.GetHabbo().HasFuse("fuse_admin") || session.GetHabbo().HasFuse("fuse_any_room_controller"))
                    return true;

                if (RoomData.Type != "private") return false;

                if (!requireOwnerShip)
                {
                    if (session.GetHabbo().HasFuse("fuse_any_rooms_rights")) return true;
                    if (EveryoneGotRights ||
                        (UsersWithRights != null && UsersWithRights.Contains(session.GetHabbo().Id))) return true;
                }
                else return false;

                if (RoomData.Group == null) return false;

                if (groupMembers)
                {
                    if (RoomData.Group.Admins.ContainsKey(session.GetHabbo().Id)) return true;
                    if (RoomData.Group.Members.ContainsKey(session.GetHabbo().Id)) return true;
                }
                else if (checkForGroups)
                {
                    if (RoomData.Group.Admins.ContainsKey(session.GetHabbo().Id)) return true;
                    if (RoomData.Group.AdminOnlyDeco == 0u && RoomData.Group.Members.ContainsKey(session.GetHabbo().Id))
                        return true;
                }
            }
            catch (Exception e)
            {
                ServerLogManager.LogException(e, "Yupi.Game.Rooms.Room.CheckRights");
            }

            return false;
        }

        /// <summary>
        ///     Checks the rights DoorBell
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="requireOwnerShip">if set to <c>true</c> [require ownership].</param>
        /// <param name="checkForGroups">if set to <c>true</c> [check for groups].</param>
        /// <param name="groupMembers"></param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool CheckRightsDoorBell(GameClient session, bool requireOwnerShip = false, bool checkForGroups = false,
            bool groupMembers = false)
        {
            try
            {
                if (session == null || session.GetHabbo() == null) return false;
                if (session.GetHabbo().UserName == RoomData.Owner && RoomData.Type == "private") return true;
                if (session.GetHabbo().HasFuse("fuse_admin") || session.GetHabbo().HasFuse("fuse_any_room_controller"))
                    return true;

                if (RoomData.Type != "private") return false;

                if (!requireOwnerShip)
                {
                    if (session.GetHabbo().HasFuse("fuse_any_rooms_rights")) return true;
                    if (EveryoneGotRights ||
                        (UsersWithRights != null && UsersWithRights.Contains(session.GetHabbo().Id))) return true;
                }
                else if (RoomData.Group != null)
                {
                    if (groupMembers)
                    {
                        if (RoomData.Group.Admins.ContainsKey(session.GetHabbo().Id)) return true;
                        if (RoomData.Group.Members.ContainsKey(session.GetHabbo().Id)) return true;
                    }
                    else if (checkForGroups)
                    {
                        if (RoomData.Group.Admins.ContainsKey(session.GetHabbo().Id)) return true;
                        if (RoomData.Group.AdminOnlyDeco == 0u &&
                            RoomData.Group.Members.ContainsKey(session.GetHabbo().Id)) return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                ServerLogManager.LogException(e, "Yupi.Game.Rooms.Room.CheckRights");
            }
            return false;
        }

        /// <summary>
        ///     Processes the room.
        /// </summary>
        internal void ProcessRoom(object callItem)
        {
            try
            {
                if (_isCrashed || Disposed || Yupi.ShutdownStarted)
                    return;
                try
                {
                    int idle = 0;
                    GetRoomItemHandler().OnCycle();
                    GetRoomUserManager().OnCycle(ref idle);

                    if (idle > 0)
                        _idleTime++;
                    else
                        _idleTime = 0;

                    if (!_mCycleEnded)
                    {
                        if ((_idleTime >= 25 && !JustLoaded) || (_idleTime >= 100 && JustLoaded))
                        {
                            Yupi.GetGame().GetRoomManager().UnloadRoom(this, "No users");
                            return;
                        }

                        ServerMessage serverMessage = GetRoomUserManager().SerializeStatusUpdates(false);

                        if (serverMessage != null)
                            SendMessage(serverMessage);
                    }

                    _gameItemHandler?.OnCycle();

                    _game?.OnCycle();

                    if (GotBanzai())
                        _banzai.OnCycle();

                    if (GotSoccer())
                        _soccer.OnCycle();

                    if (GetRoomMusicController() != null)
                        GetRoomMusicController().Update(this);

                    GetWiredHandler().OnCycle();
                    WorkRoomKickQueue();
                }
                catch (Exception e)
                {
                    ServerLogManager.LogException(e.ToString());
                    OnRoomCrash(e);
                }
            }
            catch (Exception e)
            {
                ServerLogManager.LogCriticalException($"Sub crash in room cycle: {e}");
            }
        }

        /// <summary>
        ///     Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void SendMessage(byte[] message)
        {
            try
            {
                foreach (RoomUser user in _roomUserManager.UserList.Values)
                {
                    if (user.IsBot)
                        continue;

                    GameClient usersClient = user.GetClient();

                    if (usersClient?.GetConnection() == null)
                        continue;

                    usersClient.GetConnection().SendData(message);
                }
            }
            catch (Exception e)
            {
                ServerLogManager.LogException(e, "Yupi.Game.Rooms.Room.SendMessgae");
            }
        }

        /// <summary>
        ///     Broadcasts the chat message.
        /// </summary>
        /// <param name="chatMsg">The chat MSG.</param>
        /// <param name="roomUser">The room user.</param>
        /// <param name="p">The p.</param>
        internal void BroadcastChatMessage(ServerMessage chatMsg, RoomUser roomUser, uint p)
        {
            try
            {
                byte[] packetData = chatMsg.GetReversedBytes();

                foreach (RoomUser user in _roomUserManager.UserList.Values)
                {
                    if (user.IsBot || user.IsPet)
                        continue;

                    GameClient usersClient = user.GetClient();
                    if (usersClient == null || roomUser == null || usersClient.GetHabbo() == null)
                        continue;

                    try
                    {
                        if (user.OnCampingTent || !roomUser.OnCampingTent)
                        {
                            if (!usersClient.GetHabbo().MutedUsers.Contains(p))
                                usersClient.SendMessage(packetData);
                        }
                    }
                    catch (Exception e)
                    {
                        ServerLogManager.LogException(e, "Yupi.Game.Rooms.Room.SendMessageToUsersWithRights");
                    }
                }
            }
            catch (Exception e)
            {
                ServerLogManager.LogException(e, "Yupi.Game.Rooms.Room.SendMessageToUsersWithRights");
            }
        }

        /// <summary>
        ///     Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void SendMessage(ServerMessage message)
        {
            if (message != null)
                SendMessage(message.GetReversedBytes());
        }

        /// <summary>
        ///     Sends the message.
        /// </summary>
        /// <param name="messages">The messages.</param>
        internal void SendMessage(List<ServerMessage> messages)
        {
            if (messages.Count == 0)
                return;

            try
            {
                byte[] totalBytes = new byte[0];
                int currentWorking = 0;

                foreach (ServerMessage message in messages)
                {
                    byte[] toAppend = message.GetReversedBytes();

                    int newLength = totalBytes.Length + toAppend.Length;

                    Array.Resize(ref totalBytes, newLength);

                    for (int i = 0; i < toAppend.Length; i++)
                    {
                        totalBytes[currentWorking] = toAppend[i];
                        currentWorking++;
                    }
                }

                SendMessage(totalBytes);
            }
            catch (Exception e)
            {
                ServerLogManager.LogException(e, "Yupi.Game.Rooms.Room.SendMessage List<ServerMessage>");
            }
        }

        /// <summary>
        ///     Sends the message to users with rights.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void SendMessageToUsersWithRights(ServerMessage message)
        {
            byte[] messagebytes = message.GetReversedBytes();

            try
            {
                foreach (RoomUser unit in _roomUserManager.UserList.Values)
                {
                    RoomUser user = unit;
                    if (user == null)
                        continue;

                    if (user.IsBot)
                        continue;

                    GameClient usersClient = user.GetClient();
                    if (usersClient == null || usersClient.GetConnection() == null)
                        continue;

                    if (!CheckRights(usersClient))
                        continue;

                    usersClient.GetConnection().SendData(messagebytes);
                }
            }
            catch (Exception e)
            {
                ServerLogManager.LogException(e, "Yupi.Game.Rooms.Room.SendMessageToUsersWithRights");
            }
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        internal void Destroy()
        {
            SendMessage(new ServerMessage(LibraryParser.OutgoingRequest("OutOfRoomMessageComposer")));
            Dispose();
        }

        /// <summary>
        ///     Users the is banned.
        /// </summary>
        /// <param name="pId">The p identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool UserIsBanned(uint pId)
        {
            return Bans.ContainsKey(pId);
        }

        /// <summary>
        ///     Removes the ban.
        /// </summary>
        /// <param name="pId">The p identifier.</param>
        internal void RemoveBan(uint pId)
        {
            Bans.Remove(pId);
        }

        /// <summary>
        ///     Adds the ban.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="time">The time.</param>
        internal void AddBan(int userId, long time)
        {
            if (!Bans.ContainsKey(Convert.ToInt32(userId)))
                Bans.Add(userId, Yupi.GetUnixTimeStamp() + time);

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery("REPLACE INTO rooms_bans VALUES (" + userId + ", " + RoomId + ", '" +
                                                    (Yupi.GetUnixTimeStamp() + time) + "')");
        }

        /// <summary>
        ///     Banneds the users.
        /// </summary>
        /// <returns>List&lt;System.UInt32&gt;.</returns>
        internal List<uint> BannedUsers()
        {
            List<uint> list = new List<uint>();
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"SELECT user_id FROM rooms_bans WHERE expire > UNIX_TIMESTAMP() AND room_id={RoomId}");
                DataTable table = commitableQueryReactor.GetTable();
                list.AddRange(from DataRow dataRow in table.Rows select (uint) dataRow[0]);
            }
            return list;
        }

        /// <summary>
        ///     Determines whether [has ban expired] [the specified p identifier].
        /// </summary>
        /// <param name="pId">The p identifier.</param>
        /// <returns><c>true</c> if [has ban expired] [the specified p identifier]; otherwise, <c>false</c>.</returns>
        internal bool HasBanExpired(uint pId)
        {
            return !UserIsBanned(pId) || Bans[pId] < Yupi.GetUnixTimeStamp();
        }

        /// <summary>
        ///     Unbans the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        internal void Unban(uint userId)
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery("DELETE FROM rooms_bans WHERE user_id=" + userId + " AND room_id=" +
                                                    RoomId +
                                                    " LIMIT 1");
            Bans.Remove(userId);
        }

        /// <summary>
        ///     Determines whether [has active trade] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if [has active trade] [the specified user]; otherwise, <c>false</c>.</returns>
        internal bool HasActiveTrade(RoomUser user)
        {
            return !user.IsBot && HasActiveTrade(user.GetClient().GetHabbo().Id);
        }

        /// <summary>
        ///     Determines whether [has active trade] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns><c>true</c> if [has active trade] [the specified user identifier]; otherwise, <c>false</c>.</returns>
        internal bool HasActiveTrade(uint userId)
        {
            object[] array = ActiveTrades.ToArray();
            return array.Cast<Trade>().Any(trade => trade.ContainsUser(userId));
        }

        /// <summary>
        ///     Gets the user trade.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Trade.</returns>
        internal Trade GetUserTrade(uint userId)
        {
            object[] array = ActiveTrades.ToArray();
            return array.Cast<Trade>().FirstOrDefault(trade => trade.ContainsUser(userId));
        }

        /// <summary>
        ///     Tries the start trade.
        /// </summary>
        /// <param name="userOne">The user one.</param>
        /// <param name="userTwo">The user two.</param>
        internal void TryStartTrade(RoomUser userOne, RoomUser userTwo)
        {
            if (userOne == null || userTwo == null || userOne.IsBot || userTwo.IsBot || userOne.IsTrading ||
                userTwo.IsTrading || HasActiveTrade(userOne) || HasActiveTrade(userTwo))
                return;
            ActiveTrades.Add(new Trade(userOne.GetClient().GetHabbo().Id, userTwo.GetClient().GetHabbo().Id, RoomId));
        }

        /// <summary>
        ///     Tries the stop trade.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        internal void TryStopTrade(uint userId)
        {
            Trade userTrade = GetUserTrade(userId);
            if (userTrade == null)
                return;
            userTrade.CloseTrade(userId);
            ActiveTrades.Remove(userTrade);
        }

        /// <summary>
        ///     Sets the maximum users.
        /// </summary>
        /// <param name="maxUsers">The maximum users.</param>
        internal void SetMaxUsers(uint maxUsers)
        {
            RoomData.UsersMax = maxUsers;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery("UPDATE rooms_data SET users_max = " + maxUsers + " WHERE id = " +
                                                    RoomId);
        }

        /// <summary>
        ///     Check if u can go to bed
        /// </summary>
        /// <param name="user">The maximum users.</param>
        /// <param name="goalX">Click To X.</param>
        /// <param name="goalY">Click To Y.</param>
        internal bool MovedToBed(RoomUser user, ref int goalX, ref int goalY)
        {
            if (ContainsBeds > 0)
            {
                List<RoomItem> furni = GetGameMap().GetCoordinatedItems(new Point(goalX, goalY));
                foreach (RoomItem furno in furni)
                {
                    if (furno.GetBaseItem().InteractionType == Interaction.Bed ||
                        furno.GetBaseItem().InteractionType == Interaction.PressurePadBed)
                    {
                        if (furno.Rot == 0 || furno.Rot == 4)
                        {
                            if (user.X == goalX)
                                return false;
                        }
                        else
                        {
                            if (user.Y == goalY)
                                return false;
                        }
                        foreach (Point casella in furno.GetCoords)
                        {
                            if (casella.X == goalX && casella.Y == goalY)
                            {
                                if (furno.Rot == 0 || furno.Rot == 4)
                                {
                                    if (GetGameMap().CanWalk(casella.X, furno.Y, false))
                                    {
                                        goalX = casella.X;
                                        goalY = furno.Y;
                                        return true;
                                    }
                                    return false;
                                }
                                if (GetGameMap().CanWalk(furno.X, casella.Y, false))
                                {
                                    goalX = furno.X;
                                    goalY = casella.Y;
                                    return true;
                                }
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        ///     Flushes the settings.
        /// </summary>
        internal void FlushSettings()
        {
            _mCycleEnded = true;
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                GetRoomItemHandler().SaveFurniture(commitableQueryReactor);
            RoomData.Tags.Clear();
            UsersWithRights.Clear();
            Bans.Clear();
            ActiveTrades.Clear();
            LoadedGroups.Clear();
            if (GotFreeze())
                _freeze = new Freeze(this);
            if (GotBanzai())
                _banzai = new BattleBanzai(this);
            if (GotSoccer())
                _soccer = new Soccer(this);
            if (_gameItemHandler != null)
                _gameItemHandler = new GameItemHandler(this);
        }

        /// <summary>
        ///     Reloads the settings.
        /// </summary>
        internal void ReloadSettings()
        {
            RoomData data = Yupi.GetGame().GetRoomManager().GenerateRoomData(RoomId);
            InitializeFromRoomData(data, false);
        }

        /// <summary>
        ///     Updates the furniture.
        /// </summary>
        internal void UpdateFurniture()
        {
            List<ServerMessage> list = new List<ServerMessage>();
            RoomItem[] array = GetRoomItemHandler().FloorItems.Values.ToArray();
            RoomItem[] array2 = array;
            foreach (RoomItem roomItem in array2)
            {
                ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("UpdateRoomItemMessageComposer"));
                roomItem.Serialize(serverMessage);
                list.Add(serverMessage);
            }
            Array.Clear(array, 0, array.Length);
            RoomItem[] array3 = GetRoomItemHandler().WallItems.Values.ToArray();
            RoomItem[] array4 = array3;
            foreach (RoomItem roomItem2 in array4)
            {
                ServerMessage serverMessage2 =
                    new ServerMessage(LibraryParser.OutgoingRequest("UpdateRoomWallItemMessageComposer"));
                roomItem2.Serialize(serverMessage2);
                list.Add(serverMessage2);
            }
            Array.Clear(array3, 0, array3.Length);
            SendMessage(list);
        }

        /// <summary>
        ///     Checks the mute.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool CheckMute(GameClient session)
        {
            if (RoomMuted || session.GetHabbo().Muted)
                return true;
            if (!MutedUsers.ContainsKey(session.GetHabbo().Id))
                return false;
            if (MutedUsers[session.GetHabbo().Id] >= Yupi.GetUnixTimeStamp())
                return true;
            MutedUsers.Remove(session.GetHabbo().Id);
            return false;
        }

        /// <summary>
        ///     Adds the chatlog.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="message">The message.</param>
        /// <param name="globalMessage"></param>
        internal void AddChatlog(uint id, string message, bool globalMessage)
        {
            lock (RoomData.RoomChat)
                RoomData.RoomChat.Push(new Chatlog(id, message, DateTime.Now, globalMessage));
        }

        internal void SaveRoomChatlog()
        {
            lock (RoomData.RoomChat)
            {
                using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
                    adapter.RunFastQuery($"DELETE FROM users_chatlogs WHERE room_id = {RoomData.Id}");

                    foreach (Chatlog chatlog in RoomData.RoomChat)
                        chatlog.Save(Yupi.GetDatabaseManager().GetQueryReactor(), RoomData.Id);
            }
        }

        /// <summary>
        ///     Resets the game map.
        /// </summary>
        /// <param name="newModelName">New name of the model.</param>
        /// <param name="wallHeight">Height of the wall.</param>
        /// <param name="wallThick">The wall thick.</param>
        /// <param name="floorThick">The floor thick.</param>
        internal void ResetGameMap(string newModelName, int wallHeight, int wallThick, int floorThick)
        {
            RoomData.ModelName = newModelName;
            RoomData.ModelName = newModelName;
            RoomData.ResetModel();
            RoomData.WallHeight = wallHeight;
            RoomData.WallThickness = wallThick;
            RoomData.FloorThickness = floorThick;
            _gameMap = new Gamemap(this);
        }

        /// <summary>
        ///     Initializes from room data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="forceLoad"></param>
        private void InitializeFromRoomData(RoomData data, bool forceLoad)
        {
            Initialize(data.Id, data, data.AllowRightsOverride, data.WordFilter, forceLoad);
        }

        /// <summary>
        ///     Initializes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roomData">The room data.</param>
        /// <param name="rightOverride">if set to <c>true</c> [right override].</param>
        /// <param name="wordFilter">The word filter.</param>
        /// <param name="forceLoad"></param>
        private void Initialize(uint id, RoomData roomData, bool rightOverride, List<string> wordFilter, bool forceLoad)
        {
            RoomData = roomData;

            Disposed = false;
            RoomId = id;
            Bans = new Dictionary<long, double>();
            MutedUsers = new Dictionary<uint, uint>();
            ActiveTrades = new ArrayList();
            MutedBots = false;
            MutedPets = false;
            _mCycleEnded = false;
            EveryoneGotRights = rightOverride;
            LoadedGroups = new Dictionary<uint, string>();
            _roomKick = new Queue();
            _idleTime = 0;
            RoomMuted = false;
            _gameMap = new Gamemap(this);
            _roomItemHandler = new RoomItemHandler(this);
            _roomUserManager = new RoomUserManager(this);
            WordFilter = wordFilter;

            LoadRights();
            LoadMusic();
            LoadBans();
            InitUserBots();

            if (!forceLoad)
            {
                _roomThread = new Thread(StartRoomProcessing) {Name = "Room Loader"};
                _roomThread.Start();
            }

            Yupi.GetGame().GetRoomManager().QueueActiveRoomAdd(RoomData);
        }

        /// <summary>
        ///     Works the room kick queue.
        /// </summary>
        private void WorkRoomKickQueue()
        {
            if (_roomKick.Count <= 0)
                return;

            lock (_roomKick.SyncRoot)
            {
                while (_roomKick.Count > 0)
                {
                    RoomKick roomKick = (RoomKick) _roomKick.Dequeue();
                    List<RoomUser> list = new List<RoomUser>();

                    foreach (
                        RoomUser current in
                            _roomUserManager.UserList.Values.Where(
                                current =>
                                    !current.IsBot && current.GetClient().GetHabbo().Rank < (ulong) roomKick.MinRank))
                    {
                        if (roomKick.Alert.Length > 0)
                            current.GetClient()
                                .SendNotif(string.Format(Yupi.GetLanguage().GetVar("kick_mod_room_message"),
                                    roomKick.Alert));
                        list.Add(current);
                    }

                    foreach (RoomUser current2 in list)
                    {
                        GetRoomUserManager().RemoveUserFromRoom(current2.GetClient(), true, false);
                        current2.GetClient().CurrentRoomUserId = -1;
                    }
                }
            }
        }

        /// <summary>
        ///     Called when [room crash].
        /// </summary>
        /// <param name="e">The e.</param>
        private void OnRoomCrash(Exception e)
        {
            ServerLogManager.LogThreadException(e.ToString(), $"Room cycle task for room {RoomId}");
            Yupi.GetGame().GetRoomManager().UnloadRoom(this, "Room crashed");
            _isCrashed = true;
        }

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        private void Dispose()
        {
            _mCycleEnded = true;

            Yupi.GetGame().GetRoomManager().QueueActiveRoomRemove(RoomData);

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                GetRoomItemHandler().SaveFurniture(commitableQueryReactor);
                commitableQueryReactor.RunFastQuery($"UPDATE rooms_data SET users_now=0 WHERE id = {RoomId} LIMIT 1");
            }

            _processTimer?.Dispose();
            _processTimer = null;
            RoomData.Tags.Clear();
            _roomUserManager.UserList.Clear();
            UsersWithRights.Clear();
            Bans.Clear();
            LoadedGroups.Clear();

            RoomData.RoomChat.Clear();

            GetWiredHandler().Destroy();

            foreach (RoomItem current in GetRoomItemHandler().FloorItems.Values)
                current.Destroy();

            foreach (RoomItem current2 in GetRoomItemHandler().WallItems.Values)
                current2.Destroy();

            ActiveTrades.Clear();

            RoomData = null;
            Yupi.GetGame().GetRoomManager().RemoveRoomData(RoomId);
        }
    }
}
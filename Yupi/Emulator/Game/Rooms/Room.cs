using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Catalogs;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Datas;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Wired;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.RoomBots;
using Yupi.Emulator.Game.RoomBots.Enumerators;
using Yupi.Emulator.Game.Rooms.Chat;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Rooms.Items.Games;
using Yupi.Emulator.Game.Rooms.Items.Games.Handlers;
using Yupi.Emulator.Game.Rooms.Items.Games.Teams;
using Yupi.Emulator.Game.Rooms.Items.Games.Types.Banzai;
using Yupi.Emulator.Game.Rooms.Items.Games.Types.Freeze;
using Yupi.Emulator.Game.Rooms.Items.Games.Types.Soccer;
using Yupi.Emulator.Game.Rooms.Items.Handlers;
using Yupi.Emulator.Game.Rooms.RoomInvokedItems;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Rooms.User.Path;
using Yupi.Emulator.Game.Rooms.User.Trade;
using Yupi.Emulator.Game.SoundMachine;
using Yupi.Emulator.Game.SoundMachine.Songs;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Rooms
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
     public ArrayList ActiveTrades;

        /// <summary>
        ///     The bans
        /// </summary>
     public Dictionary<long, double> Bans;

        /// <summary>
        ///     The _containsBeds count of bed on room
        /// </summary>
     public int ContainsBeds;

        /// <summary>
        ///     The _m disposed
        /// </summary>
        public bool Disposed;

        /// <summary>
        ///     The everyone got rights
        /// </summary>
     public bool EveryoneGotRights, RoomMuted;

        /// <summary>
        ///     The just loaded
        /// </summary>
        public bool JustLoaded = true;

     public DateTime LastTimerReset;

        /// <summary>
        ///     The loaded groups
        /// </summary>
     public Dictionary<uint, string> LoadedGroups;

        /// <summary>
        ///     The moodlight data
        /// </summary>
     public MoodlightData MoodlightData;

        /// <summary>
        ///     The muted bots
        /// </summary>
     public bool MutedBots, DiscoMode, MutedPets;

        /// <summary>
        ///     The muted users
        /// </summary>
     public Dictionary<uint, uint> MutedUsers;

        /// <summary>
        ///     The team banzai
        /// </summary>
     public TeamManager TeamBanzai;

        /// <summary>
        ///     The team freeze
        /// </summary>
     public TeamManager TeamFreeze;

        /// <summary>
        ///     The toner data
        /// </summary>
     public TonerData TonerData;

        /// <summary>
        ///     The users with rights
        /// </summary>
     public List<uint> UsersWithRights;

        /// <summary>
        ///     The word filter
        /// </summary>
     public List<string> WordFilter;

        /// <summary>
        ///     Gets the user count.
        /// </summary>
        /// <value>The user count.</value>
     public int UserCount => _roomUserManager?.GetRoomUserCount() ?? 0;

        /// <summary>
        ///     Gets the tag count.
        /// </summary>
        /// <value>The tag count.</value>
     public int TagCount => RoomData.Tags.Count;

        /// <summary>
        ///     Gets the room identifier.
        /// </summary>
        /// <value>The room identifier.</value>
     public uint RoomId { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance can trade in room.
        /// </summary>
        /// <value><c>true</c> if this instance can trade in room; otherwise, <c>false</c>.</value>
     public bool CanTradeInRoom => true;

        /// <summary>
        ///     Gets the room data.
        /// </summary>
        /// <value>The room data.</value>
     public RoomData RoomData { get; private set; }

     public void Start(RoomData data, bool forceLoad = false)
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
     public Gamemap GetGameMap()
        {
            return _gameMap;
        }

        /// <summary>
        ///     Gets the room item handler.
        /// </summary>
        /// <returns>RoomItemHandler.</returns>
     public RoomItemHandler GetRoomItemHandler()
        {
            return _roomItemHandler;
        }

        /// <summary>
        ///     Gets the room user manager.
        /// </summary>
        /// <returns>RoomUserManager.</returns>
     public RoomUserManager GetRoomUserManager()
        {
            return _roomUserManager;
        }

        /// <summary>
        ///     Gets the soccer.
        /// </summary>
        /// <returns>Soccer.</returns>
     public Soccer GetSoccer()
        {
            return _soccer ?? (_soccer = new Soccer(this));
        }

        /// <summary>
        ///     Gets the team manager for banzai.
        /// </summary>
        /// <returns>TeamManager.</returns>
     public TeamManager GetTeamManagerForBanzai()
        {
            return TeamBanzai ?? (TeamBanzai = TeamManager.CreateTeamforGame("banzai"));
        }

        /// <summary>
        ///     Gets the team manager for freeze.
        /// </summary>
        /// <returns>TeamManager.</returns>
     public TeamManager GetTeamManagerForFreeze()
        {
            return TeamFreeze ?? (TeamFreeze = TeamManager.CreateTeamforGame("freeze"));
        }

        /// <summary>
        ///     Gets the banzai.
        /// </summary>
        /// <returns>BattleBanzai.</returns>
     public BattleBanzai GetBanzai()
        {
            return _banzai ?? (_banzai = new BattleBanzai(this));
        }

        /// <summary>
        ///     Gets the freeze.
        /// </summary>
        /// <returns>Freeze.</returns>
     public Freeze GetFreeze()
        {
            return _freeze ?? (_freeze = new Freeze(this));
        }

        /// <summary>
        ///     Gets the game manager.
        /// </summary>
        /// <returns>GameManager.</returns>
     public GameManager GetGameManager()
        {
            return _game ?? (_game = new GameManager(this));
        }

        /// <summary>
        ///     Gets the game item handler.
        /// </summary>
        /// <returns>GameItemHandler.</returns>
     public GameItemHandler GetGameItemHandler()
        {
            return _gameItemHandler ?? (_gameItemHandler = new GameItemHandler(this));
        }

        /// <summary>
        ///     Gets the room music controller.
        /// </summary>
        /// <returns>RoomMusicController.</returns>
     public SoundMachineManager GetRoomMusicController()
        {
            return _musicController ?? (_musicController = new SoundMachineManager());
        }

        /// <summary>
        ///     Gots the music controller.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
     public bool GotMusicController()
        {
            return _musicController != null;
        }

        /// <summary>
        ///     Gots the soccer.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
     public bool GotSoccer()
        {
            return _soccer != null;
        }

        /// <summary>
        ///     Gots the banzai.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
     public bool GotBanzai()
        {
            return _banzai != null;
        }

        /// <summary>
        ///     Gots the freeze.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
     public bool GotFreeze()
        {
            return _freeze != null;
        }

        /// <summary>
        ///     Starts the room processing.
        /// </summary>
     public void StartRoomProcessing()
        {
            _processTimer = new Timer(ProcessRoom, null, 500, 500);
        }

        /// <summary>
        ///     Initializes the user bots.
        /// </summary>
     public void InitUserBots()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    $"SELECT * FROM bots_data WHERE room_id = {RoomId} AND ai_type = 'generic'");

                DataTable table = queryReactor.GetTable();

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
     public void ClearTags()
        {
            RoomData.Tags.Clear();
        }

        /// <summary>
        ///     Adds the tag range.
        /// </summary>
        /// <param name="tags">The tags.</param>
     public void AddTagRange(List<string> tags)
        {
            RoomData.Tags.AddRange(tags);
        }

        /// <summary>
        ///     Initializes the bots.
        /// </summary>
     public void InitBots()
        {
            List<RoomBot> botsForRoom = Yupi.GetGame().GetBotManager().GetBotsForRoom(RoomId);
            foreach (RoomBot current in botsForRoom.Where(current => !current.IsPet))
                DeployBot(current);
        }

        /// <summary>
        ///     Initializes the pets.
        /// </summary>
     public void InitPets()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery($"SELECT * FROM pets_data WHERE room_id = {RoomId}");

                DataTable table = queryReactor.GetTable();

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
     public RoomUser DeployBot(RoomBot bot)
        {
            return _roomUserManager.DeployBot(bot, null);
        }

        /// <summary>
        ///     Queues the room kick.
        /// </summary>
        /// <param name="kick">The kick.</param>
     public void QueueRoomKick(RoomKick kick)
        {
            lock (_roomKick.SyncRoot)
            {
                _roomKick.Enqueue(kick);
            }
        }

        /// <summary>
        ///     Called when [room kick].
        /// </summary>
     public void OnRoomKick()
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
     public void OnUserEnter(RoomUser user)
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
        /// <param name="message">The messageBuffer.</param>
        /// <param name="shout">if set to <c>true</c> [shout].</param>
     public void OnUserSay(RoomUser user, string message, bool shout)
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
     public void LoadMusic()
        {
            DataTable table;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    $"SELECT items_songs.songid,items_rooms.id,items_rooms.item_name FROM items_songs LEFT JOIN items_rooms ON items_rooms.id = items_songs.itemid WHERE items_songs.roomid = {RoomId}");

                table = queryReactor.GetTable();
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
     public void LoadRights()
        {
            UsersWithRights = new List<uint>();
            DataTable dataTable;
            if (RoomData.Group != null)
                return;
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    $"SELECT rooms_rights.user_id FROM rooms_rights WHERE room_id = {RoomId}");
                dataTable = queryReactor.GetTable();
            }
            if (dataTable == null)
                return;
            foreach (DataRow dataRow in dataTable.Rows)
                UsersWithRights.Add(Convert.ToUInt32(dataRow["user_id"]));
        }

        /// <summary>
        ///     Loads the bans.
        /// </summary>
     public void LoadBans()
        {
            Bans = new Dictionary<long, double>();
            DataTable table;
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery($"SELECT user_id, expire FROM rooms_bans WHERE room_id = {RoomId}");
                table = queryReactor.GetTable();
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
     public bool CheckRights(GameClient session)
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
     public bool CheckRights(GameClient session, bool requireOwnerShip = false, bool checkForGroups = false,
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
                YupiLogManager.LogException(e, "Registered Room Exception.", "Yupi.Room");
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
     public bool CheckRightsDoorBell(GameClient session, bool requireOwnerShip = false, bool checkForGroups = false,
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
                YupiLogManager.LogException(e, "Registered Room Exception.", "Yupi.Room");
            }
            return false;
        }

        /// <summary>
        ///     Processes the room.
        /// </summary>
     public void ProcessRoom(object callItem)
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

                        SimpleServerMessageBuffer simpleServerMessageBuffer = GetRoomUserManager().SerializeStatusUpdates(false);

                        if (simpleServerMessageBuffer != null)
                            SendMessage(simpleServerMessageBuffer);
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
                    YupiLogManager.LogException(e, "Registered Room Crashing.", "Yupi.Room");

                    OnRoomCrash(e);
                }
            }
            catch (Exception e)
            {
                YupiLogManager.LogException(e, "Registered Room Crashing.", "Yupi.Room");
            }
        }

		public void Send(ServerMessage message) {
			SendMessage(message.GetReversedBytes());
		}

        /// <summary>
        ///     Sends the messageBuffer.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
     public void SendMessage(byte[] message)
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

                    usersClient.GetConnection().Send(message);
                }
            }
            catch (Exception e)
            {
                YupiLogManager.LogException(e, "Failed to Send Outgoing Message to Client.", "Yupi.User");
            }
        }

        /// <summary>
        ///     Broadcasts the chat messageBuffer.
        /// </summary>
        /// <param name="chatMsg">The chat MSG.</param>
        /// <param name="roomUser">The room user.</param>
        /// <param name="p">The p.</param>
     public void BroadcastChatMessage(SimpleServerMessageBuffer chatMsg, RoomUser roomUser, uint p)
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
                        YupiLogManager.LogException(e, "Failed Broadcasting Message to Client.", "Yupi.User");
                    }
                }
            }
            catch (Exception e)
            {
                YupiLogManager.LogException(e, "Failed Broadcasting Message to Client.", "Yupi.User");
            }
        }

        /// <summary>
        ///     Sends the messageBuffer.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
     public void SendMessage(SimpleServerMessageBuffer messageBuffer)
        {
            if (messageBuffer != null)
                SendMessage(messageBuffer.GetReversedBytes());
        }

        /// <summary>
        ///     Sends the messageBuffer.
        /// </summary>
        /// <param name="messages">The messages.</param>
     public void SendMessage(List<SimpleServerMessageBuffer> messages)
        {
            if (messages.Count == 0)
                return;

            try
            {
                byte[] totalBytes = new byte[0];
                int currentWorking = 0;

                foreach (SimpleServerMessageBuffer message in messages)
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
                YupiLogManager.LogException(e, "Failed to Broadcasting Message to Client.", "Yupi.User");
            }
        }

        /// <summary>
        ///     Sends the messageBuffer to users with rights.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
     public void SendMessageToUsersWithRights(SimpleServerMessageBuffer messageBuffer)
        {
            byte[] messagebytes = messageBuffer.GetReversedBytes();

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

                    if (usersClient?.GetConnection() == null)
                        continue;

                    if (!CheckRights(usersClient))
                        continue;

                    usersClient.GetConnection().Send(messagebytes);
                }
            }
            catch (Exception e)
            {
                YupiLogManager.LogException(e, "Failed to Broadcasting Message to Client.", "Yupi.User");
            }
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
     public void Destroy()
        {
			// TODO Merge Destroy & Dispose ???
			using(SimpleServerMessageBuffer message = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("OutOfRoomMessageComposer"))) {
				SendMessage(message);
			}
            Dispose();
        }

        /// <summary>
        ///     Users the is banned.
        /// </summary>
        /// <param name="pId">The p identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
     public bool UserIsBanned(uint pId)
        {
            return Bans.ContainsKey(pId);
        }

        /// <summary>
        ///     Removes the ban.
        /// </summary>
        /// <param name="pId">The p identifier.</param>
     public void RemoveBan(uint pId)
        {
            Bans.Remove(pId);
        }

        /// <summary>
        ///     Adds the ban.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="time">The time.</param>
     public void AddBan(int userId, long time)
        {
            if (!Bans.ContainsKey(Convert.ToInt32(userId)))
                Bans.Add(userId, Yupi.GetUnixTimeStamp() + time);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery("REPLACE INTO rooms_bans VALUES (" + userId + ", " + RoomId + ", '" +
                                          (Yupi.GetUnixTimeStamp() + time) + "')");
        }

        /// <summary>
        ///     Banneds the users.
        /// </summary>
        /// <returns>List&lt;System.UInt32&gt;.</returns>
     public List<uint> BannedUsers()
        {
            List<uint> list = new List<uint>();

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
				// TODO NEVER interpret a query!!!
                queryReactor.SetQuery($"SELECT user_id FROM rooms_bans WHERE expire > UNIX_TIMESTAMP() AND room_id={RoomId}");

                DataTable table = queryReactor.GetTable();

                if (table == null)
                    return null;

                list.AddRange(from DataRow dataRow in table.Rows select (uint) dataRow[0]);

                return list;
            }
        }

        /// <summary>
        ///     Determines whether [has ban expired] [the specified p identifier].
        /// </summary>
        /// <param name="pId">The p identifier.</param>
        /// <returns><c>true</c> if [has ban expired] [the specified p identifier]; otherwise, <c>false</c>.</returns>
     public bool HasBanExpired(uint pId)
        {
            return !UserIsBanned(pId) || Bans[pId] < Yupi.GetUnixTimeStamp();
        }

        /// <summary>
        ///     Unbans the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
     public void Unban(uint userId)
        {
			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) {
                queryReactor.RunFastQuery("DELETE FROM rooms_bans WHERE user_id=" + userId + " AND room_id=" +
                                          RoomId +
                                          " LIMIT 1");
				// TODO Secure query
			}
            Bans.Remove(userId);
        }

        /// <summary>
        ///     Determines whether [has active trade] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if [has active trade] [the specified user]; otherwise, <c>false</c>.</returns>
     public bool HasActiveTrade(RoomUser user)
        {
            return !user.IsBot && HasActiveTrade(user.GetClient().GetHabbo().Id);
        }

        /// <summary>
        ///     Determines whether [has active trade] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns><c>true</c> if [has active trade] [the specified user identifier]; otherwise, <c>false</c>.</returns>
     public bool HasActiveTrade(uint userId)
        {
            object[] array = ActiveTrades.ToArray();
            return array.Cast<Trade>().Any(trade => trade.ContainsUser(userId));
        }

        /// <summary>
        ///     Gets the user trade.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Trade.</returns>
     public Trade GetUserTrade(uint userId)
        {
            object[] array = ActiveTrades.ToArray();
            return array.Cast<Trade>().FirstOrDefault(trade => trade.ContainsUser(userId));
        }

        /// <summary>
        ///     Tries the start trade.
        /// </summary>
        /// <param name="userOne">The user one.</param>
        /// <param name="userTwo">The user two.</param>
     public void TryStartTrade(RoomUser userOne, RoomUser userTwo)
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
     public void TryStopTrade(uint userId)
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
     public void SetMaxUsers(uint maxUsers)
        {
            RoomData.UsersMax = maxUsers;
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery("UPDATE rooms_data SET users_max = " + maxUsers + " WHERE id = " +
                                          RoomId);
        }

        /// <summary>
        ///     Check if u can go to bed
        /// </summary>
        /// <param name="user">The maximum users.</param>
        /// <param name="goalX">Click To X.</param>
        /// <param name="goalY">Click To Y.</param>
     public bool MovedToBed(RoomUser user, ref int goalX, ref int goalY)
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
     public void FlushSettings()
        {
            _mCycleEnded = true;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                GetRoomItemHandler().SaveFurniture(queryReactor);

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
     public void ReloadSettings()
        {
            RoomData data = Yupi.GetGame().GetRoomManager().GenerateRoomData(RoomId);
            InitializeFromRoomData(data, false);
        }

        /// <summary>
        ///     Updates the furniture.
        /// </summary>
     public void UpdateFurniture()
        {
            List<SimpleServerMessageBuffer> list = new List<SimpleServerMessageBuffer>();
            RoomItem[] array = GetRoomItemHandler().FloorItems.Values.ToArray();
            RoomItem[] array2 = array;
            foreach (RoomItem roomItem in array2)
            {
                SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateRoomItemMessageComposer"));
                roomItem.Serialize(simpleServerMessageBuffer);
                list.Add(simpleServerMessageBuffer);
            }
            Array.Clear(array, 0, array.Length);
            RoomItem[] array3 = GetRoomItemHandler().WallItems.Values.ToArray();
            RoomItem[] array4 = array3;
            foreach (RoomItem roomItem2 in array4)
            {
                SimpleServerMessageBuffer simpleServerMessage2 =
                    new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateRoomWallItemMessageComposer"));
                roomItem2.Serialize(simpleServerMessage2);
                list.Add(simpleServerMessage2);
            }
            Array.Clear(array3, 0, array3.Length);
            SendMessage(list);
        }

        /// <summary>
        ///     Checks the mute.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
     public bool CheckMute(GameClient session)
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
        /// <param name="message">The messageBuffer.</param>
        /// <param name="globalMessage"></param>
     public void AddChatlog(uint id, string message, bool globalMessage)
        {
            lock (RoomData.RoomChat)
                RoomData.RoomChat.Push(new Chatlog(id, message, DateTime.Now, globalMessage));
        }

     public void SaveRoomChatlog()
        {
            lock (RoomData.RoomChat)
            {
                using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
                    adapter.RunFastQuery($"DELETE FROM users_chatlogs WHERE room_id = {RoomData.Id}");

                foreach (Chatlog chatlog in RoomData.RoomChat)
                    chatlog.Save(RoomData.Id);
            }
        }

        /// <summary>
        ///     Resets the game map.
        /// </summary>
        /// <param name="newModelName">New name of the model.</param>
        /// <param name="wallHeight">Height of the wall.</param>
        /// <param name="wallThick">The wall thick.</param>
        /// <param name="floorThick">The floor thick.</param>
     public void ResetGameMap(string newModelName, int wallHeight, int wallThick, int floorThick)
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
            YupiLogManager.LogMessage("Confirmning Room Crash.", "Yupi.Room");

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

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                GetRoomItemHandler().SaveFurniture(queryReactor);

                queryReactor.RunFastQuery($"UPDATE rooms_data SET users_now = 0 WHERE id = {RoomId} LIMIT 1");
            }

            _processTimer?.Dispose();
            _processTimer = null;

            RoomData.Tags.Clear();

            GetRoomUserManager().UserList.Clear();

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
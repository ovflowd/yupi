using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Timers;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Core.Settings;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Achievements.Structs;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Users.Badges;
using Yupi.Emulator.Game.Users.Data.Models;
using Yupi.Emulator.Game.Users.Inventory;
using Yupi.Emulator.Game.Users.Inventory.Components;
using Yupi.Emulator.Game.Users.Messenger;
using Yupi.Emulator.Game.Users.Messenger.Structs;
using Yupi.Emulator.Game.Users.Relationships;
using Yupi.Emulator.Game.Users.Subscriptions;
using Yupi.Emulator.Messages;


namespace Yupi.Emulator.Game.Users
{
    /// <summary>
    ///     Class Habbo.
    /// </summary>
    public class Habbo
    {
        /// <summary>
        ///     The _my groups
        /// </summary>
        private readonly List<uint> _myGroups;

        /// <summary>
        ///     The _avatar effects inventory component
        /// </summary>
        private AvatarEffectComponent _avatarEffectComponent;

        /// <summary>
        ///     The _badge component
        /// </summary>
        private UserBadgeManager _badgeComponent;

        /// <summary>
        ///     The _habboinfo saved
        /// </summary>
        private bool _habboinfoSaved;

        /// <summary>
        ///     The _inventory component
        /// </summary>
        private InventoryComponent _inventoryComponent;

        /// <summary>
        ///     The _loaded my groups
        /// </summary>
        private bool _loadedMyGroups;

        /// <summary>
        ///     The _m client
        /// </summary>
        private GameClient _mClient;

        /// <summary>
        ///     The _messenger
        /// </summary>
        private HabboMessenger _messenger;

        /// <summary>
        ///     The _subscription manager
        /// </summary>
        private SubscriptionManager _subscriptionManager;

        /// <summary>
        ///     The achievements
        /// </summary>
        public Dictionary<string, UserAchievement> Achievements;

        /// <summary>
        ///     The answered polls
        /// </summary>
		public HashSet<uint> AnsweredPolls;

        // NEW
		public bool AnsweredPool = false;

        /// <summary>
        ///     The appear offline
        /// </summary>
		public bool AppearOffline;

        /// <summary>
        ///     Bobba Filter
        /// </summary>
		public int BobbaFiltered = 0;

        /// <summary>
        ///     The builders expire
        /// </summary>
		public int BuildersExpire;

        /// <summary>
        ///     The builders items maximum
        /// </summary>
		public int BuildersItemsMax;

        /// <summary>
        ///     The builders items used
        /// </summary>
		public int BuildersItemsUsed;

        /// <summary>
        ///     The _clothing manager
        /// </summary>
		public UserClothesManager ClothesManagerManager;

        /// <summary>
        ///     The create date
        /// </summary>
		public double CreateDate;

        /// <summary>
        ///     The credits
        /// </summary>
		public uint Credits, AchievementPoints;

        /// <summary>
        ///     The current quest identifier
        /// </summary>
		public int CurrentQuestId;

        /// <summary>
        ///     The current room identifier
        /// </summary>
     public uint CurrentRoomId;

        /// <summary>
        ///     The current talent level
        /// </summary>
		public int CurrentTalentLevel;

		public bool DisableEventAlert;

        /// <summary>
        ///     The disconnected
        /// </summary>
		public bool IsOnline;

        /// <summary>
        ///     The credits
        /// </summary>
		public uint Duckets;

		public int DutyLevel;

        /// <summary>
        ///     The favorite rooms
        /// </summary>
		public List<uint> FavoriteRooms;

        /// <summary>
        ///     The favourite group
        /// </summary>
		public uint FavouriteGroup;

        /// <summary>
        ///     The flood time
        /// </summary>
		public int FloodTime;

        /// <summary>
        ///     The friend count
        /// </summary>
		public uint FriendCount;

        /// <summary>
        ///     The guide other user
        /// </summary>
        public GameClient GuideOtherUser;

        /// <summary>
        ///     The has friend requests disabled
        /// </summary>
		public bool HasFriendRequestsDisabled;

        /// <summary>
        ///     The hide in room
        /// </summary>
		public bool HideInRoom;

        /// <summary>
        ///     The home room
        /// </summary>
		public uint HomeRoom;

        /// <summary>
        ///     The hopper identifier
        /// </summary>
		public uint HopperId;

        /// <summary>
        ///     The identifier
        /// </summary>
		public uint Id;

        /// <summary>
        ///     The is hopping
        /// </summary>
		public bool IsHopping;

        /// <summary>
        ///     The is teleporting
        /// </summary>
		public bool IsTeleporting;

        /// <summary>
        ///     The last change
        /// </summary>
		public int LastChange;

        /// <summary>
        ///     The last gift open time
        /// </summary>
		public DateTime LastGiftOpenTime;

        /// <summary>
        ///     The last gift purchase time
        /// </summary>
		public DateTime LastGiftPurchaseTime;

        /// <summary>
        ///     The last online
        /// </summary>
		public int LastOnline;

        /// <summary>
        ///     The last quest completed
        /// </summary>
		public int LastQuestCompleted = 0;

        public uint LastSelectedUser = 0;

		// TODO Should not be required when using proper caching!
        /// <summary>
        ///     The last SQL query
        /// </summary>
		public int LastSqlQuery;

        public DateTime LastUsed = DateTime.Now;

        /// <summary>
        ///     The loading checks passed
        /// </summary>
		public bool LoadingChecksPassed;

        /// <summary>
        ///     The loading room
        /// </summary>
		public uint LoadingRoom;

        /// <summary>
        ///     The minimail unread messages
        /// </summary>
		public uint MinimailUnreadMessages;

        /// <summary>
        ///     The muted
        /// </summary>
		public bool Muted;

        /// <summary>
        ///     The muted users
        /// </summary>
		public List<uint> MutedUsers;

        /// <summary>
        ///     The navigator logs
        /// </summary>
		public Dictionary<int, UserSearchLog> NavigatorLogs;

        /// <summary>
        ///     The nux passed
        /// </summary>
		public bool NuxPassed;

        /// <summary>
        ///     The on duty
        /// </summary>
		public bool OnDuty;

        /// <summary>
        ///     The own rooms serialized
        /// </summary>
		public bool OwnRoomsSerialized = false;

		public UserPreferences Preferences;

        /// <summary>
        ///     The previous online
        /// </summary>
		public int PreviousOnline;

        /// <summary>
        ///     The quests
        /// </summary>
		public Dictionary<int, int> Quests;

        /// <summary>
        ///     The rank
        /// </summary>
		public uint Rank;

        /// <summary>
        ///     The rated rooms
        /// </summary>
		public HashSet<uint> RatedRooms;

        /// <summary>
        ///     The recently visited rooms
        /// </summary>
		public LinkedList<uint> RecentlyVisitedRooms;

        /// <summary>
        ///     The relationships
        /// </summary>
		public Dictionary<int, Relationship> Relationships;

        /// <summary>
        ///     The release name
        /// </summary>
		public string ReleaseName;

        /// <summary>
        ///     The respect
        /// </summary>
		public int Respect, DailyRespectPoints, DailyPetRespectPoints, DailyCompetitionVotes;

        /// <summary>
        ///     The spam flood time
        /// </summary>
		public DateTime SpamFloodTime;

        /// <summary>
        ///     The spam protection bol
        /// </summary>
		public bool SpamProtectionBol;

        /// <summary>
        ///     The spam protection count
        /// </summary>
		public int SpamProtectionCount = 1, SpamProtectionTime, SpamProtectionAbuse;

        /// <summary>
        ///     The spectator mode
        /// </summary>
		public bool SpectatorMode;

        /// <summary>
        ///     The tags
        /// </summary>
		public List<string> Tags;

        /// <summary>
        ///     The talents
        /// </summary>
		public Dictionary<int, UserTalent> Talents;

        /// <summary>
        ///     The talent status
        /// </summary>
		public string TalentStatus;

        /// <summary>
        ///     The teleporter identifier
        /// </summary>
		public uint TeleporterId;

        /// <summary>
        ///     The teleporting room identifier
        /// </summary>
		public uint TeleportingRoomId;

        /// <summary>
        ///     TimeLoggedOn
        /// </summary>
		public DateTime TimeLoggedOn;

        /// <summary>
        ///     The timer_ elapsed
        /// </summary>
        public bool TimerElapsed;

        /// <summary>
        ///     The trade locked
        /// </summary>
		public bool TradeLocked;

        /// <summary>
        ///     The trade lock expire
        /// </summary>
		public int TradeLockExpire;

        /// <summary>
        ///     The user groups
        /// </summary>
		public HashSet<GroupMember> UserGroups;

        /// <summary>
        ///     The user name
        /// </summary>
		public string UserName, RealName, Motto, Look, Gender;

        /// <summary>
        ///     The users rooms
        /// </summary>
		public HashSet<RoomData> UsersRooms;

        /// <summary>
        ///     The vip
        /// </summary>
		public bool Vip;

		public YoutubeManager YoutubeManager;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Habbo" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="realName">Name of the real.</param>
        /// <param name="rank">The rank.</param>
        /// <param name="motto">The motto.</param>
        /// <param name="look">The look.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="credits">The credits.</param>
        /// <param name="duckets">The activity points.</param>
        /// <param name="muted">if set to <c>true</c> [muted].</param>
        /// <param name="homeRoom">The home room.</param>
        /// <param name="respect">The respect.</param>
        /// <param name="dailyRespectPoints">The daily respect points.</param>
        /// <param name="dailyPetRespectPoints">The daily pet respect points.</param>
        /// <param name="hasFriendRequestsDisabled">if set to <c>true</c> [has friend requests disabled].</param>
        /// <param name="currentQuestId">The current quest identifier.</param>
        /// <param name="achievementPoints">The achievement points.</param>
        /// <param name="lastOnline">The last online.</param>
        /// <param name="appearOffline">if set to <c>true</c> [appear offline].</param>
        /// <param name="hideInRoom">if set to <c>true</c> [hide in room].</param>
        /// <param name="vip">if set to <c>true</c> [vip].</param>
        /// <param name="createDate">The create date.</param>
        /// <param name="citizenShip">The citizen ship.</param>
        /// <param name="diamonds">The diamonds.</param>
        /// <param name="groups">The groups.</param>
        /// <param name="favId">The fav identifier.</param>
        /// <param name="lastChange">The last change.</param>
        /// <param name="tradeLocked">if set to <c>true</c> [trade locked].</param>
        /// <param name="tradeLockExpire">The trade lock expire.</param>
        /// <param name="nuxPassed">if set to <c>true</c> [nux passed].</param>
        /// <param name="buildersExpire">The builders expire.</param>
        /// <param name="buildersItemsMax">The builders items maximum.</param>
        /// <param name="buildersItemsUsed">The builders items used.</param>
        /// <param name="onDuty">if set to <c>true</c> [on duty].</param>
        /// <param name="naviLogs">The navi logs.</param>
        /// <param name="dailyCompetitionVotes"></param>
        /// <param name="dutyLevel"></param>
		public Habbo(uint id, string userName, string realName, uint rank, string motto, string look, string gender,
            uint credits, uint duckets, bool muted, uint homeRoom, int respect,
            int dailyRespectPoints, int dailyPetRespectPoints, bool hasFriendRequestsDisabled, int currentQuestId, uint achievementPoints, int lastOnline, bool appearOffline,
            bool hideInRoom, bool vip, double createDate, string citizenShip, uint diamonds,
            HashSet<GroupMember> groups, int favId, int lastChange, bool tradeLocked, int tradeLockExpire,
            bool nuxPassed,
            int buildersExpire, int buildersItemsMax, int buildersItemsUsed, bool onDuty,
            Dictionary<int, UserSearchLog> naviLogs, int dailyCompetitionVotes, int dutyLevel)
        {
            Id = id;
            UserName = userName;
            RealName = realName;

            _myGroups = new List<uint>();

            BuildersExpire = buildersExpire;
            BuildersItemsMax = buildersItemsMax;
            BuildersItemsUsed = buildersItemsUsed;

            if (rank < 1u)
                rank = 1u;

            ReleaseName = string.Empty;

            OnDuty = onDuty;
            DutyLevel = dutyLevel;

            Rank = rank;
            Motto = motto;
            Look = look.ToLower();
            Vip = rank > 5 || vip;
            LastChange = lastChange;
            TradeLocked = tradeLocked;
            NavigatorLogs = naviLogs;
            TradeLockExpire = tradeLockExpire;
            Gender = gender.ToLower() == "f" ? "f" : "m";
            Credits = credits;
            Duckets = duckets;
            Diamonds = diamonds;
            AchievementPoints = achievementPoints;
            Muted = muted;
            LoadingRoom = 0u;
            CreateDate = createDate;
            LoadingChecksPassed = false;
            FloodTime = 0;
            NuxPassed = nuxPassed;
            CurrentRoomId = 0u;
            TimeLoggedOn = DateTime.Now;
            HomeRoom = homeRoom;
            HideInRoom = hideInRoom;
            AppearOffline = appearOffline;
            FavoriteRooms = new List<uint>();
            MutedUsers = new List<uint>();
            Tags = new List<string>();
            Achievements = new Dictionary<string, UserAchievement>();
            Talents = new Dictionary<int, UserTalent>();
            Relationships = new Dictionary<int, Relationship>();
            RatedRooms = new HashSet<uint>();
            Respect = respect;
            DailyRespectPoints = dailyRespectPoints;
            DailyPetRespectPoints = dailyPetRespectPoints;
            IsTeleporting = false;
            TeleporterId = 0u;
            UsersRooms = new HashSet<RoomData>();
            HasFriendRequestsDisabled = hasFriendRequestsDisabled;
            LastOnline = Yupi.GetUnixTimeStamp();
            PreviousOnline = lastOnline;
            RecentlyVisitedRooms = new LinkedList<uint>();
            CurrentQuestId = currentQuestId;
            IsHopping = false;

            FavouriteGroup = Yupi.GetGame().GetGroupManager().GetGroup((uint) favId) != null ? (uint) favId : 0;

            UserGroups = groups;

            if (DailyPetRespectPoints > 99)
                DailyPetRespectPoints = 99;

            if (DailyRespectPoints > 99)
                DailyRespectPoints = 99;

            LastGiftPurchaseTime = DateTime.Now;
            LastGiftOpenTime = DateTime.Now;
            TalentStatus = citizenShip;
            CurrentTalentLevel = GetCurrentTalentLevel();
            DailyCompetitionVotes = dailyCompetitionVotes;
            DisableEventAlert = false;
        }

		public uint Diamonds
        {
            get
            {
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery($"SELECT diamonds FROM users WHERE id = {Id}");

                    return (uint) queryReactor.GetInteger();
                }
            }
            set
            {
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery(string.Format("UPDATE users SET diamonds = {1} WHERE id = {0}",
                        Id, value));
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance can change name.
        /// </summary>
        /// <value><c>true</c> if this instance can change name; otherwise, <c>false</c>.</value>
        public bool CanChangeName => (ServerExtraSettings.ChangeNameStaff && HasFuse("fuse_can_change_name")) ||
                                     (ServerExtraSettings.ChangeNameVip && Vip) ||
                                     (ServerExtraSettings.ChangeNameEveryone &&
                                      Yupi.GetUnixTimeStamp() > LastChange + 604800);

        /// <summary>
        ///     Gets the head part.
        /// </summary>
        /// <value>The head part.</value>
		public string HeadPart
        {
            get
            {
                string[] strtmp = Look.Split('.');
                string tmp2 = strtmp.FirstOrDefault(x => x.Contains("hd-"));
                string lookToReturn = tmp2 ?? string.Empty;

                if (Look.Contains("ha-"))
                    lookToReturn += $".{strtmp.FirstOrDefault(x => x.Contains("ha-"))}";
                if (Look.Contains("ea-"))
                    lookToReturn += $".{strtmp.FirstOrDefault(x => x.Contains("ea-"))}";
                if (Look.Contains("hr-"))
                    lookToReturn += $".{strtmp.FirstOrDefault(x => x.Contains("hr-"))}";
                if (Look.Contains("he-"))
                    lookToReturn += $".{strtmp.FirstOrDefault(x => x.Contains("he-"))}";
                if (Look.Contains("fa-"))
                    lookToReturn += $".{strtmp.FirstOrDefault(x => x.Contains("fa-"))}";

                return lookToReturn;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether [in room].
        /// </summary>
        /// <value><c>true</c> if [in room]; otherwise, <c>false</c>.</value>
		public bool InRoom => CurrentRoomId >= 1 && CurrentRoom != null;

        /// <summary>
        ///     Gets the current room.
        /// </summary>
        /// <value>The current room.</value>
		public Room CurrentRoom
            => CurrentRoomId <= 0u ? null : Yupi.GetGame().GetRoomManager().GetRoom(CurrentRoomId);

        /// <summary>
        ///     Gets a value indicating whether this instance is helper.
        /// </summary>
        /// <value><c>true</c> if this instance is helper; otherwise, <c>false</c>.</value>
		public bool IsHelper => TalentStatus == "helper" || Rank >= 4;

        /// <summary>
        ///     Gets a value indicating whether this instance is citizen.
        /// </summary>
        /// <value><c>true</c> if this instance is citizen; otherwise, <c>false</c>.</value>
		public bool IsCitizen => CurrentTalentLevel > 4;

        /// <summary>
        ///     Gets my groups.
        /// </summary>
        /// <value>My groups.</value>
		public List<uint> MyGroups
        {
            get
            {
                if (!_loadedMyGroups)
                    _LoadMyGroups();

                return _myGroups;
            }
        }

        /// <summary>
        ///     Handles the ElapsedEvent event of the timer control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs" /> instance containing the event data.</param>
        public void timer_ElapsedEvent(object source, ElapsedEventArgs e)
        {
            TimerElapsed = true;
        }

        /// <summary>
        ///     Initializes the information.
        /// </summary>
        /// <param name="data">The data.</param>
		public void InitInformation(UserData data)
        {
            _subscriptionManager = new SubscriptionManager(Id, data);
            _badgeComponent = new UserBadgeManager(Id, data);
            Quests = data.Quests;
            _messenger = new HabboMessenger(Id);
            _messenger.Init(data.Friends, data.Requests);
            SpectatorMode = false;
            IsOnline = true;
            UsersRooms = data.Rooms;
            Relationships = data.Relations;
            AnsweredPolls = data.SuggestedPolls;
        }

        /// <summary>
        ///     Initializes the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="data">The data.</param>
		public void Init(GameClient client, UserData data)
        {
            _mClient = client;
            _subscriptionManager = new SubscriptionManager(Id, data);
            _badgeComponent = new UserBadgeManager(Id, data);
            _inventoryComponent = new InventoryComponent(Id, client, data);
            _inventoryComponent.SetActiveState(client);
            _avatarEffectComponent = new AvatarEffectComponent(Id, client, data);
            Quests = data.Quests;
            _messenger = new HabboMessenger(Id);
            _messenger.Init(data.Friends, data.Requests);
            FriendCount = Convert.ToUInt32(data.Friends.Count);
            SpectatorMode = false;
            IsOnline = true;
            UsersRooms = data.Rooms;
            MinimailUnreadMessages = data.MiniMailCount;
            Relationships = data.Relations;
            AnsweredPolls = data.SuggestedPolls;
            ClothesManagerManager = new UserClothesManager(Id);
            Preferences = new UserPreferences(Id);
            YoutubeManager = new YoutubeManager(Id);
        }

        /// <summary>
        ///     Updates the rooms.
        /// </summary>
		public void UpdateRooms()
        {
            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                UsersRooms.Clear();
                dbClient.SetQuery("SELECT * FROM rooms_data WHERE owner = @userId ORDER BY id ASC LIMIT 50");
                dbClient.AddParameter("userId", Id);

                DataTable table = dbClient.GetTable();

                foreach (DataRow dataRow in table.Rows)
                    UsersRooms.Add(Yupi.GetGame()
                        .GetRoomManager()
                        .FetchRoomData(Convert.ToUInt32(dataRow["id"]), dataRow));
            }
        }

        /// <summary>
        ///     Loads the data.
        /// </summary>
        /// <param name="data">The data.</param>
		public void LoadData(UserData data)
        {
            LoadAchievements(data.Achievements);
            LoadTalents(data.Talents);
            LoadFavorites(data.FavouritedRooms);
            LoadMutedUsers(data.Ignores);
            LoadTags(data.Tags);
        }

        /// <summary>
        ///     Gots the command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool GotCommand(string cmd)
        {
            return Yupi.GetGame().GetRoleManager().RankGotCommand(Rank, cmd);
        }

        /// <summary>
        ///     Determines whether the specified fuse has fuse.
        /// </summary>
        /// <param name="fuse">The fuse.</param>
        /// <returns><c>true</c> if the specified fuse has fuse; otherwise, <c>false</c>.</returns>
		public bool HasFuse(string fuse)
        {
            return Yupi.GetGame().GetRoleManager().RankHasRight(Rank, fuse) ||
                   (GetSubscriptionManager().HasSubscription &&
                    Yupi.GetGame()
                        .GetRoleManager()
                        .HasVip(GetSubscriptionManager().GetSubscription().SubscriptionId, fuse));
        }

        /// <summary>
        ///     Loads the favorites.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
		public void LoadFavorites(List<uint> roomId)
        {
            FavoriteRooms = roomId;
        }

        /// <summary>
        ///     Loads the muted users.
        /// </summary>
        /// <param name="usersMuted">The users muted.</param>
		public void LoadMutedUsers(List<uint> usersMuted)
        {
            MutedUsers = usersMuted;
        }

        /// <summary>
        ///     Loads the tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
		public void LoadTags(List<string> tags)
        {
            Tags = tags;
        }

        /// <summary>
        ///     Serializes the club.
        /// </summary>
		public void SerializeClub()
        {
            GameClient client = GetClient();
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer();
            simpleServerMessageBuffer.Init(PacketLibraryManager.OutgoingHandler("SubscriptionStatusMessageComposer"));
            simpleServerMessageBuffer.AppendString("club_habbo");
            if (client.GetHabbo().GetSubscriptionManager().HasSubscription)
            {
                double num = client.GetHabbo().GetSubscriptionManager().GetSubscription().ExpireTime;
                double num2 = num - Yupi.GetUnixTimeStamp();
                int num3 = (int) Math.Ceiling(num2/86400.0);
                int i =
                    (int)
                        Math.Ceiling((Yupi.GetUnixTimeStamp() -
                                      (double) client.GetHabbo().GetSubscriptionManager().GetSubscription().ActivateTime)/
                                     86400.0);
                int num4 = num3/31;

                if (num4 >= 1)
                    num4--;

                simpleServerMessageBuffer.AppendInteger(num3 - num4*31);
                simpleServerMessageBuffer.AppendInteger(1);
                simpleServerMessageBuffer.AppendInteger(num4);
                simpleServerMessageBuffer.AppendInteger(1);
                simpleServerMessageBuffer.AppendBool(true);
                simpleServerMessageBuffer.AppendBool(true);
                simpleServerMessageBuffer.AppendInteger(i);
                simpleServerMessageBuffer.AppendInteger(i);
                simpleServerMessageBuffer.AppendInteger(10);
            }
            else
            {
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendBool(false);
                simpleServerMessageBuffer.AppendBool(false);
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendInteger(0);
            }

            client.SendMessage(simpleServerMessageBuffer);

            SimpleServerMessageBuffer simpleServerMessage2 = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UserClubRightsMessageComposer"));

            simpleServerMessage2.AppendInteger(GetSubscriptionManager().HasSubscription ? 2 : 0);
            simpleServerMessage2.AppendInteger(Rank);
            simpleServerMessage2.AppendBool(Rank >= Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"]));

            client.SendMessage(simpleServerMessage2);
        }

        /// <summary>
        ///     Loads the achievements.
        /// </summary>
        /// <param name="achievements">The achievements.</param>
		public void LoadAchievements(Dictionary<string, UserAchievement> achievements)
        {
            Achievements = achievements;
        }

        /// <summary>
        ///     Loads the talents.
        /// </summary>
        /// <param name="talents">The talents.</param>
		public void LoadTalents(Dictionary<int, UserTalent> talents)
        {
            Talents = talents;
        }

        /// <summary>
        ///     Called when [disconnect].
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="showConsole"></param>
		public void OnDisconnect(string reason, bool showConsole = false)
        {
            if (!IsOnline)
                return;

            IsOnline = false;

            if (_inventoryComponent != null)
            {
                lock (_inventoryComponent)
                {
                    _inventoryComponent?.RunDbUpdate();
                    _inventoryComponent?.SetIdleState();
                }
            }

            string navilogs = string.Empty;

            if (NavigatorLogs.Any())
            {
                navilogs = NavigatorLogs.Values.Aggregate(navilogs, (current, navi) => current + $"{navi.Id},{navi.Value1},{navi.Value2};");

                navilogs = navilogs.Remove(navilogs.Length - 1);
            }

            Yupi.GetGame().GetClientManager().UnregisterClient(Id, UserName);

            if(showConsole)
                YupiWriterManager.WriteLine(UserName + " left game. Reason: " + reason, "Yupi.User", ConsoleColor.DarkYellow);

            TimeSpan getOnlineSeconds = DateTime.Now - TimeLoggedOn;

            int secondsToGive = getOnlineSeconds.Seconds;

            if (!_habboinfoSaved)
            {
                _habboinfoSaved = true;

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery(string.Format("UPDATE users SET activity_points = {0}, credits = {1}, diamonds = {2}, online = '0', last_online = '{3}', builders_items_used = {4}, navigator_logs = @navilogs  WHERE id = {5} LIMIT 1;" +
                                                        "UPDATE users_stats SET achievement_score = {6} WHERE id = {5} LIMIT 1;", 
                                                        Duckets, Credits, Diamonds, Yupi.GetUnixTimeStamp(), BuildersItemsUsed, Id, AchievementPoints));

                    queryReactor.AddParameter("navilogs", navilogs);
                    queryReactor.RunQuery();

                    queryReactor.RunFastQuery("UPDATE users_stats SET online_seconds = online_seconds + " + secondsToGive + " WHERE id = " + Id);

                    if (Rank >= 4)
                        queryReactor.RunFastQuery($"UPDATE moderation_tickets SET status = 'open', moderator_id = 0 WHERE status = 'picked' AND moderator_id = {Id}");

                    queryReactor.RunFastQuery($"UPDATE users SET block_newfriends = '{Convert.ToInt32(HasFriendRequestsDisabled)}', hide_online = '{Convert.ToInt32(AppearOffline)}', hide_inroom = '{Convert.ToInt32(HideInRoom)}' WHERE id = {Id}");
                }
            }

            if (InRoom)
                CurrentRoom?.GetRoomUserManager().RemoveUserFromRoom(_mClient, false, false);

            if (_messenger != null)
            {
                _messenger.AppearOffline = true;
                _messenger.Destroy();
                _messenger = null;
            }

            _avatarEffectComponent?.Dispose();

            _mClient = null;
        }

        /// <summary>
        ///     Initializes the messenger.
        /// </summary>
		public void InitMessenger()
        {
            GameClient client = GetClient();

            if (client == null)
                return;

			client.Router.GetComposer<LoadFriendsCategories>().Compose(client);

            client.SendMessage(_messenger.SerializeCategories());

			client.Router.GetComposer<LoadFriendsMessageComposer>().Compose(client, _messenger.Friends, client);
			client.Router.GetComposer<FriendRequestsMessageComposer>().Compose(client, _messenger.Requests);

            if (Yupi.OfflineMessages.ContainsKey(Id))
            {
                List<OfflineMessage> list = Yupi.OfflineMessages[Id];
                foreach (OfflineMessage current in list)
				{
					client.Router.GetComposer<ConsoleChatMessageComposer>().Compose(client, current);
				}
                Yupi.OfflineMessages.Remove(Id);
                OfflineMessage.RemoveAllMessages(Yupi.GetDatabaseManager().GetQueryReactor(), Id);
            }

            if (_messenger.Requests.Count > Yupi.FriendRequestLimit)
                client.SendNotif(Yupi.GetLanguage().GetVar("user_friend_request_max"));

            _messenger.OnStatusChanged(false);
        }

        /// <summary>
        ///     Updates the credits balance.
        /// </summary>
		public void UpdateCreditsBalance()
        {
            if (_mClient?.GetMessageHandler() == null || _mClient.GetMessageHandler().GetResponse() == null)
                return;

            _mClient.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.OutgoingHandler("CreditsBalanceMessageComposer"));
            _mClient.GetMessageHandler().GetResponse().AppendString($"{Credits}.0");
            _mClient.GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Updates the activity points balance.
        /// </summary>
		public void UpdateActivityPointsBalance()
        {
            if (_mClient?.GetMessageHandler() == null || _mClient.GetMessageHandler().GetResponse() == null)
                return;

            _mClient.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.OutgoingHandler("ActivityPointsMessageComposer"));
            _mClient.GetMessageHandler().GetResponse().AppendInteger(3);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(0);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(Duckets);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(5);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(Diamonds);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(105);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(Diamonds);
            _mClient.GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Updates the seasonal currency balance.
        /// </summary>
		public void UpdateSeasonalCurrencyBalance()
        {
            if (_mClient?.GetMessageHandler() == null || _mClient.GetMessageHandler().GetResponse() == null)
                return;

            _mClient.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.OutgoingHandler("ActivityPointsMessageComposer"));
            _mClient.GetMessageHandler().GetResponse().AppendInteger(3);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(0);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(Duckets);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(5);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(Diamonds);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(105);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(Diamonds);
            _mClient.GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Notifies the new pixels.
        /// </summary>
        /// <param name="change">The change.</param>
		public void NotifyNewPixels(uint change)
        {
            if (_mClient?.GetMessageHandler() == null || _mClient.GetMessageHandler().GetResponse() == null)
                return;

            _mClient.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.OutgoingHandler("ActivityPointsNotificationMessageComposer"));
            _mClient.GetMessageHandler().GetResponse().AppendInteger(Duckets);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(change);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(0);
            _mClient.GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Notifies the new diamonds.
        /// </summary>
        /// <param name="change">The change.</param>
		public void NotifyNewDiamonds(int change)
        {
            if (_mClient?.GetMessageHandler() == null || _mClient.GetMessageHandler().GetResponse() == null)
                return;

            _mClient.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.OutgoingHandler("ActivityPointsNotificationMessageComposer"));
            _mClient.GetMessageHandler().GetResponse().AppendInteger(Diamonds);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(change);
            _mClient.GetMessageHandler().GetResponse().AppendInteger(5);
            _mClient.GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Notifies the voucher.
        /// </summary>
        /// <param name="isValid">if set to <c>true</c> [is valid].</param>
        /// <param name="productName">Name of the product.</param>
        /// <param name="productDescription">The product description.</param>
		public void NotifyVoucher(bool isValid, string productName, string productDescription)
        {
            if (isValid)
            {
                _mClient.GetMessageHandler()
                    .GetResponse()
                    .Init(PacketLibraryManager.OutgoingHandler("VoucherValidMessageComposer"));
                _mClient.GetMessageHandler().GetResponse().AppendString(productName);
                _mClient.GetMessageHandler().GetResponse().AppendString(productDescription);
                _mClient.GetMessageHandler().SendResponse();
                return;
            }

            _mClient.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.OutgoingHandler("VoucherErrorMessageComposer"));
            _mClient.GetMessageHandler().GetResponse().AppendString("1");
            _mClient.GetMessageHandler().SendResponse();
        }

        /// <summary>
        ///     Mutes this instance.
        /// </summary>
		public void Mute()
        {
            if (!Muted)
                Muted = true;
        }

        /// <summary>
        ///     Uns the mute.
        /// </summary>
		public void UnMute()
        {
            if (Muted)
                GetClient().SendNotif("You were unmuted.");

            Muted = false;

            if (CurrentRoom != null && CurrentRoom.MutedUsers.ContainsKey(Id))
                CurrentRoom.MutedUsers.Remove(Id);
        }

        /// <summary>
        ///     Gets the subscription manager.
        /// </summary>
        /// <returns>SubscriptionManager.</returns>
		public SubscriptionManager GetSubscriptionManager() => _subscriptionManager;

		public YoutubeManager GetYoutubeManager() => YoutubeManager;

        /// <summary>
        ///     Gets the messenger.
        /// </summary>
        /// <returns>HabboMessenger.</returns>
		public HabboMessenger GetMessenger() => _messenger;

        /// <summary>
        ///     Gets the badge component.
        /// </summary>
        /// <returns>BadgeComponent.</returns>
		public UserBadgeManager GetBadgeComponent() => _badgeComponent;

        /// <summary>
        ///     Gets the inventory component.
        /// </summary>
        /// <returns>InventoryComponent.</returns>
		public InventoryComponent GetInventoryComponent() => _inventoryComponent;

        /// <summary>
        ///     Gets the avatar effects inventory component.
        /// </summary>
        /// <returns>AvatarEffectComponent.</returns>
		public AvatarEffectComponent GetAvatarEffectsInventoryComponent() => _avatarEffectComponent;

        /// <summary>
        ///     Runs the database update.
        /// </summary>
		public void RunDbUpdate()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.RunFastQuery(
                    $"UPDATE users_stats SET achievement_score = '{AchievementPoints}' WHERE id = {Id}");
                queryReactor.RunFastQuery(
                    $"UPDATE users SET last_online = '{Yupi.GetUnixTimeStamp()}', activity_points = '{Duckets}', credits = '{Credits}', diamonds = '{Diamonds}' WHERE id = '{Id}'");
            }

            _habboinfoSaved = true;
        }

        /// <summary>
        ///     Gets the quest progress.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>System.Int32.</returns>
		public int GetQuestProgress(int p)
        {
            int result;

            Quests.TryGetValue(p, out result);

            return result;
        }

        /// <summary>
        ///     Gets the achievement data.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>UserAchievement.</returns>
		public UserAchievement GetAchievementData(string p)
        {
            UserAchievement result;

            Achievements.TryGetValue(p, out result);

            return result;
        }

        /// <summary>
        ///     Gets the talent data.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>UserTalent.</returns>
		public UserTalent GetTalentData(int t)
        {
            UserTalent result;

            Talents.TryGetValue(t, out result);

            return result;
        }

        /// <summary>
        ///     Gets the current talent level.
        /// </summary>
        /// <returns>System.Int32.</returns>
		public int GetCurrentTalentLevel()
            =>
                Talents.Values.Select(current => Yupi.GetGame().GetTalentManager().GetTalent(current.TalentId).Level)
                    .Concat(new[] {1})
                    .Max();

        /// <summary>
        ///     _s the load my groups.
        /// </summary>
		public void _LoadMyGroups()
        {
            using (IQueryAdapter commitableQueryAdapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryAdapter.SetQuery($"SELECT id FROM groups_data WHERE owner_id = {Id}");

                foreach (DataRow dRow in commitableQueryAdapter.GetTable().Rows)
                    _myGroups.Add(Convert.ToUInt32(dRow["id"]));

                _loadedMyGroups = true;
            }
        }

        /// <summary>
        ///     Gots the poll data.
        /// </summary>
        /// <param name="pollId">The poll identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool GotPollData(uint pollId) => AnsweredPolls.Contains(pollId);

        /// <summary>
        ///     Checks the trading.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool CheckTrading()
        {
            if (!TradeLocked)
                return true;

            if (TradeLockExpire - Yupi.GetUnixTimeStamp() > 0)
                return false;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery($"UPDATE users SET trade_lock = '0' WHERE id = {Id}");

            TradeLocked = false;
            return true;
        }

        /// <summary>
        ///     Gets the client.
        /// </summary>
        /// <returns>GameClient.</returns>
        private GameClient GetClient() => Yupi.GetGame().GetClientManager().GetClientByUserId(Id);
    }
}

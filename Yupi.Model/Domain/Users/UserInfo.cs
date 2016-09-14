using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Timers;
using Yupi.Model.Domain.Components;
using Yupi.Util;
using System.Net;

namespace Yupi.Model.Domain
{
    public class UserInfo : BaseInfo
    {
        public static UserInfo None = new UserInfo() {Id = 0, Name = string.Empty};

        public virtual UserBadgeComponent Badges { get; protected set; }
        public virtual UserBuilderComponent BuilderInfo { get; protected set; }
        public virtual UserWallet Wallet { get; protected set; }
        public virtual UserPreferences Preferences { get; protected set; }
        public virtual UserEffectComponent EffectComponent { get; protected set; }

        public virtual string Email { get; set; }
        public virtual bool AppearOffline { get; set; }
        public virtual int BobbaFiltered { get; set; }
        public virtual DateTime CreateDate { get; set; }

        public virtual Subscription Subscription { get; set; }

        public virtual IList<UserSearchLog> NavigatorLog { get; protected set; }

        [ManyToMany]
        public virtual IList<RoomData> FavoriteRooms { get; protected set; }

        public virtual IList<SupportTicket> SupportTickets { get; protected set; }
        public virtual IList<TradeLock> TradeLocks { get; protected set; }

        public virtual IList<Minimail> Minimail { get; protected set; }

        public virtual Group FavouriteGroup { get; set; }
        public virtual RoomData HomeRoom { get; set; }

        public virtual bool HasFriendRequestsDisabled { get; set; }
        public virtual bool HideInRoom { get; set; }

        public virtual DateTime LastOnline { get; set; }

        // TODO Move to log
        public virtual IPAddress LastIp { get; set; }

        public virtual bool Muted { get; set; }

        // TODO Rename
        [ManyToMany]
        public virtual IList<UserInfo> MutedUsers { get; protected set; }

        public virtual IList<UserCaution> Cautions { get; protected set; }
        public virtual IList<UserBan> Bans { get; protected set; }

        public virtual int Rank { get; set; }

        [ManyToMany]
        public virtual IList<RoomData> RatedRooms { get; protected set; }

        [ManyToMany]
        public virtual IList<RoomData> RecentlyVisitedRooms { get; protected set; }

        public virtual RelationshipComponent Relationships { get; protected set; }

        public virtual UserRespectComponent Respect { get; protected set; }

        public virtual DateTime SpamFloodTime { get; set; }

        public virtual bool SpectatorMode { get; set; }

        [OneToMany]
        public virtual IList<string> Tags { get; protected set; }

        [OneToMany]
        public virtual IList<UserTalent> Talents { get; protected set; }

        [ManyToMany]
        public virtual IList<Group> UserGroups { get; protected set; }

        public virtual IList<UserAchievement> Achievements { get; protected set; }

        public virtual string Look { get; set; }
        public virtual string Gender { get; set; }

        public virtual Inventory Inventory { get; protected set; }

        [OneToMany]
        public virtual IList<RoomData> UsersRooms { get; protected set; }

        public UserInfo()
        {
            Badges = new UserBadgeComponent();
            Wallet = new UserWallet();
            Preferences = new UserPreferences();
            EffectComponent = new UserEffectComponent();
            CreateDate = DateTime.Now;
            Subscription = new Subscription();
            NavigatorLog = new List<UserSearchLog>();
            FavoriteRooms = new List<RoomData>();
            SupportTickets = new List<SupportTicket>();
            TradeLocks = new List<TradeLock>();
            MutedUsers = new List<UserInfo>();
            Cautions = new List<UserCaution>();
            Bans = new List<UserBan>();
            Rank = 1;
            RatedRooms = new List<RoomData>();
            RecentlyVisitedRooms = new List<RoomData>();
            Relationships = new RelationshipComponent();
            Respect = new UserRespectComponent();
            Tags = new List<string>();
            Talents = new List<UserTalent>();
            UserGroups = new List<Group>();
            Achievements = new List<UserAchievement>();
            Inventory = new Inventory();
            UsersRooms = new List<RoomData>();
            Look = "hr-115-42.hd-190-1.ch-215-62.lg-285-91.sh-290-62";
            Gender = "M";
            Motto = string.Empty;
            Email = string.Empty;
            BuilderInfo = new UserBuilderComponent();
            Minimail = new List<Minimail>();
        }

        public virtual bool IsBanned()
        {
            // TODO Implement IP & Machine ID Bans
            return Bans.Any(x => x.ExpiresAt > DateTime.Now);
        }

        public virtual bool CanTrade()
        {
            return !TradeLocks.Any(x => x.ExpiresAt > DateTime.Now);
        }

        public virtual bool CanChangeName()
        {
            /*return (ServerExtraSettings.ChangeNameStaff && HasFuse("fuse_can_change_name")) ||
                                     (ServerExtraSettings.ChangeNameVip && Vip) ||
                                     (ServerExtraSettings.ChangeNameEveryone);*/
            return false; // TODO Reimplement
        }

        // TODO Use enum!
        public virtual bool HasPermission(string permission)
        {
            // FIXME
            return false;
        }

        // TODO Cleanup
        /*
        public virtual bool IsHelper ()
        {
            return TalentStatus == "helper" || Rank >= 4;
        }

        public virtual bool IsCitizen ()
        {
            return CurrentTalentLevel > 4;
        }*/

        /*
        public virtual bool GotCommand(string cmd)
        {
            return Yupi.GetGame().GetRoleManager().RankGotCommand(Rank, cmd);
        }
            
        public virtual bool HasFuse(string fuse)
        {
            return Yupi.GetGame().GetRoleManager().RankHasRight(Rank, fuse) ||
                   (GetSubscriptionManager().HasSubscription &&
                    Yupi.GetGame()
                        .GetRoleManager()
                        .HasVip(GetSubscriptionManager().GetSubscription().SubscriptionId, fuse));
        }

                                public virtual void SerializeClub()
        {
            GameClient client = GetClient();
            if (client.GetHabbo().GetSubscriptionManager().HasSubscription)
            {
                client.Router.GetComposer<SubscriptionStatusMessageComposer>().Compose(client, client.GetHabbo().GetSubscriptionManager().GetSubscription());  
            } else {
                client.Router.GetComposer<SubscriptionStatusMessageComposer>().Compose(client, null);  
            }

            client.Router.GetComposer<UserClubRightsMessageComposer>().Compose(client, GetSubscriptionManager().HasSubscription, Rank,
                Rank >= Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"]));  
        }
            
        public virtual void OnDisconnect(string reason, bool showConsole = false)
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

            if (InRoom)
                CurrentRoom?.GetRoomUserManager().RemoveUserFromRoom(_mClient, false, false);

            _avatarEffectComponent?.Dispose();

            _mClient = null;
        }

      */
    }
}
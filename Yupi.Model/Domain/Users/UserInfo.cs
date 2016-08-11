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
	public class UserInfo
	{
		public static UserInfo None = new UserInfo() { Id = 0, UserName = string.Empty };

		public virtual int Id { get; protected set; }

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

		[ManyToMany]
		public virtual IList<RoomData> FavoriteRooms { get; protected set; }

		public virtual IList<SupportTicket> SupportTickets { get; protected set; }
		public virtual IList<TradeLock> TradeLocks { get; protected set; }

		public virtual Group FavouriteGroup { get; set; }
		public virtual uint HomeRoom { get; set; }

		public virtual bool HasFriendRequestsDisabled { get; set; }
		public virtual bool HideInRoom { get; set; }

		public virtual DateTime LastOnline { get; set; }
		public virtual IPAddress LastIp { get; set; }

		public virtual bool Muted { get; set; }

		// TODO Rename
		[ManyToMany]
		public virtual IList<UserInfo> MutedUsers { get; protected set; }

		public virtual IList<UserCaution> Cautions { get; protected set; }
		public virtual IList<UserBan> Bans { get; protected set; }

		public virtual int Rank { get; set; }

		[ManyToMany]
		public virtual HashSet<RoomData> RatedRooms { get; protected set; }

		[ManyToMany]
		public virtual IList<RoomData> RecentlyVisitedRooms { get; protected set; }

		public virtual RelationshipComponent Relationships { get; protected set; }

		public virtual string ReleaseName { get; set; }

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

		public virtual string UserName { get; set; }
		public virtual string Motto { get; set; }
		public virtual string Look { get; set; }
		public virtual string Gender { get; set; }

		public virtual Inventory Inventory { get; protected set; }

		[OneToMany]
		public virtual IList<RoomData> UsersRooms { get; protected set; }
		public virtual bool IsVip { get; set; }

		public UserInfo() {
			FavouriteGroup = Group.None;
		}

		public virtual bool CanChangeName ()
		{
			/*return (ServerExtraSettings.ChangeNameStaff && HasFuse("fuse_can_change_name")) ||
                                     (ServerExtraSettings.ChangeNameVip && Vip) ||
                                     (ServerExtraSettings.ChangeNameEveryone);*/
			return false; // TODO Reimplement
		}

		// TODO Use enum!
		public virtual bool HasPermission(string permission) {
			throw new NotImplementedException ();
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

                        		public virtual void InitMessenger()
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
        }*/
	}
}

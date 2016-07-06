using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Timers;
using FluentNHibernate.Data;
using Yupi.Model.Achievements;
using Yupi.Model.Groups;

namespace Yupi.Model.Users
{
	public class Habbo : Entity
	{
		public uint Id;

		public bool AppearOffline;
		public int BobbaFiltered;

		public int BuildersExpire;
		public int BuildersItemsMax;
		public int BuildersItemsUsed;

		public double CreateDate;

		public uint Credits;
		public uint AchievementPoints;
		public uint Duckets;

		// TODO Foreign key
		public List<uint> FavoriteRooms;
		public uint FavouriteGroup;
		public uint HomeRoom;

		public bool HasFriendRequestsDisabled;
		public bool HideInRoom;

		public DateTime LastOnline;
    
		public bool Muted;

		// TODO Rename
		public List<uint> MutedUsers;


		public UserPreferences Preferences;

		public uint Rank;

		public HashSet<uint> RatedRooms;

		public IList<uint> RecentlyVisitedRooms;


		public IList<Relationship> Relationships;

		public string ReleaseName;

		public int Respect;

		public int DailyRespectPoints;
		public int DailyPetRespectPoints;
		public int DailyCompetitionVotes;

		public DateTime SpamFloodTime;

		public bool SpectatorMode;

		public IList<string> Tags;

		public IList<UserTalent> Talents;

		public IList<Group> UserGroups;

		public string UserName, Motto, Look, Gender;

		public IList<RoomData> UsersRooms;

		public bool IsVip;

		public uint Diamonds { get; set; }

		public bool CanChangeName ()
		{
			/*return (ServerExtraSettings.ChangeNameStaff && HasFuse("fuse_can_change_name")) ||
                                     (ServerExtraSettings.ChangeNameVip && Vip) ||
                                     (ServerExtraSettings.ChangeNameEveryone);*/
			return false; // TODO Reimplement
		}

		public bool IsHelper ()
		{
			return TalentStatus == "helper" || Rank >= 4;
		}

		public bool IsCitizen ()
		{
			return CurrentTalentLevel > 4;
		}

		/*
		public bool GotCommand(string cmd)
        {
            return Yupi.GetGame().GetRoleManager().RankGotCommand(Rank, cmd);
        }
			
		public bool HasFuse(string fuse)
        {
            return Yupi.GetGame().GetRoleManager().RankHasRight(Rank, fuse) ||
                   (GetSubscriptionManager().HasSubscription &&
                    Yupi.GetGame()
                        .GetRoleManager()
                        .HasVip(GetSubscriptionManager().GetSubscription().SubscriptionId, fuse));
        }

                        		public void SerializeClub()
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

            if (InRoom)
                CurrentRoom?.GetRoomUserManager().RemoveUserFromRoom(_mClient, false, false);

            _avatarEffectComponent?.Dispose();

            _mClient = null;
        }

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
        }*/
	}
}

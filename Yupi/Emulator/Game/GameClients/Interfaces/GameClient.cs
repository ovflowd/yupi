using System;
using System.Linq;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Core.Security.BlackWords.Structs;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Game.Users.Data.Models;
using Yupi.Emulator.Game.Users.Factories;
using Yupi.Net;
using Yupi.Protocol.Buffers;
using Yupi.Protocol;
using Yupi.Emulator.Game.Rooms;
using System.Globalization;

namespace Yupi.Emulator.Game.GameClients.Interfaces
{
    /// <summary>
    ///     Class GameClient.
    /// </summary>
	public class GameClient : ISender
    {
        /// <summary>
        ///     The Client Connection
        /// </summary>
		private ISession<GameClient> _connection;

        /// <summary>
        ///     The _habbo
        /// </summary>
        private Habbo _habbo;

        /// <summary>
        ///     The current room user identifier
        /// </summary>
        public int CurrentRoomUserId;

        /// <summary>
        ///     The designed handler
        /// </summary>
		public int DesignedHandler = 1;

        /// <summary>
        ///     The machine identifier
        /// </summary>
		public string MachineId;

		// HACK Interface can be removed once cyclic dependency is resolved
		public IRouter Router;

        /// <summary>
        ///     The publicist count
        /// </summary>
		public byte PublicistCount;

		// TODO should be thread safe!
        /// <summary>
        ///     The time pinged received
        /// </summary>
		public DateTime TimePingedReceived;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameClient" /> class.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="connection">The connection.</param>
		public GameClient(ISession<GameClient> connection)
        {
            _connection = connection;

            CurrentRoomUserId = -1;

			TimePingedReceived = DateTime.Now;
        }

        /// <summary>
        ///     Handles the publicist.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        /// <param name="settings">The settings.</param>
		public void HandlePublicist(string word, string message, string method, BlackWordTypeSettings settings)
        {
            Habbo userPublicist = GetHabbo();

            if (userPublicist != null)
            {     
                if (userPublicist.Rank < 5 && settings.MaxAdvices == PublicistCount++ && settings.AutoBan)
                {
					Router.GetComposer<SuperNotificationMessageComposer> ().Compose (Yupi.GetGame().GetClientManager().StaffAlert, "Staff  Alert", 
						"O usuário " + userPublicist.UserName + " Fo Banido por Divulgar. A última palavra foi: " + word + ", na frase: " + message,
						"", "", "staffcloud", 2);
					          
                    Yupi.GetGame().GetBanManager().BanUser(this, userPublicist.UserName, 788922000.0, "Você está divulgando Hoteis. Será banido para sempre..", true, true);

                    return;
                }

                string alert = settings.Alert.Replace("{0}", userPublicist.UserName);
				// TODO use format
                alert = alert.Replace("{1}", userPublicist.Id.ToString());
                alert = alert.Replace("{2}", word);
                alert = alert.Replace("{3}", message);
                alert = alert.Replace("{4}", method);

				Router.GetComposer<UsersClassificationMessageComposer> ().Compose (Yupi.GetGame().GetClientManager().StaffAlert, userPublicist, word);
            
                if (Yupi.GetGame().GetClientManager() == null)
                    return;

                foreach (GameClient client in Yupi.GetGame().GetClientManager().Clients.Values)
                {
                    if (client?.GetHabbo() == null)
                        continue;

                    if (client.CurrentRoomUserId == 0)
                        continue;

                    if (client.GetHabbo().Rank < 5)
                        continue;

					Router.GetComposer<WhisperMessageComposer> ().Compose (client, client.CurrentRoomUserId, alert, 34);
                }
            }
        }

        /// <summary>
        ///     Gets the connection.
        /// </summary>
        /// <returns>ConnectionInformation.</returns>
		public ISession<GameClient> GetConnection() { return _connection; }

        /// <summary>
        ///     Gets the habbo.
        /// </summary>
        /// <returns>Habbo.</returns>
		public Habbo GetHabbo() => _habbo;

        /// <summary>
        ///     Tries the login.
        /// </summary>
        /// <param name="authTicket">The authentication ticket.</param>
        /// <param name="banReasonOut"></param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool TryLogin(string authTicket, out string banReasonOut)
        {
            banReasonOut = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(authTicket))
                    return false;

				string ip = GetConnection().RemoteAddress.ToString();

                if (string.IsNullOrEmpty(ip))
                    return false;

                uint errorCode;

                UserData userData = UserDataFactory.GetUserData(authTicket, out errorCode);

                if (userData?.User == null)
                    return false;
				// TODO Magic Number
                if (errorCode == 1 || errorCode == 2)
                    return false;

                Yupi.GetGame().GetClientManager().RegisterClient(this, userData.UserId, userData.User.UserName);

                _habbo = userData.User;

                userData.User.LoadData(userData);
 
                bool isBanned = Yupi.GetGame().GetBanManager().CheckIfIsBanned(userData.User.UserName, ip, MachineId);

                if (isBanned)
                {
                    string banReason = Yupi.GetGame().GetBanManager().GetBanReason(userData.User.UserName, ip, MachineId);

                    banReasonOut = banReason;

                    SendNotifWithScroll(banReason);

                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery($"SELECT ip_last FROM users WHERE id = {GetHabbo().Id} LIMIT 1");

                        string supaString = queryReactor.GetString();

                        queryReactor.SetQuery($"SELECT COUNT(0) FROM users_bans_access WHERE user_id={_habbo.Id} LIMIT 1");
                        int integer = queryReactor.GetInteger();

                        if (integer > 0)
                            queryReactor.RunFastQuery("UPDATE users_bans_access SET attempts = attempts + 1, ip='" + supaString + "' WHERE user_id=" + GetHabbo().Id + " LIMIT 1");
                        else
                            queryReactor.RunFastQuery("INSERT INTO users_bans_access (user_id, ip) VALUES (" + GetHabbo().Id + ", '" + supaString + "')");
                    }

                    return false;
                }

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery($"UPDATE users SET ip_last='{ip}' WHERE id={GetHabbo().Id}");

                userData.User.Init(this, userData);

				Router.GetComposer<UniqueMachineIDMessageComposer> ().Compose (client, MachineId);  
				Router.GetComposer<AuthenticationOKMessageComposer> ().Compose (client);    
				Router.GetComposer<HomeRoomMessageComposer> ().Compose (client, _habbo.HomeRoom);  
				Router.GetComposer<MinimailCountMessageComposer> ().Compose (client, _habbo.MinimailUnreadMessages);  
				Router.GetComposer<FavouriteRoomsMessageComposer> ().Compose (client, userData.User.FavoriteRooms);  
				Router.GetComposer<UserClubRightsMessageComposer> ().Compose (client, userData.User.GetSubscriptionManager().HasSubscription ? 2 : 0,
					userData.User.Rank);     
				Router.GetComposer<EnableNotificationsMessageComposer> ().Compose (client);   
				Router.GetComposer<EnableTradingMessageComposer> ().Compose (client);   
               
                userData.User.UpdateCreditsBalance();

				Router.GetComposer<ActivityPointsMessageComposer> ().Compose (client, userData.User.Duckets, userData.User.Diamonds);            

				if (userData.User.HasFuse("fuse_mod")) {
					Router.GetComposer<LoadModerationToolMessageComposer>().Compose(this, Yupi.GetGame().GetModerationTool(), this.GetHabbo());
				}
    
				Router.GetComposer<SendAchievementsRequirementsMessageComposer>().Compose(this, Yupi.GetGame().GetAchievementManager().Achievements);
				Router.GetComposer<EffectsInventoryMessageComposer>().Compose(this, GetHabbo().GetAvatarEffectsInventoryComponent()._effects);

                Yupi.GetGame().GetAchievementManager().TryProgressHabboClubAchievements(this);
                Yupi.GetGame().GetAchievementManager().TryProgressRegistrationAchievements(this);
                Yupi.GetGame().GetAchievementManager().TryProgressLoginAchievements(this);

                return true;
            }
            catch (Exception ex)
            {
                YupiLogManager.LogException(ex, "Registered Login Exception.", "Yupi.User");
            }

            return false;
        }

        /// <summary>
        ///     Sends the notif with scroll.
        /// </summary>
        /// <param name="message">The message.</param>
		public void SendNotifWithScroll(string message)
        {
			Router.GetComposer<MOTDNotificationMessageComposer>().Compose(this, message);
        }

        /// <summary>
        ///     Sends the broadcast message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void SendBroadcastMessage(string message)
        {
			Router.GetComposer<BroadcastNotifMessageComposer>().Compose(this, message);
        }

        /// <summary>
        ///     Sends the moderator message.
        /// </summary>
        /// <param name="message">The message.</param>
		public void SendModeratorMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

			Router.GetComposer<AlertNotificationMessageComposer>().Compose(this, message);
        }

        /// <summary>
        ///     Sends the whisper.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="fromWired"></param>
		public void SendWhisper(string message, bool fromWired = false)
        {
            if (GetHabbo() == null || GetHabbo().CurrentRoom == null)
                return;

            RoomUser roomUserByHabbo = GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(GetHabbo().UserName);

            if (roomUserByHabbo == null)
                return;

			Router.GetComposer<WhisperMessageComposer>().Compose(this, roomUserByHabbo.VirtualId, message, 
				fromWired ? 34 : roomUserByHabbo.LastBubble, fromWired);
        }

        /// <summary>
        ///     Sends the notif.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        /// <param name="picture">The picture.</param>
		public void SendNotif(string message, string title = "Aviso", string picture = "") {
			Router.GetComposer<SuperNotificationMessageComposer>().Compose(this, title, message, "", "", picture, 4); 
		}
			
        /// <summary>
        ///     Disconnects the specified reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="showConsole"></param>
		public void Disconnect(string reason = "Left Game", bool showConsole = false)
        {

            GetHabbo()?.RunDbUpdate();
            GetHabbo()?.OnDisconnect(reason, showConsole);  
            GetConnection()?.Disconnect();

            CurrentRoomUserId = -1;

            _habbo = null;

            _connection = null;
        }
		// TODO Remove
		public void SendMessage(ServerMessage message) {
			Send(message);
		}

		public void Send(ServerMessage message) {
			byte[] bytes = message.GetReversedBytes();

			GetConnection().Send(bytes);
		}

		public void ClearRoomLoading()
		{
			if (GetHabbo() == null)
				return;

			GetHabbo().LoadingRoom = 0u;
			GetHabbo().LoadingChecksPassed = false;
		}



		public void PrepareRoomForUser(uint id, string pWd, bool isReload = false)
		{
			try
			{
				if (GetHabbo().LoadingRoom == id)
					return;

				if (Yupi.ShutdownStarted)
				{
					SendNotif(Yupi.GetLanguage().GetVar("server_shutdown"));
					return;
				}

				GetHabbo().LoadingRoom = id;

				Room room;

				if (GetHabbo().InRoom)
				{
					room = Yupi.GetGame().GetRoomManager().GetRoom(GetHabbo().CurrentRoomId);

					if (room?.GetRoomUserManager() != null)
						room.GetRoomUserManager().RemoveUserFromRoom(this, false, false);
				}

				room = Yupi.GetGame().GetRoomManager().LoadRoom(id);

				if (room == null)
					return;

				if (room.UserCount >= room.RoomData.UsersMax && !GetHabbo().HasFuse("fuse_enter_full_rooms") &&
					GetHabbo().Id != (ulong) room.RoomData.OwnerId)
				{
					Router.GetComposer<RoomsQueue>().Compose(this, 
						room.UserCount - (int) room.RoomData.UsersNow);
					return;
				}

				CurrentLoadingRoom = room;

				if (!GetHabbo().HasFuse("fuse_enter_any_room") && room.UserIsBanned(GetHabbo().Id))
				{
					if (!room.HasBanExpired(GetHabbo().Id))
					{
						ClearRoomLoading();

						Router.GetComposer<RoomEnterErrorMessageComposer> ().Compose (this, RoomEnterErrorMessageComposer.Error.UNKNOWN);
						Router.GetComposer<OutOfRoomMessageComposer> ().Compose (this);
					} else {
						room.RemoveBan(GetHabbo().Id);
					}
				}

				Router.GetComposer<PrepareRoomMessageComposer> ().Compose (this); 

				if (!isReload && !GetHabbo().HasFuse("fuse_enter_any_room") && !room.CheckRightsDoorBell(this, true, true, room.RoomData.Group != null 
					&& room.RoomData.Group.Members.ContainsKey(GetHabbo().Id)) 
					&& !(GetHabbo().IsTeleporting && GetHabbo().TeleportingRoomId == id) 
					&& !GetHabbo().IsHopping)
				{
					if (room.RoomData.State == 1)
					{
						if (room.UserCount == 0)
						{
							Router.GetComposer<DoorbellNoOneMessageComposer> ().Compose (this);    
						}
						else
						{
							Router.GetComposer<DoorbellMessageComposer> ().Compose (this, string.Empty);  
							room.Router.GetComposer<DoorbellMessageComposer> ().Compose (room.RightsSender, GetHabbo().UserName); 
						}

						return;
					}

					if (room.RoomData.State == 2 && !string.Equals(pWd, room.RoomData.PassWord, StringComparison.CurrentCultureIgnoreCase))
					{
						ClearRoomLoading();
						Router.GetComposer<RoomErrorMessageComposer> ().Compose (this, -100002);  
						Router.GetComposer<OutOfRoomMessageComposer> ().Compose (this);  
						return;
					}
				}

				GetHabbo().LoadingChecksPassed = true;
				GetHabbo().RecentlyVisitedRooms.AddFirst(room.RoomId);
			}
			catch (Exception e)
			{
				YupiLogManager.LogException("PrepareRoomForUser. RoomId: " + id + "; UserId: " + (GetHabbo().Id.ToString(CultureInfo.InvariantCulture) ?? "null") + Environment.NewLine + e, "Failed Preparing Room for User.");
			}
		}
    }
}
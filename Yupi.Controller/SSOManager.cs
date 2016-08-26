using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Protocol;
using System.Collections.Generic;
using Yupi.Messages.Contracts;
using Yupi.Net;
using System.Linq;
using Yupi.Util;
using Yupi.Util.Collections;

namespace Yupi.Controller
{
	public class SSOManager
	{
		private IRepository<UserInfo> UserRepository;
		private ClientManager ClientManager;
		private BiDictionary<string, UserInfo> Tickets;
		private AchievementManager AchievementManager;
		private ModerationTool ModerationTool;

		public SSOManager ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
			Tickets = new BiDictionary<string, UserInfo> ();
			AchievementManager = DependencyFactory.Resolve<AchievementManager> ();
			ModerationTool = DependencyFactory.Resolve<ModerationTool> ();
		}

		public void TryLogin (Habbo session, string ssoTicket)
		{
			UserInfo user;

			lock (Tickets) {
				Tickets.TryGetByFirst (ssoTicket, out user);
				Tickets.TryRemoveByFirst (ssoTicket);
			}

			if (user != null) {
				

				Habbo toDisconnect = ClientManager.GetByInfo (user);

				if (toDisconnect != null) {
					ClientManager.Disconnect (toDisconnect, "Logged in from other location.");
				}

				session.Info = user;

				// TODO Log login attempts (with IP)

				if (user.IsBanned ()) {
					ClientManager.Disconnect (session, "Banned from Server.");
				} else {

					session.Router.GetComposer<UniqueMachineIDMessageComposer> ().Compose (session, session.MachineId);  

					session.Router.GetComposer<AuthenticationOKMessageComposer> ().Compose (session);    

					session.Router.GetComposer<HomeRoomMessageComposer> ().Compose (session, user.HomeRoom == null ? 0 : user.HomeRoom.Id);  

					session.Router.GetComposer<MinimailCountMessageComposer> ().Compose (session, user.Minimail.Count (x => !x.Seen));  

					session.Router.GetComposer<FavouriteRoomsMessageComposer> ().Compose (session, user.FavoriteRooms);  

					session.Router.GetComposer<UserClubRightsMessageComposer> ().Compose (session, user.Subscription.IsValid (),
						user.Rank);     
					
					session.Router.GetComposer<EnableNotificationsMessageComposer> ().Compose (session);   

					if (user.CanTrade ()) {
						session.Router.GetComposer<EnableTradingMessageComposer> ().Compose (session);  
					}
					session.Router.GetComposer<CreditsBalanceMessageComposer> ().Compose (session, user.Wallet.Credits);  
					session.Router.GetComposer<ActivityPointsMessageComposer> ().Compose (session, user.Wallet);            

					if (user.HasPermission ("fuse_modtool")) {
						session.Router.GetComposer<LoadModerationToolMessageComposer> ().Compose (session, ModerationTool.Tickets, 
							ModerationTool.Templates, ModerationTool.UserMessagePresets, ModerationTool.RoomMessagePresets, user);
					}

					session.Router.GetComposer<SendAchievementsRequirementsMessageComposer> ().Compose (session, AchievementManager.Achievements);
					session.Router.GetComposer<EffectsInventoryMessageComposer> ().Compose (session, user.EffectComponent.Effects);

					AchievementManager.TryProgressHabboClubAchievements (session);
					AchievementManager.TryProgressRegistrationAchievements (session);
					AchievementManager.TryProgressLoginAchievements (session);
				}

			} else {
				ClientManager.Disconnect (session, "Invalid SSO Ticket");
			}
		}

		public string GenerateTicket (int userId) {
			UserInfo user = UserRepository.FindBy (userId);
			return GenerateTicket (user);
		}

		public string GenerateTicket (UserInfo info)
		{
			if (info == null) {
				return null;
			}

			lock (Tickets) {
				Tickets.TryRemoveBySecond (info);

				string ticket = Cryptography.GetUniqueKey (20);
			
				if (Tickets.TryAdd (ticket, info)) {
					return ticket;
				} else {
					return null;
				}
			}
		}
	}
}


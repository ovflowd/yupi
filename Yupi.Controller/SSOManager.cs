using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Protocol;
using System.Collections.Generic;
using Yupi.Messages.Contracts;
using Yupi.Net;
using System.Linq;

namespace Yupi.Controller
{
	public class SSOManager
	{
		private IRepository<UserInfo> UserRepository;
		private ClientManager ClientManager;
		private IDictionary<string, UserInfo> Tickets;
		private AchievementManager AchievementManager;
		private ModerationTool ModerationTool;

		public SSOManager ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
			Tickets = new Dictionary<string, UserInfo> ();
			AchievementManager = DependencyFactory.Resolve<AchievementManager> ();
			ModerationTool = DependencyFactory.Resolve<ModerationTool> ();
		}

		public void TryLogin(Habbo session, string ssoTicket) {
			if (Tickets.ContainsKey (ssoTicket)) {
				UserInfo user = Tickets [ssoTicket];

				session.Info = user;

				// TODO Log login attempts (with IP)

				if (user.IsBanned ()) {
					ClientManager.Disconnect (session, "Banned from Server.");
				} else {
					
					session.Router.GetComposer<UniqueMachineIDMessageComposer> ().Compose (session, session.MachineId);  

					session.Router.GetComposer<AuthenticationOKMessageComposer> ().Compose (session);    

					session.Router.GetComposer<HomeRoomMessageComposer> ().Compose (session, user.HomeRoom == null ? 0 : user.HomeRoom.Id);  

					session.Router.GetComposer<MinimailCountMessageComposer> ().Compose (session, user.Minimail.Count(x => !x.Seen));  

					session.Router.GetComposer<FavouriteRoomsMessageComposer> ().Compose (session, user.FavoriteRooms);  

					session.Router.GetComposer<UserClubRightsMessageComposer> ().Compose (session, user.Subscription.IsValid(),
						user.Rank);     
					
					session.Router.GetComposer<EnableNotificationsMessageComposer> ().Compose (session);   

					if (user.CanTrade ()) {
						session.Router.GetComposer<EnableTradingMessageComposer> ().Compose (session);  
					}
					session.Router.GetComposer<CreditsBalanceMessageComposer> ().Compose (session, user.Wallet.Credits);  
					session.Router.GetComposer<ActivityPointsMessageComposer> ().Compose (session, user.Wallet);            

					if (user.HasPermission("fuse_modtool")) {
						session.Router.GetComposer<LoadModerationToolMessageComposer>().Compose(session, ModerationTool.Tickets, 
							ModerationTool.Templates, ModerationTool.UserMessagePresets, ModerationTool.RoomMessagePresets, user);
					}

					session.Router.GetComposer<SendAchievementsRequirementsMessageComposer>().Compose(session, AchievementManager.Achievements);
					session.Router.GetComposer<EffectsInventoryMessageComposer>().Compose(session, user.EffectComponent.Effects);

					AchievementManager.TryProgressHabboClubAchievements(session);
					AchievementManager.TryProgressRegistrationAchievements(session);
					AchievementManager.TryProgressLoginAchievements(session);
				}

			} else {
				ClientManager.Disconnect (session, "Invalid SSO Ticket");
			}
		}

		public string GenerateTicket(UserInfo info) {
			Tickets.Clear ();
			Tickets.Add ("DEBUG", info);
			// FIXME Implement :D
			return "DEBUG";
		}
	}
}


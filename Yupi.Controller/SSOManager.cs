using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Protocol;
using System.Collections.Generic;
using Yupi.Messages.Contracts;

namespace Yupi.Controller
{
	public class SSOManager
	{
		private Repository<UserInfo> UserRepository;
		private ClientManager ClientManager;
		private IDictionary<string, UserInfo> Tickets;
		private AchievementManager AchievementManager;
		private ModerationTool ModerationTool;

		public SSOManager ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
			Tickets = new Dictionary<string, UserInfo> ();
			AchievementManager = DependencyFactory.Resolve<AchievementManager> ();
			ModerationTool = DependencyFactory.Resolve<ModerationTool> ();
		}

		public void TryLogin(ISession<Habbo> session, string ssoTicket) {
			if (Tickets.ContainsKey (ssoTicket)) {
				UserInfo user = Tickets [ssoTicket];

				session.UserData = new Habbo (session, user);

				// TODO Log login attempts (with IP)

				if (user.IsBanned ()) {
					ClientManager.Disconnect (session, "Banned from Server.");
				} else {
					
					session.Router.GetComposer<UniqueMachineIDMessageComposer> ().Compose (session, session.UserData.MachineId);  
					session.Router.GetComposer<AuthenticationOKMessageComposer> ().Compose (session);    
					session.Router.GetComposer<HomeRoomMessageComposer> ().Compose (session, user.HomeRoom.Id);  
				
					// TODO Implement
					//session.Router.GetComposer<MinimailCountMessageComposer> ().Compose (session, user.MinimailUnreadMessages);  

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

					AchievementManager.TryProgressHabboClubAchievements(session.UserData);
					AchievementManager.TryProgressRegistrationAchievements(session.UserData);
					AchievementManager.TryProgressLoginAchievements(session.UserData);
				}

			} else {
				ClientManager.Disconnect (session, "Invalid SSO Ticket");
			}
		}

		public void GenerateTicket() {
			throw new NotImplementedException ();
		}
	}
}


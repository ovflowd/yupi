using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Controller;
using System.Linq;


namespace Yupi.Messages.Support
{
	public class SubmitHelpTicketMessageEvent : AbstractHandler
	{
		private Repository<RoomData> RoomRepository;
		private Repository<UserInfo> UserRepository;
		private ClientManager ClientManager;

		public SubmitHelpTicketMessageEvent ()
		{
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
			RoomRepository = DependencyFactory.Resolve<Repository<RoomData>> ();
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string content = message.GetString();
			int category = message.GetInteger();
			int reportedUserId = message.GetInteger();
			int roomId = message.GetInteger();

			int messageCount = message.GetInteger();

			string[] chats = new string[messageCount];

			for (int i = 0; i < messageCount; ++i)
			{
				// TODO Unused?!
				message.GetInteger();
				// TODO Validate!
				chats[i] = message.GetString();
			}

			var pendingTickets = session.UserData.Info.SupportTickets.Where(x => x.Status != TicketStatus.Closed);
			var lastAbusive = session.UserData.Info.SupportTickets.OrderByDescending(x => x.CreatedAt).FirstOrDefault (x => x.Status == TicketStatus.Closed && x.CloseReason == TicketCloseReason.Abusive);

			if (pendingTickets.Count() != 0) {
				router.GetComposer<TicketUserAlertComposer> ().Compose (session, TicketUserAlertComposer.Status.HAS_PENDING, pendingTickets.First());
				// TODO Magic constant!
			} else if (lastAbusive != null && (DateTime.Now - lastAbusive.CreatedAt).TotalMinutes < 10) {
				router.GetComposer<TicketUserAlertComposer> ().Compose (session, TicketUserAlertComposer.Status.PREVIOUS_ABUSIVE);
			} else {
				router.GetComposer<TicketUserAlertComposer> ().Compose (session, TicketUserAlertComposer.Status.OK);

				RoomData room = RoomRepository.FindBy (roomId);
				UserInfo reportedUser = UserRepository.FindBy(reportedUserId);

				SupportTicket newTicket = new SupportTicket () {
					Category = category,
					Message = content,
					ReportedChats = chats,
					ReportedUser = reportedUser,
					Room = room,
					Type = 7 // TODO Hardcoded value
				};
						
				session.UserData.Info.SupportTickets.Add (newTicket);
				UserRepository.Save (session.UserData.Info);

				var staffs = ClientManager.Connections.Where(x => x.UserData.Info.HasPermission("handle_cfh"));

				foreach (var staff in staffs) {
					// TODO Create a controller for this...
					staff.Router.GetComposer<ModerationToolIssueMessageComposer> ().Compose (staff, newTicket);
				}
			}
		}
	}
}


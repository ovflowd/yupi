using System;
using Yupi.Model.Repository;
using Yupi.Controller;
using Yupi.Model.Domain;
using Yupi.Model;
using System.Linq;

namespace Yupi.Messages.Support
{
	public class ModerationToolCloseIssueMessageEvent : AbstractHandler
	{
		private Repository<SupportTicket> TicketRepository;
		private ClientManager ClientManager;

		public ModerationToolCloseIssueMessageEvent ()
		{
			TicketRepository = DependencyFactory.Resolve<Repository<SupportTicket>> ();
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission ("fuse_mod")) {
				return;
			}

			int result = message.GetInteger();

			message.GetInteger(); // TODO unused

			int ticketId = message.GetInteger();

			SupportTicket ticket = TicketRepository.FindBy (ticketId);

			TicketCloseReason reason;

			if (ticket != null && TicketCloseReason.TryFromInt32 (result, out reason)) {
				ticket.Close (reason);

				var staffs = ClientManager.Connections.Where(x => x.UserData.Info.HasPermission("handle_cfh"));

				foreach (var staff in staffs) {
					staff.Router.GetComposer<ModerationToolIssueMessageComposer> ().Compose (staff, ticket);
				}
			}
		}
	}
}


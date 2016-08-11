using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Controller;
using System.Linq;

namespace Yupi.Messages.Support
{
	public class ModerationToolPickIssueMessageEvent : AbstractHandler
	{
		private Repository<SupportTicket> TicketRepository;
		private ClientManager ClientManager;

		public ModerationToolPickIssueMessageEvent ()
		{
			TicketRepository = DependencyFactory.Resolve<Repository<SupportTicket>> ();
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission("fuse_mod"))
				return;

			message.GetInteger(); // TODO Unused
			int ticketId = message.GetInteger();

			SupportTicket ticket = TicketRepository.FindBy (ticketId);

			if (ticket == null || ticket.Status != TicketStatus.Closed) {
				return;
			}

			ticket.Pick (session.UserData.Info);

			TicketRepository.Save (ticket);

			var staffs = ClientManager.Connections.Where(x => x.UserData.Info.HasPermission("handle_cfh"));

			foreach (var staff in staffs) {
				staff.Router.GetComposer<ModerationToolIssueMessageComposer> ().Compose (staff, ticket);
			}
		}
	}
}


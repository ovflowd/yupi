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
		private IRepository<SupportTicket> TicketRepository;
		private ClientManager ClientManager;

		public ModerationToolPickIssueMessageEvent ()
		{
			TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>> ();
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.Info.HasPermission("fuse_mod"))
				return;

			message.GetInteger(); // TODO Unused
			int ticketId = message.GetInteger();

			SupportTicket ticket = TicketRepository.FindBy (ticketId);

			if (ticket == null || ticket.Status != TicketStatus.Closed) {
				return;
			}

			ticket.Pick (session.Info);

			TicketRepository.Save (ticket);

			foreach (Habbo staff in ClientManager.GetByPermission("handle_cfh")) {
				staff.Router.GetComposer<ModerationToolIssueMessageComposer> ().Compose (staff, ticket);
			}
		}
	}
}


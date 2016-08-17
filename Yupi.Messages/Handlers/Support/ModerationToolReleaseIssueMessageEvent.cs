using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;
using System.Linq;

namespace Yupi.Messages.Support
{
	public class ModerationToolReleaseIssueMessageEvent : AbstractHandler
	{
		private IRepository<SupportTicket> TicketRepository;
		private ClientManager ClientManager;

		public ModerationToolReleaseIssueMessageEvent ()
		{
			TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>> ();
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.Info.HasPermission("fuse_mod"))
				return;

			int ticketCount = message.GetInteger();

			for (int i = 0; i < ticketCount; i++) {
				int ticketId = message.GetInteger ();

				SupportTicket ticket = TicketRepository.FindBy (ticketId);

				if (ticket != null) {
					ticket.Release ();
					TicketRepository.Save (ticket);
				}

				foreach (Habbo staff in ClientManager.GetByPermission("handle_cfh")) {
					staff.Router.GetComposer<ModerationToolIssueMessageComposer> ().Compose (staff, ticket);
				}
			}
		}
	}
}


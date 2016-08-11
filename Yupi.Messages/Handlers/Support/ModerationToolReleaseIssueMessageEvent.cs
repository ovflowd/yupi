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
		private Repository<SupportTicket> TicketRepository;
		private ClientManager ClientManager;

		public ModerationToolReleaseIssueMessageEvent ()
		{
			TicketRepository = DependencyFactory.Resolve<Repository<SupportTicket>> ();
			ClientManager = DependencyFactory.Resolve<ClientManager> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission("fuse_mod"))
				return;

			int ticketCount = message.GetInteger();

			for (int i = 0; i < ticketCount; i++) {
				int ticketId = message.GetInteger ();

				SupportTicket ticket = TicketRepository.FindBy (ticketId);

				if (ticket != null) {
					ticket.Release ();
					TicketRepository.Save (ticket);
				}

				var staffs = ClientManager.Connections.Where(x => x.UserData.Info.HasPermission("handle_cfh"));

				foreach (var staff in staffs) {
					staff.Router.GetComposer<ModerationToolIssueMessageComposer> ().Compose (staff, ticket);
				}
			}
		}
	}
}


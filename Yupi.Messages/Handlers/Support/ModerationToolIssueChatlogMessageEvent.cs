using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;



namespace Yupi.Messages.Support
{
	public class ModerationToolIssueChatlogMessageEvent : AbstractHandler
	{
		private IRepository<SupportTicket> TicketRepository;

		public ModerationToolIssueChatlogMessageEvent ()
		{
			TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.Info.HasPermission ("fuse_mod"))
				return;

			int ticketId = message.GetInteger ();

			SupportTicket ticket = TicketRepository.FindBy (ticketId);

			if (ticket != null) {
				router.GetComposer<ModerationToolIssueChatlogMessageComposer> ().Compose (session, ticket);
			}
		}
	}
}


using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;



namespace Yupi.Messages.Support
{
	public class ModerationToolIssueChatlogMessageEvent : AbstractHandler
	{
		private Repository<SupportTicket> TicketRepository;

		public ModerationToolIssueChatlogMessageEvent ()
		{
			TicketRepository = DependencyFactory.Resolve<Repository<SupportTicket>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission ("fuse_mod"))
				return;

			int ticketId = message.GetInteger ();

			SupportTicket ticket = TicketRepository.FindBy (ticketId);

			if (ticket != null) {
				router.GetComposer<ModerationToolIssueChatlogMessageComposer> ().Compose (session, ticket);
			}
		}
	}
}


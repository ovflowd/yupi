using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationToolIssueChatlogMessageEvent : AbstractHandler
    {
        private readonly IRepository<SupportTicket> TicketRepository;

        public ModerationToolIssueChatlogMessageEvent()
        {
            TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod"))
                return;

            var ticketId = message.GetInteger();

            var ticket = TicketRepository.FindBy(ticketId);

            if (ticket != null)
                router.GetComposer<ModerationToolIssueChatlogMessageComposer>().Compose(session, ticket);
        }
    }
}
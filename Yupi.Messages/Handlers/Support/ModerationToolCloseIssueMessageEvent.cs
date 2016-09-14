using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationToolCloseIssueMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;
        private readonly IRepository<SupportTicket> TicketRepository;

        public ModerationToolCloseIssueMessageEvent()
        {
            TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod")) return;

            var result = message.GetInteger();

            message.GetInteger(); // TODO unused

            var ticketId = message.GetInteger();

            var ticket = TicketRepository.FindBy(ticketId);

            TicketCloseReason reason;

            if ((ticket != null) && TicketCloseReason.TryFromInt32(result, out reason))
            {
                ticket.Close(reason);

                foreach (var staff in ClientManager.GetByPermission("handle_cfh"))
                    staff.Router.GetComposer<ModerationToolIssueMessageComposer>().Compose(staff, ticket);
            }
        }
    }
}
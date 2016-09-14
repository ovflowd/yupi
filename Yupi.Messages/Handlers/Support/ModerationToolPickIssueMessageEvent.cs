using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationToolPickIssueMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;
        private readonly IRepository<SupportTicket> TicketRepository;

        public ModerationToolPickIssueMessageEvent()
        {
            TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod"))
                return;

            message.GetInteger(); // TODO Unused
            var ticketId = message.GetInteger();

            var ticket = TicketRepository.FindBy(ticketId);

            if ((ticket == null) || (ticket.Status != TicketStatus.Closed)) return;

            ticket.Pick(session.Info);

            TicketRepository.Save(ticket);

            foreach (var staff in ClientManager.GetByPermission("handle_cfh"))
                staff.Router.GetComposer<ModerationToolIssueMessageComposer>().Compose(staff, ticket);
        }
    }
}
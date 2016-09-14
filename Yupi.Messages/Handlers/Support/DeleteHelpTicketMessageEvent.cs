using System.Linq;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class DeleteHelpTicketMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;
        private readonly IRepository<SupportTicket> TicketRepository;

        public DeleteHelpTicketMessageEvent()
        {
            TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var openTickets = session.Info.SupportTickets.Where(x => x.Status != TicketStatus.Closed);

            foreach (var ticket in openTickets)
            {
                ticket.Close(TicketCloseReason.Deleted);
                TicketRepository.Save(ticket);

                foreach (var staff in ClientManager.GetByPermission("handle_cfh"))
                    staff.Router.GetComposer<ModerationToolIssueMessageComposer>().Compose(staff, ticket);
            }

            router.GetComposer<OpenHelpToolMessageComposer>().Compose(session, openTickets.ToList());
        }
    }
}
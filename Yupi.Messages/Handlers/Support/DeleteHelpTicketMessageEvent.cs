using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;
using System.Linq;
using Yupi.Controller;

namespace Yupi.Messages.Support
{
    public class DeleteHelpTicketMessageEvent : AbstractHandler
    {
        private IRepository<SupportTicket> TicketRepository;
        private ClientManager ClientManager;

        public DeleteHelpTicketMessageEvent()
        {
            TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            var openTickets = session.Info.SupportTickets.Where(x => x.Status != TicketStatus.Closed);

            foreach (SupportTicket ticket in openTickets)
            {
                ticket.Close(TicketCloseReason.Deleted);
                TicketRepository.Save(ticket);

                foreach (Habbo staff in ClientManager.GetByPermission("handle_cfh"))
                {
                    staff.Router.GetComposer<ModerationToolIssueMessageComposer>().Compose(staff, ticket);
                }
            }

            router.GetComposer<OpenHelpToolMessageComposer>().Compose(session, openTickets.ToList());
        }
    }
}
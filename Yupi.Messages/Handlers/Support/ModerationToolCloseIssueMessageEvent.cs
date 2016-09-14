using System;
using Yupi.Model.Repository;
using Yupi.Controller;
using Yupi.Model.Domain;
using Yupi.Model;
using System.Linq;

namespace Yupi.Messages.Support
{
    public class ModerationToolCloseIssueMessageEvent : AbstractHandler
    {
        private IRepository<SupportTicket> TicketRepository;
        private ClientManager ClientManager;

        public ModerationToolCloseIssueMessageEvent()
        {
            TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod"))
            {
                return;
            }

            int result = message.GetInteger();

            message.GetInteger(); // TODO unused

            int ticketId = message.GetInteger();

            SupportTicket ticket = TicketRepository.FindBy(ticketId);

            TicketCloseReason reason;

            if (ticket != null && TicketCloseReason.TryFromInt32(result, out reason))
            {
                ticket.Close(reason);

                foreach (Habbo staff in ClientManager.GetByPermission("handle_cfh"))
                {
                    staff.Router.GetComposer<ModerationToolIssueMessageComposer>().Compose(staff, ticket);
                }
            }
        }
    }
}
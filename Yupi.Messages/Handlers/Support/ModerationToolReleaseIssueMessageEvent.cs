using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class ModerationToolReleaseIssueMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;
        private readonly IRepository<SupportTicket> TicketRepository;

        public ModerationToolReleaseIssueMessageEvent()
        {
            TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod"))
                return;

            var ticketCount = message.GetInteger();

            for (var i = 0; i < ticketCount; i++)
            {
                var ticketId = message.GetInteger();

                var ticket = TicketRepository.FindBy(ticketId);

                if (ticket != null)
                {
                    ticket.Release();
                    TicketRepository.Save(ticket);
                }

                foreach (var staff in ClientManager.GetByPermission("handle_cfh"))
                    staff.Router.GetComposer<ModerationToolIssueMessageComposer>().Compose(staff, ticket);
            }
        }
    }
}
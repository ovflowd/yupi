namespace Yupi.Messages.Support
{
    using System;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class DeleteHelpTicketMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<SupportTicket> TicketRepository;

        #endregion Fields

        #region Constructors

        public DeleteHelpTicketMessageEvent()
        {
            TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

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

        #endregion Methods
    }
}
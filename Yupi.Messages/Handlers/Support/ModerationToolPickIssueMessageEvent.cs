namespace Yupi.Messages.Support
{
    using System;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ModerationToolPickIssueMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<SupportTicket> TicketRepository;

        #endregion Fields

        #region Constructors

        public ModerationToolPickIssueMessageEvent()
        {
            TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod"))
                return;

            message.GetInteger(); // TODO Unused
            int ticketId = message.GetInteger();

            SupportTicket ticket = TicketRepository.FindBy(ticketId);

            if (ticket == null || ticket.Status != TicketStatus.Closed)
            {
                return;
            }

            ticket.Pick(session.Info);

            TicketRepository.Save(ticket);

            foreach (Habbo staff in ClientManager.GetByPermission("handle_cfh"))
            {
                staff.Router.GetComposer<ModerationToolIssueMessageComposer>().Compose(staff, ticket);
            }
        }

        #endregion Methods
    }
}
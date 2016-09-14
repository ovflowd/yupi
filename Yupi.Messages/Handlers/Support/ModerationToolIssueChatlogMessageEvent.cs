namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ModerationToolIssueChatlogMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<SupportTicket> TicketRepository;

        #endregion Fields

        #region Constructors

        public ModerationToolIssueChatlogMessageEvent()
        {
            TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod"))
                return;

            int ticketId = message.GetInteger();

            SupportTicket ticket = TicketRepository.FindBy(ticketId);

            if (ticket != null)
            {
                router.GetComposer<ModerationToolIssueChatlogMessageComposer>().Compose(session, ticket);
            }
        }

        #endregion Methods
    }
}
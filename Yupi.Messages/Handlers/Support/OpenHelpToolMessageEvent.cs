namespace Yupi.Messages.Support
{
    using System;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class OpenHelpToolMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            var openTickets = session.Info.SupportTickets.Where(x => x.Status != TicketStatus.Closed);
            router.GetComposer<OpenHelpToolMessageComposer>().Compose(session, openTickets.ToList());
        }

        #endregion Methods
    }
}
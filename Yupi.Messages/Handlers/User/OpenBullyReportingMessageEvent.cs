namespace Yupi.Messages.User
{
    using System;

    using Yupi.Net;
    using Yupi.Protocol.Buffers;

    public class OpenBullyReportingMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<OpenBullyReportMessageComposer>().Compose(session);
        }

        #endregion Methods
    }
}
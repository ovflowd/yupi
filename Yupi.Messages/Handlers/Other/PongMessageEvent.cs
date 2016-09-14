namespace Yupi.Messages.Other
{
    using System;

    public class PongMessageEvent : AbstractHandler
    {
        #region Properties

        public override bool RequireUser
        {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            session.TimePingReceived = DateTime.Now;
        }

        #endregion Methods
    }
}
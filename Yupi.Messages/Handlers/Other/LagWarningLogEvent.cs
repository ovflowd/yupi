namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public class LagWarningLogEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            int lagCount = request.GetInteger();

            // TODO Now what?!
        }

        #endregion Methods
    }
}
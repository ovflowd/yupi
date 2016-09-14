namespace Yupi.Messages.Trade
{
    using System;

    using Yupi.Protocol.Buffers;

    public class TradeAcceptMessageComposer : Yupi.Messages.Contracts.TradeAcceptMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint userId, bool accepted)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(userId);
                message.AppendInteger(accepted);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
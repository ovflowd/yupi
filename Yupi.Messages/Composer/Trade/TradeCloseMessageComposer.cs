namespace Yupi.Messages.Trade
{
    using System;

    using Yupi.Protocol.Buffers;

    public class TradeCloseMessageComposer : Yupi.Messages.Contracts.TradeCloseMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint closedById)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(closedById);
                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
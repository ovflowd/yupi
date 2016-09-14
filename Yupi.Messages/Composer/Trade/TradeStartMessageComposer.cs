namespace Yupi.Messages.Trade
{
    using System;

    using Yupi.Protocol.Buffers;

    public class TradeStartMessageComposer : Yupi.Messages.Contracts.TradeStartMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint firstUserId, uint secondUserId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(firstUserId);
                message.AppendInteger(1);
                message.AppendInteger(secondUserId);
                message.AppendInteger(1);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
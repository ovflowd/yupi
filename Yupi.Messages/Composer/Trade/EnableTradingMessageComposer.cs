namespace Yupi.Messages.Trade
{
    using System;

    using Yupi.Protocol.Buffers;

    public class EnableTradingMessageComposer : Yupi.Messages.Contracts.EnableTradingMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(true);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
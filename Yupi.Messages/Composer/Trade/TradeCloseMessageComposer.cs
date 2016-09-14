using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Trade
{
    public class TradeCloseMessageComposer : Yupi.Messages.Contracts.TradeCloseMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, uint closedById)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(closedById);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}
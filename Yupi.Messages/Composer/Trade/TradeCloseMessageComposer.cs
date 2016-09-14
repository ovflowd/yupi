using Yupi.Protocol;

namespace Yupi.Messages.Trade
{
    public class TradeCloseMessageComposer : Contracts.TradeCloseMessageComposer
    {
        public override void Compose(ISender session, uint closedById)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(closedById);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}
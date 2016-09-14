using Yupi.Protocol;

namespace Yupi.Messages.Trade
{
    public class TradeStartMessageComposer : Contracts.TradeStartMessageComposer
    {
        public override void Compose(ISender session, uint firstUserId, uint secondUserId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(firstUserId);
                message.AppendInteger(1);
                message.AppendInteger(secondUserId);
                message.AppendInteger(1);
                session.Send(message);
            }
        }
    }
}
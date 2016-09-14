using Yupi.Protocol;

namespace Yupi.Messages.Trade
{
    public class TradeAcceptMessageComposer : Contracts.TradeAcceptMessageComposer
    {
        public override void Compose(ISender session, uint userId, bool accepted)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(userId);
                message.AppendInteger(accepted);
                session.Send(message);
            }
        }
    }
}
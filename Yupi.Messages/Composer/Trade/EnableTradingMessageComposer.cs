using Yupi.Protocol;

namespace Yupi.Messages.Trade
{
    public class EnableTradingMessageComposer : Contracts.EnableTradingMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(true);
                session.Send(message);
            }
        }
    }
}
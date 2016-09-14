using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class TradeUpdateMessageComposer : AbstractComposer<TradeUser, TradeUser>
    {
        public override void Compose(Yupi.Protocol.ISender session, TradeUser first, TradeUser second)
        {
            // Do nothing by default.
        }
    }
}
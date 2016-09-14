using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class TradeUpdateMessageComposer : AbstractComposer<TradeUser, TradeUser>
    {
        public override void Compose(ISender session, TradeUser first, TradeUser second)
        {
            // Do nothing by default.
        }
    }
}
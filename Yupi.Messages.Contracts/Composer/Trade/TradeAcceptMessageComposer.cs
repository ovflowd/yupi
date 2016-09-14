using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class TradeAcceptMessageComposer : AbstractComposer<uint, bool>
    {
        public override void Compose(ISender session, uint userId, bool accepted)
        {
            // Do nothing by default.
        }
    }
}
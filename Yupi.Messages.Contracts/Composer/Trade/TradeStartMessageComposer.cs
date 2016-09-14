using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class TradeStartMessageComposer : AbstractComposer<uint, uint>
    {
        public override void Compose(ISender session, uint firstUserId, uint secondUserId)
        {
            // Do nothing by default.
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class TradeCloseMessageComposer : AbstractComposer<uint>
    {
        public override void Compose(ISender session, uint closedById)
        {
            // Do nothing by default.
        }
    }
}
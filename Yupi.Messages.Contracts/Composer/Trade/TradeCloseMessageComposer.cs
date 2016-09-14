using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class TradeCloseMessageComposer : AbstractComposer<uint>
    {
        public override void Compose(Yupi.Protocol.ISender session, uint closedById)
        {
            // Do nothing by default.
        }
    }
}
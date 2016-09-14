using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class EnableTradingMessageComposer : AbstractComposerVoid
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            // Do nothing by default.
        }
    }
}
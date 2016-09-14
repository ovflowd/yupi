using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class EnableTradingMessageComposer : AbstractComposerVoid
    {
        public override void Compose(ISender session)
        {
            // Do nothing by default.
        }
    }
}
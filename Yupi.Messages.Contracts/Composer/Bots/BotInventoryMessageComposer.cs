using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class BotInventoryMessageComposer : AbstractComposer<HybridDictionary>
    {
        public override void Compose(ISender session, HybridDictionary items)
        {
            // Do nothing by default.
        }
    }
}
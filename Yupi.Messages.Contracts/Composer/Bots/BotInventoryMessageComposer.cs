using System.Collections.Specialized;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class BotInventoryMessageComposer : AbstractComposer<HybridDictionary>
    {
        public override void Compose(Yupi.Protocol.ISender session, HybridDictionary items)
        {
            // Do nothing by default.
        }
    }
}
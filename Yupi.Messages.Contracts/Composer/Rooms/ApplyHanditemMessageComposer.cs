using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ApplyHanditemMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(ISender session, int virtualId, int itemId)
        {
            // Do nothing by default.
        }
    }
}
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class OnGuideSessionDetachedMessageComposer : AbstractComposer<int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int value)
        {
            // Do nothing by default.
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class OnGuideSessionDetachedMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int value)
        {
            // Do nothing by default.
        }
    }
}
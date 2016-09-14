using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class WiredRewardAlertMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int status)
        {
            // Do nothing by default.
        }
    }
}
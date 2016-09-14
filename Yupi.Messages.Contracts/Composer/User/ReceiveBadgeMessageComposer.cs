using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ReceiveBadgeMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string badgeId)
        {
            // Do nothing by default.
        }
    }
}
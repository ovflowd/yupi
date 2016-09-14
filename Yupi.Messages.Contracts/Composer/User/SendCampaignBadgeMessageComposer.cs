using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SendCampaignBadgeMessageComposer : AbstractComposer<string, bool>
    {
        public override void Compose(ISender session, string badgeName, bool hasBadge)
        {
            // Do nothing by default.
        }
    }
}
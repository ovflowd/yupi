using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class SendCampaignBadgeMessageComposer : AbstractComposer<string, bool>
	{
		public override void Compose(Yupi.Protocol.ISender session, string badgeName, bool hasBadge)
		{
		 // Do nothing by default.
		}
	}
}

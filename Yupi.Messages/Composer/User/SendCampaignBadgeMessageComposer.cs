using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.User
{
	public class SendCampaignBadgeMessageComposer : Yupi.Messages.Contracts.SendCampaignBadgeMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, string badgeName, bool hasBadge)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (badgeName);
				message.AppendBool (hasBadge);
				session.Send (message);
			}
		}
	}
}


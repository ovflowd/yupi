using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.User
{
	public class SendCampaignBadgeMessageComposer : AbstractComposer<string, bool>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, string badgeName, bool hasBadge)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (badgeName);
				message.AppendBool (hasBadge);
				session.Send (message);
			}
		}
	}
}


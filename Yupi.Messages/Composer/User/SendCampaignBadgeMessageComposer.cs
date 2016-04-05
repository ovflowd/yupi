using System;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class SendCampaignBadgeMessageComposer : AbstractComposer
	{
		public override void Compose (GameClient session, string badgeName, bool hasBadge)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (badgeName);
				message.AppendBool (hasBadge);
				session.Send (message);
			}
		}
	}
}


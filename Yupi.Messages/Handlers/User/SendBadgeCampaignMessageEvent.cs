using System;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class SendBadgeCampaignMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			string text = message.GetString();
			bool hasBadge = session.GetHabbo ().GetBadgeComponent ().HasBadge (text);

			router.GetComposer<SendCampaignBadgeMessageComposer> ().Compose (session, text, hasBadge);
		}
	}
}


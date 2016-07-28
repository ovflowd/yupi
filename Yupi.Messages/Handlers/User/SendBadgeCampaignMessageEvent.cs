using System;


namespace Yupi.Messages.User
{
	public class SendBadgeCampaignMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string text = message.GetString();
			bool hasBadge = session.GetHabbo ().GetBadgeComponent ().HasBadge (text);

			router.GetComposer<SendCampaignBadgeMessageComposer> ().Compose (session, text, hasBadge);
		}
	}
}


using System;


namespace Yupi.Messages.User
{
	public class SendBadgeCampaignMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string badgeCode = message.GetString();
			bool hasBadge = session.UserData.Info.Badges.HasBadge (badgeCode);

			router.GetComposer<SendCampaignBadgeMessageComposer> ().Compose (session, badgeCode, hasBadge);
		}
	}
}


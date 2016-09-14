namespace Yupi.Messages.User
{
    using System;

    public class SendBadgeCampaignMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string badgeCode = message.GetString();
            bool hasBadge = session.Info.Badges.HasBadge(badgeCode);

            router.GetComposer<SendCampaignBadgeMessageComposer>().Compose(session, badgeCode, hasBadge);
        }

        #endregion Methods
    }
}
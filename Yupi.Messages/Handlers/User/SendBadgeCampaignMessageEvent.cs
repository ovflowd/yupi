using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class SendBadgeCampaignMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var badgeCode = message.GetString();
            var hasBadge = session.Info.Badges.HasBadge(badgeCode);

            router.GetComposer<SendCampaignBadgeMessageComposer>().Compose(session, badgeCode, hasBadge);
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class SendCampaignBadgeMessageComposer : Contracts.SendCampaignBadgeMessageComposer
    {
        public override void Compose(ISender session, string badgeName, bool hasBadge)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(badgeName);
                message.AppendBool(hasBadge);
                session.Send(message);
            }
        }
    }
}
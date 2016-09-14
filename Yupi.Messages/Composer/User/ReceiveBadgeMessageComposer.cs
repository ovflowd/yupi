using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class ReceiveBadgeMessageComposer : Contracts.ReceiveBadgeMessageComposer
    {
        public override void Compose(ISender session, string badgeId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendString(badgeId);
                session.Send(message);
            }
        }
    }
}
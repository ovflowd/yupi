using Yupi.Protocol;

namespace Yupi.Messages.Notification
{
    public class CustomUserNotificationMessageComposer : Contracts.CustomUserNotificationMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(3);
                session.Send(message);
            }
        }
    }
}
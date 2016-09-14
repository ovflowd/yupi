using Yupi.Protocol;

namespace Yupi.Messages.Notification
{
    public class MOTDNotificationMessageComposer : Contracts.MOTDNotificationMessageComposer
    {
        public override void Compose(ISender session, string text)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendString(text);
                session.Send(message);
            }
        }
    }
}
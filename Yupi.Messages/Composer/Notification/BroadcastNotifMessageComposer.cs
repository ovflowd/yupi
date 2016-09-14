using Yupi.Protocol;

namespace Yupi.Messages.Notification
{
    public class BroadcastNotifMessageComposer : Contracts.BroadcastNotifMessageComposer
    {
        public override void Compose(ISender session, string text)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(text);
                message.AppendString(string.Empty);
                session.Send(message);
            }
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.Messenger
{
    public class ConsoleInvitationMessageComposer : Contracts.ConsoleInvitationMessageComposer
    {
        public override void Compose(ISender session, int habboId, string content)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(habboId);
                message.AppendString(content);
                session.Send(message);
            }
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.Messenger
{
    public class ConsoleChatErrorMessageComposer : Contracts.ConsoleChatErrorMessageComposer
    {
        public override void Compose(ISender session, int errorId, uint conversationId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(errorId);
                message.AppendInteger(conversationId);
                message.AppendString(string.Empty);
                session.Send(message);
            }
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class TypingStatusMessageComposer : Contracts.TypingStatusMessageComposer
    {
        public override void Compose(ISender session, int virtualId, bool isTyping)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(virtualId);
                message.AppendInteger(isTyping);
                session.Send(message);
            }
        }
    }
}
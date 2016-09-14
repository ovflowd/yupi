using Yupi.Messages.Encoders;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Chat
{
    public class ShoutMessageComposer : Contracts.ShoutMessageComposer
    {
        public override void Compose(ISender session, ChatMessage msg, int count = -1)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.Append(msg, count);
                session.Send(message);
            }
        }
    }
}
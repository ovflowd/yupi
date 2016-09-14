using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ChatMessageComposer : AbstractComposer<ChatMessage, int>
    {
        public override void Compose(ISender session, ChatMessage msg, int count = -1)
        {
            // Do nothing by default.
        }
    }
}
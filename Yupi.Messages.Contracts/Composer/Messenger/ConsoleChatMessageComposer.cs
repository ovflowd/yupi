using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class ConsoleChatMessageComposer : AbstractComposer<MessengerMessage>
    {
        public override void Compose(Yupi.Protocol.ISender session, MessengerMessage message)
        {
            // Do nothing by default.
        }
    }
}
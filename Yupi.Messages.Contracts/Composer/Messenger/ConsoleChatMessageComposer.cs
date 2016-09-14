using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ConsoleChatMessageComposer : AbstractComposer<MessengerMessage>
    {
        public override void Compose(ISender session, MessengerMessage message)
        {
            // Do nothing by default.
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ConsoleChatErrorMessageComposer : AbstractComposer<int, uint>
    {
        public override void Compose(ISender session, int errorId, uint conversationId)
        {
            // Do nothing by default.
        }
    }
}
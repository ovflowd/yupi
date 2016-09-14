using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class ConsoleChatErrorMessageComposer : AbstractComposer<int, uint>
    {
        public override void Compose(Yupi.Protocol.ISender session, int errorId, uint conversationId)
        {
            // Do nothing by default.
        }
    }
}
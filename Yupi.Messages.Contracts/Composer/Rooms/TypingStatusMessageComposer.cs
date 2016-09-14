using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class TypingStatusMessageComposer : AbstractComposer<int, bool>
    {
        public override void Compose(ISender session, int virtualId, bool isTyping)
        {
            // Do nothing by default.
        }
    }
}
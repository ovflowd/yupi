using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class ConsoleInvitationMessageComposer : AbstractComposer<int, string>
    {
        public override void Compose(Yupi.Protocol.ISender session, int habboId, string content)
        {
            // Do nothing by default.
        }
    }
}
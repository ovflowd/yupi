using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ConsoleInvitationMessageComposer : AbstractComposer<int, string>
    {
        public override void Compose(ISender session, int habboId, string content)
        {
            // Do nothing by default.
        }
    }
}
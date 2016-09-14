using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class ReceiveBadgeMessageComposer : AbstractComposer<string>
    {
        public override void Compose(Yupi.Protocol.ISender session, string badgeId)
        {
            // Do nothing by default.
        }
    }
}
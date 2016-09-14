using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class MatchingPollAnsweredMessageComposer : AbstractComposer<uint, string>
    {
        public override void Compose(Yupi.Protocol.ISender session, uint userId, string text)
        {
            // Do nothing by default.
        }
    }
}
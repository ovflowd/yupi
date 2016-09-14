using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class MatchingPollAnsweredMessageComposer : AbstractComposer<uint, string>
    {
        public override void Compose(ISender session, uint userId, string text)
        {
            // Do nothing by default.
        }
    }
}
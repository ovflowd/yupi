using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SuggestPollMessageComposer : AbstractComposer<Poll>
    {
        public override void Compose(ISender session, Poll poll)
        {
            // Do nothing by default.
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomRatingMessageComposer : AbstractComposer<int, bool>
    {
        public override void Compose(ISender session, int rating, bool canVote)
        {
            // Do nothing by default.
        }
    }
}
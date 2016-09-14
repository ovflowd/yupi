using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomRatingMessageComposer : AbstractComposer<int, bool>
    {
        public override void Compose(Yupi.Protocol.ISender session, int rating, bool canVote)
        {
            // Do nothing by default.
        }
    }
}
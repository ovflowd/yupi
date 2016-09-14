using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomRatingMessageComposer : Contracts.RoomRatingMessageComposer
    {
        public override void Compose(ISender session, int rating, bool canVote)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(rating);
                message.AppendBool(canVote);
                session.Send(message);
            }
        }
    }
}
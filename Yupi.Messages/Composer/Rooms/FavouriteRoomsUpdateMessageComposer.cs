using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class FavouriteRoomsUpdateMessageComposer : Contracts.FavouriteRoomsUpdateMessageComposer
    {
        public override void Compose(ISender session, int roomId, bool isAdded)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendBool(isAdded);
                session.Send(message);
            }
        }
    }
}
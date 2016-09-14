using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class CanCreateRoomMessageComposer : Contracts.CanCreateRoomMessageComposer
    {
        public override void Compose(ISender session, UserInfo user)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(user.UsersRooms.Count >= 75 ? 1 : 0); // TODO Enum
                message.AppendInteger(75); // TODO Magic number
                session.Send(message);
            }
        }
    }
}
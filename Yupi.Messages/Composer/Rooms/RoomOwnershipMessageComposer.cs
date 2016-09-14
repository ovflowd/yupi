using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomOwnershipMessageComposer : Contracts.RoomOwnershipMessageComposer
    {
        public override void Compose(ISender session, RoomData room, UserInfo user)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(room.Id);
                message.AppendBool(room.HasOwnerRights(user));
                session.Send(message);
            }
        }
    }
}
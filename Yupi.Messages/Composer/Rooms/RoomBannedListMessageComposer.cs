using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomBannedListMessageComposer : Contracts.RoomBannedListMessageComposer
    {
        public override void Compose(ISender session, RoomData room)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(room.Id);
                message.AppendInteger(room.BannedUsers.Count);

                foreach (var user in room.BannedUsers)
                {
                    message.AppendInteger(user.Id);
                    message.AppendString(user.Name);
                }
                session.Send(message);
            }
        }
    }
}
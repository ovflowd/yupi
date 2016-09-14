using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class InitialRoomInfoMessageComposer : Contracts.InitialRoomInfoMessageComposer
    {
        public override void Compose(ISender session, RoomData room)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(room.Model.DisplayName);
                message.AppendInteger(room.Id);
                session.Send(message);
            }
        }
    }
}
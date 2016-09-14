using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class OnCreateRoomInfoMessageComposer : Contracts.OnCreateRoomInfoMessageComposer
    {
        public override void Compose(ISender session, RoomData data)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(data.Id);
                message.AppendString(data.Name);
                session.Send(message);
            }
        }
    }
}
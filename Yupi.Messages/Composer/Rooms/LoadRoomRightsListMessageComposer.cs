using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class LoadRoomRightsListMessageComposer : Contracts.LoadRoomRightsListMessageComposer
    {
        public override void Compose(ISender session, RoomData room)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(room.Id);
                message.AppendInteger(room.Rights.Count);

                foreach (var habboForId in room.Rights)
                {
                    message.AppendInteger(habboForId.Id);
                    message.AppendString(habboForId.Name);
                }

                session.Send(message);
            }
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomUserIdleMessageComposer : Contracts.RoomUserIdleMessageComposer
    {
        public override void Compose(ISender room, int entityId, bool isAsleep)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entityId);
                message.AppendBool(isAsleep);
                room.Send(message);
            }
        }
    }
}
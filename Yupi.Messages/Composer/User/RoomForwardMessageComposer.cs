using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class RoomForwardMessageComposer : Contracts.RoomForwardMessageComposer
    {
        // TODO Use RoomInfo
        public override void Compose(ISender session, int roomId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                session.Send(message);
            }
        }
    }
}
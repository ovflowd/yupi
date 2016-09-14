using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class HomeRoomMessageComposer : Contracts.HomeRoomMessageComposer
    {
        public override void Compose(ISender session, int roomId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendInteger(0); // TODO Hardcoded
                session.Send(message);
            }
        }
    }
}
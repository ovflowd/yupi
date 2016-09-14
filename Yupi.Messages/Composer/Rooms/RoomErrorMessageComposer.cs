using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomErrorMessageComposer : Contracts.RoomErrorMessageComposer
    {
        // TODO ErrorCode???
        public override void Compose(ISender session, int errorCode)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(errorCode);
                session.Send(message);
            }
        }
    }
}
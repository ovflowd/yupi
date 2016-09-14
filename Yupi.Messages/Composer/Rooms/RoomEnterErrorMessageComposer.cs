using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomEnterErrorMessageComposer : Contracts.RoomEnterErrorMessageComposer
    {
        public override void Compose(ISender session, Error error)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) error);
                session.Send(message);
            }
        }
    }
}
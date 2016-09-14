using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class UserLeftRoomMessageComposer : Contracts.UserLeftRoomMessageComposer
    {
        public override void Compose(ISender session, int virtualId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                // TODO VirtualId TO STRING?!
                message.AppendString(virtualId.ToString());
                session.Send(message);
            }
        }
    }
}
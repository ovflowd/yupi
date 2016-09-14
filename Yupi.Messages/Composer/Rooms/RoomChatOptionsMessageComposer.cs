using Yupi.Messages.Encoders;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomChatOptionsMessageComposer : Contracts.RoomChatOptionsMessageComposer
    {
        public override void Compose(ISender session, RoomData data)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.Append(data.Chat);
                session.Send(message);
            }
        }
    }
}
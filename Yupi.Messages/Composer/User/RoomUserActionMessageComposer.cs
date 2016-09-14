using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class RoomUserActionMessageComposer : Contracts.RoomUserActionMessageComposer
    {
        // TODO unknown param?!
        public override void Compose(ISender room, int virtualId, UserAction action)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(virtualId);
                message.AppendInteger(action.Value);
                room.Send(message);
            }
        }
    }
}
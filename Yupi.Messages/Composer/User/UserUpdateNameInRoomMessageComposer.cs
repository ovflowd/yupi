using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class UserUpdateNameInRoomMessageComposer : Contracts.UserUpdateNameInRoomMessageComposer
    {
        public override void Compose(ISender room, Habbo habbo)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(habbo.Info.Id);
                message.AppendInteger(habbo.Room.Data.Id);
                message.AppendString(habbo.Info.Name);
                room.Send(message);
            }
        }
    }
}
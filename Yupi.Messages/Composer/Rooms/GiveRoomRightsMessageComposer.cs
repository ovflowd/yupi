using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class GiveRoomRightsMessageComposer : Contracts.GiveRoomRightsMessageComposer
    {
        public override void Compose(ISender session, int roomId, UserInfo habbo)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendInteger(habbo.Id);
                message.AppendString(habbo.Name);
                session.Send(message);
            }
        }
    }
}
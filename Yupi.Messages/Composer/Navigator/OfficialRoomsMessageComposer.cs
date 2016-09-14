using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Navigator
{
    public class OfficialRoomsMessageComposer : Contracts.OfficialRoomsMessageComposer
    {
        public override void Compose(ISender session, RoomData roomData)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomData.Id);
                message.AppendString(roomData.CCTs);
                message.AppendInteger(roomData.Id);
                session.Send(message);
            }
        }
    }
}
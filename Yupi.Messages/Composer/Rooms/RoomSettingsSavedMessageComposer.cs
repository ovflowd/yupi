using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomSettingsSavedMessageComposer : Contracts.RoomSettingsSavedMessageComposer
    {
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
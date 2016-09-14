using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomMuteStatusMessageComposer : Contracts.RoomMuteStatusMessageComposer
    {
        public override void Compose(ISender session, bool isMuted)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(isMuted);
                session.Send(message);
            }
        }
    }
}
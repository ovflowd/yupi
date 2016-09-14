using Yupi.Protocol;

namespace Yupi.Messages.Guides
{
    public class OnGuideSessionInvitedToGuideRoomMessageComposer :
        Contracts.OnGuideSessionInvitedToGuideRoomMessageComposer
    {
        public override void Compose(ISender session, int roomId, string roomName)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendString(roomName);
                session.Send(message);
            }
        }
    }
}
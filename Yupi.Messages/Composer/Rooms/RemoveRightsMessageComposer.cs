using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RemoveRightsMessageComposer : Contracts.RemoveRightsMessageComposer
    {
        public override void Compose(ISender session, uint roomId, uint userId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendInteger(userId);
                session.Send(message);
            }
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomRightsLevelMessageComposer : Contracts.RoomRightsLevelMessageComposer
    {
        // TODO Level should be enum
        public override void Compose(ISender session, int level)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(level);
                session.Send(message);
            }
        }
    }
}
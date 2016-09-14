using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class GroupRoomMessageComposer : Contracts.GroupRoomMessageComposer
    {
        public override void Compose(ISender session, int roomId, int groupId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendInteger(groupId);
                session.Send(message);
            }
        }
    }
}
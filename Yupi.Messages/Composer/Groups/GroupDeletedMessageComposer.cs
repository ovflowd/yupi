using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class GroupDeletedMessageComposer : Contracts.GroupDeletedMessageComposer
    {
        public override void Compose(ISender room, int groupId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(groupId);
                room.Send(message);
            }
        }
    }
}
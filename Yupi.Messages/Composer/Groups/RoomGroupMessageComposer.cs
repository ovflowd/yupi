using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class RoomGroupMessageComposer : Contracts.RoomGroupMessageComposer
    {
        public override void Compose(ISender room, ISet<Group> groups)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(groups.Count);

                foreach (Group current in groups)
                {
                    message.AppendInteger(current.Id);
                    message.AppendString(current.Badge);
                }

                room.Send(message);
            }
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class GiveRespectsMessageComposer : Contracts.GiveRespectsMessageComposer
    {
        public override void Compose(ISender room, int user, int respect)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(user);
                message.AppendInteger(respect);
                room.Send(message);
            }
        }
    }
}
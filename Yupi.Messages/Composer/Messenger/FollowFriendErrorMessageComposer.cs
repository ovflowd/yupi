using Yupi.Protocol;

namespace Yupi.Messages.Messenger
{
    public class FollowFriendErrorMessageComposer : Contracts.FollowFriendErrorMessageComposer
    {
        // TODO Enum
        public override void Compose(ISender session, int status)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(status);
                session.Send(message);
            }
        }
    }
}
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class FindMoreFriendsSuccessMessageComposer : Contracts.FindMoreFriendsSuccessMessageComposer
    {
        public override void Compose(ISender session, bool success)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(success);
                session.Send(message);
            }
        }
    }
}
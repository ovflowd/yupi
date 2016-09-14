using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class GroupAreYouSureMessageComposer : Contracts.GroupAreYouSureMessageComposer
    {
        public override void Compose(ISender session, int userId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(userId);
                message.AppendInteger(0); // TODO Hardcoded
                session.Send(message);
            }
        }
    }
}
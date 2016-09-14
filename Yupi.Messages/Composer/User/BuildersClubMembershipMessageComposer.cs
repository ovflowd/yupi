using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class BuildersClubMembershipMessageComposer : Contracts.BuildersClubMembershipMessageComposer
    {
        public override void Compose(ISender session, int expire, int maxItems)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(expire);
                message.AppendInteger(maxItems);
                message.AppendInteger(2); // TODO Hardcoded
                session.Send(message);
            }
        }
    }
}
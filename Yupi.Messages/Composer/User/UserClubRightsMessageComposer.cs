using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class UserClubRightsMessageComposer : Contracts.UserClubRightsMessageComposer
    {
        public override void Compose(ISender session, bool hasVIP, int rank, bool isAmbadassor = false)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(hasVIP); // TODO Is enum
                message.AppendInteger(rank);
                message.AppendBool(isAmbadassor);
                session.Send(message);
            }
        }
    }
}
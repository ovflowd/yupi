using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class BuildersClubMembershipMessageComposer : Yupi.Messages.Contracts.BuildersClubMembershipMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, int expire, int maxItems)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(expire);
                message.AppendInteger(maxItems);
                message.AppendInteger(2); // TODO Hardcoded
                session.Send(message);
            }
        }
    }
}
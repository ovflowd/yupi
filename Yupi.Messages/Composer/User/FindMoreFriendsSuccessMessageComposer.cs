using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class FindMoreFriendsSuccessMessageComposer : Yupi.Messages.Contracts.FindMoreFriendsSuccessMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, bool success)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(success);
                session.Send(message);
            }
        }
    }
}
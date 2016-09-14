using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
    public class NotAcceptingRequestsMessageComposer : Contracts.NotAcceptingRequestsMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(39);
                message.AppendInteger(3);
                session.Send(message); // TODO Hardcoded
            }
        }
    }
}
using System;
using Yupi.Model.Domain;
using Yupi.Protocol.Buffers;
using Yupi.Messages.Encoders;

namespace Yupi.Messages.Chat
{
    public class ShoutMessageComposer : Yupi.Messages.Contracts.ShoutMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, ChatMessage msg, int count = -1)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.Append(msg, count);
                session.Send(message);
            }
        }
    }
}
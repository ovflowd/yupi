using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class TypingStatusMessageComposer : Yupi.Messages.Contracts.TypingStatusMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, int virtualId, bool isTyping)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(virtualId);
                message.AppendInteger(isTyping);
                session.Send(message);
            }
        }
    }
}
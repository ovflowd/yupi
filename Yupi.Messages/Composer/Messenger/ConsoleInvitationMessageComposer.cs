using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
    public class ConsoleInvitationMessageComposer : Yupi.Messages.Contracts.ConsoleInvitationMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, int habboId, string content)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(habboId);
                message.AppendString(content);
                session.Send(message);
            }
        }
    }
}
using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class ApplyHanditemMessageComposer : Yupi.Messages.Contracts.ApplyHanditemMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, int virtualId, int itemId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(virtualId);
                message.AppendInteger(itemId);
                session.Send(message);
            }
        }
    }
}
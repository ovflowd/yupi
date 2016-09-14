using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
    public class OnGuideSessionDetachedMessageComposer : Yupi.Messages.Contracts.OnGuideSessionDetachedMessageComposer
    {
        // TODO Meaning of value (enum)
        public override void Compose(Yupi.Protocol.ISender session, int value)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(value);
                session.Send(message);
            }
        }
    }
}
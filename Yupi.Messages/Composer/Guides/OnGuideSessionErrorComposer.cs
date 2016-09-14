using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
    public class OnGuideSessionErrorComposer : Yupi.Messages.Contracts.OnGuideSessionErrorComposer
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            // TODO Hardcoded message
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}
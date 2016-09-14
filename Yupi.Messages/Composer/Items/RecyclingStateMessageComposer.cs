using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class RecyclingStateMessageComposer : Yupi.Messages.Contracts.RecyclingStateMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, int insertId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(insertId);
                session.Send(message);
            }
        }
    }
}
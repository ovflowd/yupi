using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class UpdateInventoryMessageComposer : Yupi.Messages.Contracts.UpdateInventoryMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                session.Send(message);
            }
        }
    }
}
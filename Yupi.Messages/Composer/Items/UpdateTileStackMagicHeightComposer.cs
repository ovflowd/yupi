using System;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class UpdateTileStackMagicHeightComposer : Contracts.UpdateTileStackMagicHeightComposer
    {
        public override void Compose(ISender session, int itemId, int z)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(itemId);
                message.AppendInteger(Convert.ToUInt32(z*100));
                session.Send(message);
            }
        }
    }
}
namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Protocol.Buffers;

    public class UpdateTileStackMagicHeightComposer : Yupi.Messages.Contracts.UpdateTileStackMagicHeightComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int itemId, int z)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(itemId);
                message.AppendInteger(Convert.ToUInt32(z*100));
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
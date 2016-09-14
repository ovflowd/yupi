namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RemoveInventoryObjectMessageComposer : Contracts.RemoveInventoryObjectMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int itemId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(itemId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
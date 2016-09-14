namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Protocol.Buffers;

    public class UpdateInventoryMessageComposer : Yupi.Messages.Contracts.UpdateInventoryMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
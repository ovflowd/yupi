namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RemovePetFromInventoryComposer : Yupi.Messages.Contracts.RemovePetFromInventoryComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint petId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(petId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
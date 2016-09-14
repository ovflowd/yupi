namespace Yupi.Messages.Pets
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RespectPetMessageComposer : Yupi.Messages.Contracts.RespectPetMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int entityId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entityId);
                message.AppendBool(true);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
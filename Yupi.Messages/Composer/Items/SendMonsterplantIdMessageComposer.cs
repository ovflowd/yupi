namespace Yupi.Messages.Items
{
    using System;

    using Yupi.Protocol.Buffers;

    public class SendMonsterplantIdMessageComposer : Yupi.Messages.Contracts.SendMonsterplantIdMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint entityId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entityId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
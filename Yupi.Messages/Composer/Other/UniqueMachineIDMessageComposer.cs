namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Protocol.Buffers;

    public class UniqueMachineIDMessageComposer : Yupi.Messages.Contracts.UniqueMachineIDMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string machineId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(machineId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
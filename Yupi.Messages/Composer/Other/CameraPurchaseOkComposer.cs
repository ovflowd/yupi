namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Protocol.Buffers;

    public class CameraPurchaseOkComposer : Yupi.Messages.Contracts.CameraPurchaseOkComposer
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
namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Protocol.Buffers;

    public class InitCryptoMessageComposer : Yupi.Messages.Contracts.InitCryptoMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                // TODO What about public networks?
                message.AppendString("Yupi");
                message.AppendString("Disabled Crypto");
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
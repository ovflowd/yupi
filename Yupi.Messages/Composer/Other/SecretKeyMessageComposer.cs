namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Protocol.Buffers;

    public class SecretKeyMessageComposer : Yupi.Messages.Contracts.SecretKeyMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            // TODO Public networks???

            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString("Crypto disabled");
                message.AppendBool(false);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
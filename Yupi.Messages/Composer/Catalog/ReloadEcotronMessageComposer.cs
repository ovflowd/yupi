namespace Yupi.Messages.Catalog
{
    using System;

    using Yupi.Protocol.Buffers;

    public class ReloadEcotronMessageComposer : Yupi.Messages.Contracts.ReloadEcotronMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            // TODO Hardcoded message
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
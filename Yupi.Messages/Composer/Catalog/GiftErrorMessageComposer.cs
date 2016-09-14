namespace Yupi.Messages.Catalog
{
    using System;

    using Yupi.Protocol.Buffers;

    public class GiftErrorMessageComposer : Contracts.GiftErrorMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string username)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(username);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
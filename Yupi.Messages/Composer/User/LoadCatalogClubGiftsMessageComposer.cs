namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class LoadCatalogClubGiftsMessageComposer : Yupi.Messages.Contracts.LoadCatalogClubGiftsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0); // i
                message.AppendInteger(0); // i2
                message.AppendInteger(1); // TODO Magic constants
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
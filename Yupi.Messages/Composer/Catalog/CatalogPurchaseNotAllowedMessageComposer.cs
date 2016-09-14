namespace Yupi.Messages.Catalog
{
    using System;

    using Yupi.Protocol.Buffers;

    public class CatalogPurchaseNotAllowedMessageComposer : Yupi.Messages.Contracts.CatalogPurchaseNotAllowedMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool isForbidden)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(isForbidden);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}
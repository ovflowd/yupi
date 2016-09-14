namespace Yupi.Messages.Catalog
{
    using System;

    public class CatalogueOfferConfigMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<CatalogueOfferConfigMessageComposer>().Compose(session);
        }

        #endregion Methods
    }
}
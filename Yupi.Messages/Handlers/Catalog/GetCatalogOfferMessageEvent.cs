namespace Yupi.Messages.Catalog
{
    using System;

    public class GetCatalogOfferMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            uint num = message.GetUInt32();
            throw new NotImplementedException();
            /*
            CatalogItem catalogItem = Yupi.GetGame().GetCatalogManager().GetItemFromOffer(num);

            if (catalogItem == null || CatalogManager.LastSentOffer == num)
                return;

            CatalogManager.LastSentOffer = num;

            router.GetComposer<CatalogOfferMessageComposer> ().Compose (session, catalogItem);*/
        }

        #endregion Methods
    }
}
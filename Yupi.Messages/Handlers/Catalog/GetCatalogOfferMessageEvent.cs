using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
    public class GetCatalogOfferMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var num = message.GetUInt32();
            throw new NotImplementedException();
            /*
            CatalogItem catalogItem = Yupi.GetGame().GetCatalogManager().GetItemFromOffer(num);

            if (catalogItem == null || CatalogManager.LastSentOffer == num)
                return;

            CatalogManager.LastSentOffer = num;

            router.GetComposer<CatalogOfferMessageComposer> ().Compose (session, catalogItem);*/
        }
    }
}
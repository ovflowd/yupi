using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
    public class PurchaseFromCatalogMessageEvent : AbstractHandler
    {
        private readonly CatalogController CatalogController;

        public PurchaseFromCatalogMessageEvent()
        {
            CatalogController = DependencyFactory.Resolve<CatalogController>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            // TODO Maximum items

            var pageId = message.GetInteger();
            var itemId = message.GetInteger();
            var extraData = message.GetString();
            var amount = message.GetInteger();

            var item = CatalogController.GetById(pageId, itemId);
            CatalogController.Purchase(session, item, extraData, amount);
        }
    }
}
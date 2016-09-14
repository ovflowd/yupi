namespace Yupi.Messages.Catalog
{
    using System;

    using Yupi.Controller;
    using Yupi.Messages.Notification;
    using Yupi.Model;
    using Yupi.Model.Domain;

    public class PurchaseFromCatalogMessageEvent : AbstractHandler
    {
        #region Fields

        private CatalogController CatalogController;

        #endregion Fields

        #region Constructors

        public PurchaseFromCatalogMessageEvent()
        {
            CatalogController = DependencyFactory.Resolve<CatalogController>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            // TODO Maximum items

            int pageId = message.GetInteger();
            int itemId = message.GetInteger();
            string extraData = message.GetString();
            int amount = message.GetInteger();

            CatalogItem item = CatalogController.GetById(pageId, itemId);
            CatalogController.Purchase(session, item, extraData, amount);
        }

        #endregion Methods
    }
}
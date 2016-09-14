namespace Yupi.Messages.Catalog
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class PurchaseFromCatalogAsGiftMessageEvent : AbstractHandler
    {
        #region Fields

        private CatalogController CatalogController;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public PurchaseFromCatalogAsGiftMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            CatalogController = DependencyFactory.Resolve<CatalogController>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            int pageId = message.GetInteger();
            int itemId = message.GetInteger();
            string extraData = message.GetString();
            string giftUser = message.GetString();
            string giftMessage = message.GetString();
            int giftSpriteId = message.GetInteger();
            int giftLazo = message.GetInteger();
            int giftColor = message.GetInteger();
            bool showSender = message.GetBool();

            UserInfo info = UserRepository.FindBy(x => x.Name == giftUser);

            if (info == null)
            {
                router.GetComposer<GiftErrorMessageComposer>().Compose(session, giftUser);
            }

            CatalogItem item = CatalogController.GetById(pageId, itemId);
            CatalogController.PurchaseGift(session, item, extraData, info);
        }

        #endregion Methods
    }
}
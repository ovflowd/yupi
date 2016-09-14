using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Controller;

namespace Yupi.Messages.Catalog
{
    public class PurchaseFromCatalogAsGiftMessageEvent : AbstractHandler
    {
        private IRepository<UserInfo> UserRepository;
        private CatalogController CatalogController;

        public PurchaseFromCatalogAsGiftMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            CatalogController = DependencyFactory.Resolve<CatalogController>();
        }

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
    }
}
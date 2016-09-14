using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
    public class PurchaseFromCatalogAsGiftMessageEvent : AbstractHandler
    {
        private readonly CatalogController CatalogController;
        private readonly IRepository<UserInfo> UserRepository;

        public PurchaseFromCatalogAsGiftMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            CatalogController = DependencyFactory.Resolve<CatalogController>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var pageId = message.GetInteger();
            var itemId = message.GetInteger();
            var extraData = message.GetString();
            var giftUser = message.GetString();
            var giftMessage = message.GetString();
            var giftSpriteId = message.GetInteger();
            var giftLazo = message.GetInteger();
            var giftColor = message.GetInteger();
            var showSender = message.GetBool();

            var info = UserRepository.FindBy(x => x.Name == giftUser);

            if (info == null) router.GetComposer<GiftErrorMessageComposer>().Compose(session, giftUser);

            var item = CatalogController.GetById(pageId, itemId);
            CatalogController.PurchaseGift(session, item, extraData, info);
        }
    }
}
using System;
using System.Collections.Generic;
using Yupi.Messages.Contracts;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;

namespace Yupi.Controller
{
    public class CatalogController
    {
        private readonly AchievementManager AchievementManager;
        private readonly IRepository<CatalogItem> ItemRepository;
        private readonly IRepository<UserInfo> UserRepository;

        public CatalogController()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ItemRepository = DependencyFactory.Resolve<IRepository<CatalogItem>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        public CatalogItem GetById(int pageId, int itemId)
        {
            return ItemRepository.FindBy(x => (x.Id == itemId) && (x.PageId.Id == pageId));
        }

        public void PurchaseGift(Habbo user, CatalogItem catalogItem, string extraData, UserInfo receiver)
        {
            if (!catalogItem.AllowGift) return;

            if (Purchase(user, catalogItem, extraData, 1) && (user.Info != receiver))
                AchievementManager
                    .ProgressUserAchievement(user, "ACH_GiftGiver", 1);

            throw new NotImplementedException();

            /*
             * 
             *  Yupi.GetGame()
                                .GetAchievementManager()
                                .ProgressUserAchievement(clientByUserId, "ACH_GiftReceiver", 1, true);
            */
        }

        // TODO Make extraData optional
        public bool Purchase(Habbo user, CatalogItem catalogItem, string extraData, int amount)
        {
            if (catalogItem == null) return false;

            if (!catalogItem.CanPurchase(user.Info.Wallet, amount))
            {
                // TODO Should this really only be sent when the item is Limited? (no error on other items?)
                if (catalogItem is LimitedCatalogItem)
                    user.Router.GetComposer<CatalogLimitedItemSoldOutMessageComposer>().Compose(user);
                return false;
            }

            // TODO Move to CanPurchase
            if (catalogItem.ClubOnly && !user.Info.Subscription.IsValid())
            {
                user.Router.GetComposer<CatalogPurchaseNotAllowedMessageComposer>().Compose(user, true);
                return false;
            }

            catalogItem.Purchase(user.Info.Wallet, amount);

            ItemRepository.Save(catalogItem);

            user.Router.GetComposer<CreditsBalanceMessageComposer>().Compose(user, user.Info.Wallet.Credits);
            user.Router.GetComposer<ActivityPointsMessageComposer>().Compose(user, user.Info.Wallet);

            var items = new Dictionary<BaseItem, List<Item>>();

            foreach (var baseItem in catalogItem.BaseItems.Keys)
            {
                var item = baseItem.CreateNew();
                item.Owner = user.Info;
                item.TryParseExtraData(extraData);

                if (!items.ContainsKey(baseItem)) items.Add(baseItem, new List<Item>());

                items[baseItem].Add(item);

                user.Info.Inventory.Add(item);
            }

            user.Router.GetComposer<UpdateInventoryMessageComposer>().Compose(user);
            user.Router.GetComposer<PurchaseOkComposer>().Compose(user, catalogItem, catalogItem.BaseItems);

            // TODO Can this be solved better?
            foreach (var item in items)
                user.Router.GetComposer<NewInventoryObjectMessageComposer>().Compose(user, item.Key, item.Value);

            if (catalogItem.Badge.Length > 0)
            {
                user.Info.Badges.GiveBadge(catalogItem.Badge);
                UserRepository.Save(user.Info);
            }

            return true;
        }
    }
}
// ---------------------------------------------------------------------------------
// <copyright file="CatalogController.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class CatalogController
    {
        #region Fields

        private AchievementManager AchievementManager;
        private IRepository<CatalogOffer> ItemRepository;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public CatalogController()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ItemRepository = DependencyFactory.Resolve<IRepository<CatalogOffer>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        #endregion Constructors

        #region Methods

        public CatalogOffer GetById(int pageId, int itemId)
        {
            return ItemRepository.FindBy(x => x.Id == itemId && x.PageId.Id == pageId);
        }

        // TODO Make extraData optional
        public bool Purchase(Habbo user, CatalogOffer catalogItem, string extraData, int amount)
        {
            if (catalogItem == null)
            {
                return false;
            }

            if (!catalogItem.CanPurchase(user.Info.Wallet, amount))
            {
                // TODO Should this really only be sent when the item is Limited? (no error on other items?)
                if (catalogItem is LimitedCatalogItem)
                {
                    user.Router.GetComposer<CatalogLimitedItemSoldOutMessageComposer>().Compose(user);
                }
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

            foreach (BaseItem baseItem in catalogItem.BaseItems.Keys)
            {
                Item item = baseItem.CreateNew();
                item.Owner = user.Info;
                item.TryParseExtraData(extraData);

                if (!items.ContainsKey(baseItem))
                {
                    items.Add(baseItem, new List<Item>());
                }

                items[baseItem].Add(item);

                user.Info.Inventory.Add(item);
            }

            user.Router.GetComposer<UpdateInventoryMessageComposer>().Compose(user);
            user.Router.GetComposer<PurchaseOkComposer>().Compose(user, catalogItem, catalogItem.BaseItems);

            // TODO Can this be solved better?
            foreach (var item in items)
            {
                user.Router.GetComposer<NewInventoryObjectMessageComposer>().Compose(user, item.Key, item.Value);
            }

            if (catalogItem.Badge.Length > 0)
            {
                user.Info.Badges.GiveBadge(catalogItem.Badge);
                UserRepository.Save(user.Info);
            }

            return true;
        }

        public void PurchaseGift(Habbo user, CatalogOffer catalogItem, string extraData, UserInfo receiver)
        {
            if (!catalogItem.AllowGift)
            {
                return;
            }

            if (Purchase(user, catalogItem, extraData, 1) && user.Info != receiver)
            {
                AchievementManager
                    .ProgressUserAchievement(user, "ACH_GiftGiver", 1);
            }

            throw new NotImplementedException();

            /*
             *
             *  Yupi.GetGame()
                                .GetAchievementManager()
                                .ProgressUserAchievement(clientByUserId, "ACH_GiftReceiver", 1, true);
            */
        }

        #endregion Methods
    }
}
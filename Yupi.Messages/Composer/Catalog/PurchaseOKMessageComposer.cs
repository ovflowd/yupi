using System.Collections.Generic;
using Yupi.Messages.Contracts;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Catalog
{
    public class PurchaseOKMessageComposer : PurchaseOkComposer
    {
        public override void Compose(ISender session, CatalogItem itemCatalog, IDictionary<BaseItem, int> items,
            int clubLevel = 1)
        {
            var isLimited = itemCatalog is LimitedCatalogItem;
            var limitedStack = 0;
            var limitedSold = 0;

            if (isLimited)
            {
                limitedStack = ((LimitedCatalogItem) itemCatalog).LimitedStack;
                limitedSold = ((LimitedCatalogItem) itemCatalog).LimitedSold;
            }

            Compose(session, itemCatalog.Id, itemCatalog.Name, itemCatalog.CreditsCost, items, clubLevel,
                itemCatalog.DiamondsCost,
                itemCatalog.DucketsCost, isLimited, limitedStack, limitedSold);
        }

        // TODO Refactor this !!!
        private void Compose(ISender session, int itemId, string itemName, int creditsCost,
            IDictionary<BaseItem, int> items = null, int clubLevel = 1,
            int diamondsCost = 0,
            int activityPointsCost = 0, bool isLimited = false,
            int limitedStack = 0,
            int limitedSelled = 0)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(itemId);
                message.AppendString(itemName);
                message.AppendBool(false); // TODO Hardcoded
                message.AppendInteger(creditsCost);
                message.AppendInteger(diamondsCost);
                message.AppendInteger(activityPointsCost);
                message.AppendBool(true);
                message.AppendInteger(items?.Count ?? 0);

                if (items != null)
                    foreach (var itemDic in items)
                    {
                        var item = itemDic.Key;
                        message.AppendString(item.Type.ToString());

                        // TODO Is this right?!
                        if (item.Type == ItemType.Badge)
                        {
                            message.AppendString(item.PublicName);
                            continue;
                        }

                        message.AppendInteger(item.SpriteId);
                        message.AppendString(item.PublicName);
                        message.AppendInteger(itemDic.Value); //productCount
                        message.AppendBool(isLimited);

                        if (!isLimited)
                            continue;

                        message.AppendInteger(limitedStack);
                        message.AppendInteger(limitedSelled);
                    }

                message.AppendInteger(clubLevel);
                message.AppendBool(false); //window.visible?

                session.Send(message);
            }
        }
    }
}
// ---------------------------------------------------------------------------------
// <copyright file="PurchaseOKMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Catalog
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class PurchaseOKMessageComposer : Yupi.Messages.Contracts.PurchaseOkComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, CatalogOffer itemCatalog,
            IDictionary<BaseItem, int> items,
            int clubLevel = 1)
        {
            bool isLimited = itemCatalog is LimitedCatalogItem;
            int limitedStack = 0;
            int limitedSold = 0;

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
        private void Compose(Yupi.Protocol.ISender session, int itemId, string itemName, int creditsCost,
            IDictionary<BaseItem, int> items = null, int clubLevel = 1,
            int diamondsCost = 0,
            int activityPointsCost = 0, bool isLimited = false,
            int limitedStack = 0,
            int limitedSelled = 0)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
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
                {
                    foreach (KeyValuePair<BaseItem, int> itemDic in items)
                    {
                        BaseItem item = itemDic.Key;
                        message.AppendString(item.Type.ToString());

                        // TODO Is this right?!
                        if (item.Type == Yupi.Model.ItemType.Badge)
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
                }

                message.AppendInteger(clubLevel);
                message.AppendBool(false); //window.visible?

                session.Send(message);
            }
        }

        #endregion Methods
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.Catalogs.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Catalogs.Composers
{
    /// <summary>
    ///     Class CatalogPacket.
    /// </summary>
     public static class CatalogPageComposer
    {
        /// <summary>
        ///     Composes the index.
        /// </summary>
        /// <param name="rank">The rank.</param>
        /// <param name="type">The type.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public static SimpleServerMessageBuffer ComposeIndex(uint rank, string type)
        {
            IEnumerable<CatalogPage> pages =
                Yupi.GetGame().GetCatalogManager().Categories.Values.OfType<CatalogPage>().ToList();

            IOrderedEnumerable<CatalogPage> sortedPages = pages.Where(x => x.ParentId == -2 && x.MinRank <= rank).OrderBy(x => x.OrderNum);

            if (type == "NORMAL")
                sortedPages = pages.Where(x => x.ParentId == -1 && x.MinRank <= rank).OrderBy(x => x.OrderNum);

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("CatalogueIndexMessageComposer"));

            messageBuffer.AppendBool(true);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(-1);
            messageBuffer.AppendString("root");
            messageBuffer.AppendString(string.Empty);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(sortedPages.Count());

            foreach (CatalogPage cat in sortedPages)
            {
                messageBuffer.AppendBool(cat.Visible);
                messageBuffer.AppendInteger(cat.IconImage);
                messageBuffer.AppendInteger(cat.PageId);
                messageBuffer.AppendString(cat.CodeName);
                messageBuffer.AppendString(cat.Caption);
                messageBuffer.AppendInteger(cat.FlatOffers.Count);

                foreach (uint i in cat.FlatOffers.Keys)
                    messageBuffer.AppendInteger(i);

                IOrderedEnumerable<CatalogPage> sortedSubPages =
                    pages.Where(x => x.ParentId == cat.PageId && x.MinRank <= rank).OrderBy(x => x.OrderNum);

                messageBuffer.AppendInteger(sortedSubPages.Count());

                foreach (CatalogPage subCat in sortedSubPages)
                {
                    messageBuffer.AppendBool(subCat.Visible);
                    messageBuffer.AppendInteger(subCat.IconImage);
                    messageBuffer.AppendInteger(subCat.PageId);
                    messageBuffer.AppendString(subCat.CodeName);
                    messageBuffer.AppendString(subCat.Caption);
                    messageBuffer.AppendInteger(subCat.FlatOffers.Count);

                    foreach (uint i2 in subCat.FlatOffers.Keys)
                        messageBuffer.AppendInteger(i2);

                    messageBuffer.AppendInteger(0);
                }
            }

            messageBuffer.AppendBool(false);
            messageBuffer.AppendString(type);

            return messageBuffer;
        }
			
        /// <summary>
        ///     Composes the club purchase page.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="windowId">The window identifier.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public static SimpleServerMessageBuffer ComposeClubPurchasePage(GameClient session, int windowId)
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("CatalogueClubPageMessageComposer"));
            List<CatalogItem> habboClubItems = Yupi.GetGame().GetCatalogManager().HabboClubItems;

            messageBuffer.AppendInteger(habboClubItems.Count);

            foreach (CatalogItem item in habboClubItems)
            {
                messageBuffer.AppendInteger(item.Id);
                messageBuffer.AppendString(item.Name);
                messageBuffer.AppendBool(false);
                messageBuffer.AppendInteger(item.CreditsCost);

                if (item.DiamondsCost > 0)
                {
                    messageBuffer.AppendInteger(item.DiamondsCost);
                    messageBuffer.AppendInteger(105);
                }
                else
                {
                    messageBuffer.AppendInteger(item.DucketsCost);
                    messageBuffer.AppendInteger(0);
                }

                messageBuffer.AppendBool(true);
                string[] fuckingArray = item.Name.Split('_');
                double dayTime = 31;

                if (item.Name.Contains("DAY"))
                    dayTime = int.Parse(fuckingArray[3]);
                else if (item.Name.Contains("MONTH"))
                {
                    int monthTime = int.Parse(fuckingArray[3]);
                    dayTime = monthTime*31;
                }
                else if (item.Name.Contains("YEAR"))
                {
                    int yearTimeOmg = int.Parse(fuckingArray[3]);
                    dayTime = yearTimeOmg*31*12;
                }

                DateTime newExpiryDate = DateTime.Now.AddDays(dayTime);

                if (session.GetHabbo().GetSubscriptionManager().HasSubscription)
                    newExpiryDate =
                        Yupi.UnixToDateTime(session.GetHabbo().GetSubscriptionManager().GetSubscription().ExpireTime)
                            .AddDays(dayTime);

                messageBuffer.AppendInteger((int) dayTime/31);
                messageBuffer.AppendInteger((int) dayTime);
                messageBuffer.AppendBool(false);
                messageBuffer.AppendInteger((int) dayTime);
                messageBuffer.AppendInteger(newExpiryDate.Year);
                messageBuffer.AppendInteger(newExpiryDate.Month);
                messageBuffer.AppendInteger(newExpiryDate.Day);
            }

            messageBuffer.AppendInteger(windowId);
            return messageBuffer;
        }

     public static SimpleServerMessageBuffer PurchaseOk(CatalogItem itemCatalog, Dictionary<Item, uint> items,
            int clubLevel = 1)
        {
            return PurchaseOk(itemCatalog.Id, itemCatalog.Name, itemCatalog.CreditsCost, items, clubLevel,
                itemCatalog.DiamondsCost,
                itemCatalog.DucketsCost, itemCatalog.IsLimited, itemCatalog.LimitedStack, itemCatalog.LimitedSelled);
        }

        /// <summary>
        ///     Purchases the ok.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public static SimpleServerMessageBuffer PurchaseOk(uint itemId, string itemName, uint creditsCost,
            Dictionary<Item, uint> items = null, int clubLevel = 1,
            uint diamondsCost = 0,
            uint activityPointsCost = 0, bool isLimited = false,
            uint limitedStack = 0,
            uint limitedSelled = 0)
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("PurchaseOKMessageComposer"));
            messageBuffer.AppendInteger(itemId);
            messageBuffer.AppendString(itemName);
            messageBuffer.AppendBool(false);
            messageBuffer.AppendInteger(creditsCost);
            messageBuffer.AppendInteger(diamondsCost);
            messageBuffer.AppendInteger(activityPointsCost);
            messageBuffer.AppendBool(true);
            messageBuffer.AppendInteger(items?.Count ?? 0);

            if (items != null)
            {
                foreach (KeyValuePair<Item, uint> itemDic in items)
                {
                    Item item = itemDic.Key;
                    messageBuffer.AppendString(item.Type.ToString());

                    if (item.Type == 'b')
                    {
                        messageBuffer.AppendString(item.PublicName);
                        continue;
                    }

                    messageBuffer.AppendInteger(item.SpriteId);
                    messageBuffer.AppendString(item.PublicName);
                    messageBuffer.AppendInteger(itemDic.Value); //productCount
                    messageBuffer.AppendBool(isLimited);

                    if (!isLimited)
                        continue;

                    messageBuffer.AppendInteger(limitedStack);
                    messageBuffer.AppendInteger(limitedSelled);
                }
            }

            messageBuffer.AppendInteger(clubLevel); //clubLevel
            messageBuffer.AppendBool(false); //window.visible?

            return messageBuffer;
        }

        /// <summary>
        ///     Composes the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="message">The messageBuffer.</param>
     public static void ComposeItem(CatalogItem item, SimpleServerMessageBuffer messageBuffer)
        {
            messageBuffer.AppendInteger(item.Id);

            string displayName = item.Name;

            if (PetTypeManager.ItemIsPet(item.Name))
                displayName = PetTypeManager.GetHabboPetType(item.Name);

            messageBuffer.AppendString(displayName);
            messageBuffer.AppendBool(false);
            messageBuffer.AppendInteger(item.CreditsCost);

            if (item.DiamondsCost > 0)
            {
                messageBuffer.AppendInteger(item.DiamondsCost);
                messageBuffer.AppendInteger(105);
            }
            else
            {
                messageBuffer.AppendInteger(item.DucketsCost);
                messageBuffer.AppendInteger(0);
            }
            messageBuffer.AppendBool(item.GetFirstBaseItem().AllowGift);

            switch (item.Name)
            {
                case "g0 group_product":
                    messageBuffer.AppendInteger(0);
                    break;

                case "room_ad_plus_badge":
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString("b");
                    messageBuffer.AppendString("RADZZ");
                    break;

                default:
                    if (item.Name.StartsWith("builders_club_addon_") || item.Name.StartsWith("builders_club_time_"))
                        messageBuffer.AppendInteger(0);
                    else if (item.Badge == "")
                        messageBuffer.AppendInteger(item.Items.Count);
                    else
                    {
                        messageBuffer.AppendInteger(item.Items.Count + 1);
                        messageBuffer.AppendString("b");
                        messageBuffer.AppendString(item.Badge);
                    }
                    break;
            }
            foreach (Item baseItem in item.Items.Keys)
            {
                if (item.Name == "g0 group_product" || item.Name.StartsWith("builders_club_addon_") ||
                    item.Name.StartsWith("builders_club_time_"))
                    break;
                if (item.Name == "room_ad_plus_badge")
                {
                    messageBuffer.AppendString("");
                    messageBuffer.AppendInteger(0);
                }
                else
                {
                    messageBuffer.AppendString(baseItem.Type.ToString());
                    messageBuffer.AppendInteger(baseItem.SpriteId);

                    if (item.Name.Contains("wallpaper_single") || item.Name.Contains("floor_single") ||
                        item.Name.Contains("landscape_single"))
                    {
                        string[] array = item.Name.Split('_');
                        messageBuffer.AppendString(array[2]);
                    }
                    else if (item.Name.StartsWith("bot_") || baseItem.InteractionType == Interaction.MusicDisc ||
                             item.GetFirstBaseItem().Name == "poster")
                        messageBuffer.AppendString(item.ExtraData);
                    else if (item.Name.StartsWith("poster_"))
                    {
                        string[] array2 = item.Name.Split('_');
                        messageBuffer.AppendString(array2[1]);
                    }
                    else if (item.Name.StartsWith("poster "))
                    {
                        string[] array3 = item.Name.Split(' ');
                        messageBuffer.AppendString(array3[1]);
                    }
                    else if (item.SongId > 0u && baseItem.InteractionType == Interaction.MusicDisc)
                        messageBuffer.AppendString(item.ExtraData);
                    else
                        messageBuffer.AppendString(string.Empty);

                    messageBuffer.AppendInteger(item.Items[baseItem]);
                    messageBuffer.AppendBool(item.IsLimited);
                    if (!item.IsLimited)
                        continue;
                    messageBuffer.AppendInteger(item.LimitedStack);
                    messageBuffer.AppendInteger(item.LimitedStack - item.LimitedSelled);
                }
            }
            messageBuffer.AppendInteger(item.ClubOnly ? 1 : 0);

            if (item.IsLimited || item.FirstAmount != 1)
            {
                messageBuffer.AppendBool(false);
                return;
            }

            messageBuffer.AppendBool(item.HaveOffer && !item.IsLimited);
        }
    }
}
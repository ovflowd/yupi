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
    internal static class CatalogPageComposer
    {
        /// <summary>
        ///     Composes the index.
        /// </summary>
        /// <param name="rank">The rank.</param>
        /// <param name="type">The type.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal static SimpleServerMessageBuffer ComposeIndex(uint rank, string type)
        {
            IEnumerable<CatalogPage> pages =
                Yupi.GetGame().GetCatalogManager().Categories.Values.OfType<CatalogPage>().ToList();

            IOrderedEnumerable<CatalogPage> sortedPages = pages.Where(x => x.ParentId == -2 && x.MinRank <= rank).OrderBy(x => x.OrderNum);

            if (type == "NORMAL")
                sortedPages = pages.Where(x => x.ParentId == -1 && x.MinRank <= rank).OrderBy(x => x.OrderNum);

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("CatalogueIndexMessageComposer"));

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
        ///     Composes the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal static SimpleServerMessageBuffer ComposePage(CatalogPage page)
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("CataloguePageMessageComposer"));
            messageBuffer.AppendInteger(page.PageId);

            switch (page.Layout)
            {
                case "frontpage":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("frontpage4");
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.Text2);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(-1);
                    messageBuffer.AppendBool(false);
                    break;

                case "roomads":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("roomads");
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString("events_header");
                    messageBuffer.AppendString("");
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString("");
                    break;

                case "builders_club_frontpage_normal":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("builders_club_frontpage");
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendInteger(8554);
                    messageBuffer.AppendString("builders_club_1_month");
                    messageBuffer.AppendString("");
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(1024);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendBool(false);
                    messageBuffer.AppendInteger(8606);
                    messageBuffer.AppendString("builders_club_14_days");
                    messageBuffer.AppendString("");
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(1024);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendBool(false);
                    messageBuffer.AppendInteger(8710);
                    messageBuffer.AppendString("builders_club_31_days");
                    messageBuffer.AppendString("");
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(1024);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendBool(false);
                    break;

                case "bots":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("bots");
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.Text2);
                    messageBuffer.AppendString(page.TextDetails);
                    break;

                case "badge_display":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("badge_display");
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.Text2);
                    messageBuffer.AppendString(page.TextDetails);
                    break;

                case "info_loyalty":
                case "info_duckets":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString(page.Layout);
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.Text1);
                    break;

                case "sold_ltd_items":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("sold_ltd_items");
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.Text2);
                    messageBuffer.AppendString(page.TextDetails);
                    break;

                case "recycler_info":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString(page.Layout);
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.Text2);
                    messageBuffer.AppendString(page.TextDetails);
                    break;

                case "recycler_prizes":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("recycler_prizes");
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString("catalog_recycler_headline3");
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.Text1);
                    break;

                case "spaces_new":
                case "spaces":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("spaces_new");
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.Text1);
                    break;

                case "recycler":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("recycler");
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.Text1);
                    break;

                case "trophies":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("trophies");
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.TextDetails);
                    break;

                case "pets":
                case "pets2":
                case "pets3":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString(page.Layout);
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendInteger(4);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.Text2);
                    messageBuffer.AppendString(page.TextDetails);
                    messageBuffer.AppendString(page.TextTeaser);
                    break;

                case "soundmachine":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString(page.Layout);
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.TextDetails);
                    break;

                case "vip_buy":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString(page.Layout);
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendInteger(0);
                    break;

                case "guild_custom_furni":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("guild_custom_furni");
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString("");
                    messageBuffer.AppendString("");
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.TextDetails);
                    messageBuffer.AppendString(page.Text2);
                    break;

                case "guild_frontpage":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("guild_frontpage");
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.TextDetails);
                    messageBuffer.AppendString(page.Text2);
                    break;

                case "guild_forum":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("guild_forum");
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(2);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.Text2);
                    break;

                case "club_gifts":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("club_gifts");
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.Text1);
                    break;

                case "default_3x3":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString(page.Layout);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendString(page.LayoutSpecial);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.TextDetails);
                    messageBuffer.AppendString(page.TextTeaser);
                    break;

                default:
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString(page.Layout);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendString(page.LayoutSpecial);
                    messageBuffer.AppendInteger(4);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.Text2);
                    messageBuffer.AppendString(page.TextTeaser);
                    messageBuffer.AppendString(page.TextDetails);
                    break;

                case "builders_3x3":
                    messageBuffer.AppendString("BUILDERS_CLUB");
                    messageBuffer.AppendString("default_3x3");
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendString(page.LayoutSpecial);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.TextDetails.Replace("[10]", Convert.ToChar(10).ToString())
                        .Replace("[13]", Convert.ToChar(13).ToString()));
                    messageBuffer.AppendString(page.TextTeaser.Replace("[10]", Convert.ToChar(10).ToString())
                        .Replace("[13]", Convert.ToChar(13).ToString()));
                    break;

                case "builders_3x3_color":
                    messageBuffer.AppendString("BUILDERS_CLUB");
                    messageBuffer.AppendString("default_3x3_color_grouping");
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendString(page.LayoutTeaser);
                    messageBuffer.AppendString(page.LayoutSpecial);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendString(page.Text1);
                    messageBuffer.AppendString(page.TextDetails.Replace("[10]", Convert.ToChar(10).ToString())
                        .Replace("[13]", Convert.ToChar(13).ToString()));
                    messageBuffer.AppendString(page.TextTeaser.Replace("[10]", Convert.ToChar(10).ToString())
                        .Replace("[13]", Convert.ToChar(13).ToString()));
                    break;

                case "builders_club_frontpage":
                    messageBuffer.AppendString("BUILDERS_CLUB");
                    messageBuffer.AppendString("builders_club_frontpage");
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    messageBuffer.AppendInteger(3);
                    messageBuffer.AppendInteger(8554);
                    messageBuffer.AppendString("builders_club_1_month");
                    messageBuffer.AppendString("");
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(1024);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendBool(false);
                    messageBuffer.AppendInteger(8606);
                    messageBuffer.AppendString("builders_club_14_days");
                    messageBuffer.AppendString("");
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(1024);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendBool(false);
                    messageBuffer.AppendInteger(8710);
                    messageBuffer.AppendString("builders_club_31_days");
                    messageBuffer.AppendString("");
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(2560000);
                    messageBuffer.AppendInteger(1024);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendBool(false);
                    break;

                case "builders_club_addons":
                    messageBuffer.AppendString("BUILDERS_CLUB");
                    messageBuffer.AppendString("builders_club_addons");
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    break;

                case "builders_club_addons_normal":
                    messageBuffer.AppendString("NORMAL");
                    messageBuffer.AppendString("builders_club_addons");
                    messageBuffer.AppendInteger(0);
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendString(page.LayoutHeadline);
                    break;
            }

            if (page.Layout.StartsWith("frontpage") || page.Layout == "vip_buy" || page.Layout == "recycler")
            {
                messageBuffer.AppendInteger(0);
            }
            else
            {
                messageBuffer.AppendInteger(page.Items.Count);

                foreach (CatalogItem item in page.Items.Values)
                    ComposeItem(item, messageBuffer);
            }

            messageBuffer.AppendInteger(-1);
            messageBuffer.AppendBool(false);

            return messageBuffer;
        }

        /// <summary>
        ///     Composes the club purchase page.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="windowId">The window identifier.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal static SimpleServerMessageBuffer ComposeClubPurchasePage(GameClient session, int windowId)
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("CatalogueClubPageMessageComposer"));
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

        internal static SimpleServerMessageBuffer PurchaseOk(CatalogItem itemCatalog, Dictionary<Item, uint> items,
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
        internal static SimpleServerMessageBuffer PurchaseOk(uint itemId, string itemName, uint creditsCost,
            Dictionary<Item, uint> items = null, int clubLevel = 1,
            uint diamondsCost = 0,
            uint activityPointsCost = 0, bool isLimited = false,
            uint limitedStack = 0,
            uint limitedSelled = 0)
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("PurchaseOKMessageComposer"));
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
        internal static void ComposeItem(CatalogItem item, SimpleServerMessageBuffer messageBuffer)
        {
            messageBuffer.AppendInteger(item.Id);

            string displayName = item.Name;

            if (PetTypeManager.ItemIsPet(item.Name))
                displayName = PetTypeManager.GetHabboPetType(item.Name);

            messageBuffer.AppendString(displayName, true);
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
using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Catalogs.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pets;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Catalogs.Composers
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
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage ComposeIndex(uint rank, string type)
        {
            IEnumerable<CatalogPage> pages =
                Yupi.GetGame().GetCatalog().Categories.Values.OfType<CatalogPage>().ToList();

            IOrderedEnumerable<CatalogPage> sortedPages = pages.Where(x => x.ParentId == -2 && x.MinRank <= rank).OrderBy(x => x.OrderNum);

            if (type == "NORMAL")
                sortedPages = pages.Where(x => x.ParentId == -1 && x.MinRank <= rank).OrderBy(x => x.OrderNum);

            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("CatalogueIndexMessageComposer"));

            message.AppendBool(true);
            message.AppendInteger(0);
            message.AppendInteger(-1);
            message.AppendString("root");
            message.AppendString(string.Empty);
            message.AppendInteger(0);
            message.AppendInteger(sortedPages.Count());

            foreach (CatalogPage cat in sortedPages)
            {
                message.AppendBool(cat.Visible);
                message.AppendInteger(cat.IconImage);
                message.AppendInteger(cat.PageId);
                message.AppendString(cat.CodeName);
                message.AppendString(cat.Caption);
                message.AppendInteger(cat.FlatOffers.Count);

                foreach (uint i in cat.FlatOffers.Keys)
                    message.AppendInteger(i);

                IOrderedEnumerable<CatalogPage> sortedSubPages =
                    pages.Where(x => x.ParentId == cat.PageId && x.MinRank <= rank).OrderBy(x => x.OrderNum);

                message.AppendInteger(sortedSubPages.Count());

                foreach (CatalogPage subCat in sortedSubPages)
                {
                    message.AppendBool(subCat.Visible);
                    message.AppendInteger(subCat.IconImage);
                    message.AppendInteger(subCat.PageId);
                    message.AppendString(subCat.CodeName);
                    message.AppendString(subCat.Caption);
                    message.AppendInteger(subCat.FlatOffers.Count);

                    foreach (uint i2 in subCat.FlatOffers.Keys)
                        message.AppendInteger(i2);

                    message.AppendInteger(0);
                }
            }

            message.AppendBool(false);
            message.AppendString(type);

            return message;
        }

        /// <summary>
        ///     Composes the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage ComposePage(CatalogPage page)
        {
            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("CataloguePageMessageComposer"));
            message.AppendInteger(page.PageId);

            switch (page.Layout)
            {
                case "frontpage":
                    message.AppendString("NORMAL");
                    message.AppendString("frontpage4");
                    message.AppendInteger(2);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendInteger(2);
                    message.AppendString(page.Text1);
                    message.AppendString(page.Text2);
                    message.AppendInteger(0);
                    message.AppendInteger(-1);
                    message.AppendBool(false);
                    break;

                case "roomads":
                    message.AppendString("NORMAL");
                    message.AppendString("roomads");
                    message.AppendInteger(2);
                    message.AppendString("events_header");
                    message.AppendString("");
                    message.AppendInteger(2);
                    message.AppendString(page.Text1);
                    message.AppendString("");
                    break;

                case "builders_club_frontpage_normal":
                    message.AppendString("NORMAL");
                    message.AppendString("builders_club_frontpage");
                    message.AppendInteger(0);
                    message.AppendInteger(1);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendInteger(3);
                    message.AppendInteger(8554);
                    message.AppendString("builders_club_1_month");
                    message.AppendString("");
                    message.AppendInteger(2560000);
                    message.AppendInteger(2560000);
                    message.AppendInteger(1024);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendBool(false);
                    message.AppendInteger(8606);
                    message.AppendString("builders_club_14_days");
                    message.AppendString("");
                    message.AppendInteger(2560000);
                    message.AppendInteger(2560000);
                    message.AppendInteger(1024);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendBool(false);
                    message.AppendInteger(8710);
                    message.AppendString("builders_club_31_days");
                    message.AppendString("");
                    message.AppendInteger(2560000);
                    message.AppendInteger(2560000);
                    message.AppendInteger(1024);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendBool(false);
                    break;

                case "bots":
                    message.AppendString("NORMAL");
                    message.AppendString("bots");
                    message.AppendInteger(2);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendInteger(3);
                    message.AppendString(page.Text1);
                    message.AppendString(page.Text2);
                    message.AppendString(page.TextDetails);
                    break;

                case "badge_display":
                    message.AppendString("NORMAL");
                    message.AppendString("badge_display");
                    message.AppendInteger(2);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendInteger(3);
                    message.AppendString(page.Text1);
                    message.AppendString(page.Text2);
                    message.AppendString(page.TextDetails);
                    break;

                case "info_loyalty":
                case "info_duckets":
                    message.AppendString("NORMAL");
                    message.AppendString(page.Layout);
                    message.AppendInteger(1);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendInteger(1);
                    message.AppendString(page.Text1);
                    break;

                case "sold_ltd_items":
                    message.AppendString("NORMAL");
                    message.AppendString("sold_ltd_items");
                    message.AppendInteger(2);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendInteger(3);
                    message.AppendString(page.Text1);
                    message.AppendString(page.Text2);
                    message.AppendString(page.TextDetails);
                    break;

                case "recycler_info":
                    message.AppendString("NORMAL");
                    message.AppendString(page.Layout);
                    message.AppendInteger(2);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendInteger(3);
                    message.AppendString(page.Text1);
                    message.AppendString(page.Text2);
                    message.AppendString(page.TextDetails);
                    break;

                case "recycler_prizes":
                    message.AppendString("NORMAL");
                    message.AppendString("recycler_prizes");
                    message.AppendInteger(1);
                    message.AppendString("catalog_recycler_headline3");
                    message.AppendInteger(1);
                    message.AppendString(page.Text1);
                    break;

                case "spaces_new":
                case "spaces":
                    message.AppendString("NORMAL");
                    message.AppendString("spaces_new");
                    message.AppendInteger(1);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendInteger(1);
                    message.AppendString(page.Text1);
                    break;

                case "recycler":
                    message.AppendString("NORMAL");
                    message.AppendString("recycler");
                    message.AppendInteger(2);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendInteger(1);
                    message.AppendString(page.Text1);
                    break;

                case "trophies":
                    message.AppendString("NORMAL");
                    message.AppendString("trophies");
                    message.AppendInteger(1);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendInteger(2);
                    message.AppendString(page.Text1);
                    message.AppendString(page.TextDetails);
                    break;

                case "pets":
                case "pets2":
                case "pets3":
                    message.AppendString("NORMAL");
                    message.AppendString(page.Layout);
                    message.AppendInteger(2);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendInteger(4);
                    message.AppendString(page.Text1);
                    message.AppendString(page.Text2);
                    message.AppendString(page.TextDetails);
                    message.AppendString(page.TextTeaser);
                    break;

                case "soundmachine":
                    message.AppendString("NORMAL");
                    message.AppendString(page.Layout);
                    message.AppendInteger(2);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendInteger(2);
                    message.AppendString(page.Text1);
                    message.AppendString(page.TextDetails);
                    break;

                case "vip_buy":
                    message.AppendString("NORMAL");
                    message.AppendString(page.Layout);
                    message.AppendInteger(2);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendInteger(0);
                    break;

                case "guild_custom_furni":
                    message.AppendString("NORMAL");
                    message.AppendString("guild_custom_furni");
                    message.AppendInteger(3);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString("");
                    message.AppendString("");
                    message.AppendInteger(3);
                    message.AppendString(page.Text1);
                    message.AppendString(page.TextDetails);
                    message.AppendString(page.Text2);
                    break;

                case "guild_frontpage":
                    message.AppendString("NORMAL");
                    message.AppendString("guild_frontpage");
                    message.AppendInteger(2);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendInteger(3);
                    message.AppendString(page.Text1);
                    message.AppendString(page.TextDetails);
                    message.AppendString(page.Text2);
                    break;

                case "guild_forum":
                    message.AppendString("NORMAL");
                    message.AppendString("guild_forum");
                    message.AppendInteger(0);
                    message.AppendInteger(2);
                    message.AppendString(page.Text1);
                    message.AppendString(page.Text2);
                    break;

                case "club_gifts":
                    message.AppendString("NORMAL");
                    message.AppendString("club_gifts");
                    message.AppendInteger(1);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendInteger(1);
                    message.AppendString(page.Text1);
                    break;

                case "default_3x3":
                    message.AppendString("NORMAL");
                    message.AppendString(page.Layout);
                    message.AppendInteger(3);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendString(page.LayoutSpecial);
                    message.AppendInteger(3);
                    message.AppendString(page.Text1);
                    message.AppendString(page.TextDetails);
                    message.AppendString(page.TextTeaser);
                    break;

                default:
                    message.AppendString("NORMAL");
                    message.AppendString(page.Layout);
                    message.AppendInteger(3);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendString(page.LayoutSpecial);
                    message.AppendInteger(4);
                    message.AppendString(page.Text1);
                    message.AppendString(page.Text2);
                    message.AppendString(page.TextTeaser);
                    message.AppendString(page.TextDetails);
                    break;

                case "builders_3x3":
                    message.AppendString("BUILDERS_CLUB");
                    message.AppendString("default_3x3");
                    message.AppendInteger(3);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendString(page.LayoutSpecial);
                    message.AppendInteger(3);
                    message.AppendString(page.Text1);
                    message.AppendString(page.TextDetails.Replace("[10]", Convert.ToChar(10).ToString())
                        .Replace("[13]", Convert.ToChar(13).ToString()));
                    message.AppendString(page.TextTeaser.Replace("[10]", Convert.ToChar(10).ToString())
                        .Replace("[13]", Convert.ToChar(13).ToString()));
                    break;

                case "builders_3x3_color":
                    message.AppendString("BUILDERS_CLUB");
                    message.AppendString("default_3x3_color_grouping");
                    message.AppendInteger(3);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendString(page.LayoutTeaser);
                    message.AppendString(page.LayoutSpecial);
                    message.AppendInteger(3);
                    message.AppendString(page.Text1);
                    message.AppendString(page.TextDetails.Replace("[10]", Convert.ToChar(10).ToString())
                        .Replace("[13]", Convert.ToChar(13).ToString()));
                    message.AppendString(page.TextTeaser.Replace("[10]", Convert.ToChar(10).ToString())
                        .Replace("[13]", Convert.ToChar(13).ToString()));
                    break;

                case "builders_club_frontpage":
                    message.AppendString("BUILDERS_CLUB");
                    message.AppendString("builders_club_frontpage");
                    message.AppendInteger(0);
                    message.AppendInteger(1);
                    message.AppendString(page.LayoutHeadline);
                    message.AppendInteger(3);
                    message.AppendInteger(8554);
                    message.AppendString("builders_club_1_month");
                    message.AppendString("");
                    message.AppendInteger(2560000);
                    message.AppendInteger(2560000);
                    message.AppendInteger(1024);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendBool(false);
                    message.AppendInteger(8606);
                    message.AppendString("builders_club_14_days");
                    message.AppendString("");
                    message.AppendInteger(2560000);
                    message.AppendInteger(2560000);
                    message.AppendInteger(1024);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendBool(false);
                    message.AppendInteger(8710);
                    message.AppendString("builders_club_31_days");
                    message.AppendString("");
                    message.AppendInteger(2560000);
                    message.AppendInteger(2560000);
                    message.AppendInteger(1024);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendBool(false);
                    break;

                case "builders_club_addons":
                    message.AppendString("BUILDERS_CLUB");
                    message.AppendString("builders_club_addons");
                    message.AppendInteger(0);
                    message.AppendInteger(1);
                    message.AppendString(page.LayoutHeadline);
                    break;

                case "builders_club_addons_normal":
                    message.AppendString("NORMAL");
                    message.AppendString("builders_club_addons");
                    message.AppendInteger(0);
                    message.AppendInteger(1);
                    message.AppendString(page.LayoutHeadline);
                    break;
            }

            if (page.Layout.StartsWith("frontpage") || page.Layout == "vip_buy" || page.Layout == "recycler")
            {
                message.AppendInteger(0);
            }
            else
            {
                message.AppendInteger(page.Items.Count);

                foreach (CatalogItem item in page.Items.Values)
                    ComposeItem(item, message);
            }

            message.AppendInteger(-1);
            message.AppendBool(false);

            return message;
        }

        /// <summary>
        ///     Composes the club purchase page.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="windowId">The window identifier.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage ComposeClubPurchasePage(GameClient session, int windowId)
        {
            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("CatalogueClubPageMessageComposer"));
            List<CatalogItem> habboClubItems = Yupi.GetGame().GetCatalog().HabboClubItems;

            message.AppendInteger(habboClubItems.Count);

            foreach (CatalogItem item in habboClubItems)
            {
                message.AppendInteger(item.Id);
                message.AppendString(item.Name);
                message.AppendBool(false);
                message.AppendInteger(item.CreditsCost);

                if (item.DiamondsCost > 0)
                {
                    message.AppendInteger(item.DiamondsCost);
                    message.AppendInteger(105);
                }
                else
                {
                    message.AppendInteger(item.DucketsCost);
                    message.AppendInteger(0);
                }

                message.AppendBool(true);
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

                message.AppendInteger((int) dayTime/31);
                message.AppendInteger((int) dayTime);
                message.AppendBool(false);
                message.AppendInteger((int) dayTime);
                message.AppendInteger(newExpiryDate.Year);
                message.AppendInteger(newExpiryDate.Month);
                message.AppendInteger(newExpiryDate.Day);
            }

            message.AppendInteger(windowId);
            return message;
        }

        internal static ServerMessage PurchaseOk(CatalogItem itemCatalog, Dictionary<Item, uint> items,
            int clubLevel = 1)
        {
            return PurchaseOk(itemCatalog.Id, itemCatalog.Name, itemCatalog.CreditsCost, items, clubLevel,
                itemCatalog.DiamondsCost,
                itemCatalog.DucketsCost, itemCatalog.IsLimited, itemCatalog.LimitedStack, itemCatalog.LimitedSelled);
        }

        /// <summary>
        ///     Purchases the ok.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage PurchaseOk(uint itemId, string itemName, uint creditsCost,
            Dictionary<Item, uint> items = null, int clubLevel = 1,
            uint diamondsCost = 0,
            uint activityPointsCost = 0, bool isLimited = false,
            uint limitedStack = 0,
            uint limitedSelled = 0)
        {
            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("PurchaseOKMessageComposer"));
            message.AppendInteger(itemId);
            message.AppendString(itemName);
            message.AppendBool(false);
            message.AppendInteger(creditsCost);
            message.AppendInteger(diamondsCost);
            message.AppendInteger(activityPointsCost);
            message.AppendBool(true);
            message.AppendInteger(items?.Count ?? 0);

            if (items != null)
            {
                foreach (KeyValuePair<Item, uint> itemDic in items)
                {
                    Item item = itemDic.Key;
                    message.AppendString(item.Type.ToString());

                    if (item.Type == 'b')
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

            message.AppendInteger(clubLevel); //clubLevel
            message.AppendBool(false); //window.visible?

            return message;
        }

        /// <summary>
        ///     Composes the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="message">The message.</param>
        internal static void ComposeItem(CatalogItem item, ServerMessage message)
        {
            message.AppendInteger(item.Id);

            string displayName = item.Name;

            if (PetTypeManager.ItemIsPet(item.Name))
                displayName = PetTypeManager.GetHabboPetType(item.Name);

            message.AppendString(displayName, true);
            message.AppendBool(false);
            message.AppendInteger(item.CreditsCost);

            if (item.DiamondsCost > 0)
            {
                message.AppendInteger(item.DiamondsCost);
                message.AppendInteger(105);
            }
            else
            {
                message.AppendInteger(item.DucketsCost);
                message.AppendInteger(0);
            }
            message.AppendBool(item.GetFirstBaseItem().AllowGift);

            switch (item.Name)
            {
                case "g0 group_product":
                    message.AppendInteger(0);
                    break;

                case "room_ad_plus_badge":
                    message.AppendInteger(1);
                    message.AppendString("b");
                    message.AppendString("RADZZ");
                    break;

                default:
                    if (item.Name.StartsWith("builders_club_addon_") || item.Name.StartsWith("builders_club_time_"))
                        message.AppendInteger(0);
                    else if (item.Badge == "")
                        message.AppendInteger(item.Items.Count);
                    else
                    {
                        message.AppendInteger(item.Items.Count + 1);
                        message.AppendString("b");
                        message.AppendString(item.Badge);
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
                    message.AppendString("");
                    message.AppendInteger(0);
                }
                else
                {
                    message.AppendString(baseItem.Type.ToString());
                    message.AppendInteger(baseItem.SpriteId);

                    if (item.Name.Contains("wallpaper_single") || item.Name.Contains("floor_single") ||
                        item.Name.Contains("landscape_single"))
                    {
                        string[] array = item.Name.Split('_');
                        message.AppendString(array[2]);
                    }
                    else if (item.Name.StartsWith("bot_") || baseItem.InteractionType == Interaction.MusicDisc ||
                             item.GetFirstBaseItem().Name == "poster")
                        message.AppendString(item.ExtraData);
                    else if (item.Name.StartsWith("poster_"))
                    {
                        string[] array2 = item.Name.Split('_');
                        message.AppendString(array2[1]);
                    }
                    else if (item.Name.StartsWith("poster "))
                    {
                        string[] array3 = item.Name.Split(' ');
                        message.AppendString(array3[1]);
                    }
                    else if (item.SongId > 0u && baseItem.InteractionType == Interaction.MusicDisc)
                        message.AppendString(item.ExtraData);
                    else
                        message.AppendString(string.Empty);

                    message.AppendInteger(item.Items[baseItem]);
                    message.AppendBool(item.IsLimited);
                    if (!item.IsLimited)
                        continue;
                    message.AppendInteger(item.LimitedStack);
                    message.AppendInteger(item.LimitedStack - item.LimitedSelled);
                }
            }
            message.AppendInteger(item.ClubOnly ? 1 : 0);

            if (item.IsLimited || item.FirstAmount != 1)
            {
                message.AppendBool(false);
                return;
            }

            message.AppendBool(item.HaveOffer && !item.IsLimited);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.Catalogs.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Messages;


namespace Yupi.Emulator.Game.Catalogs.Composers
{
    /// <summary>
    ///     Class CatalogPacket.
    /// </summary>
     public static class CatalogPageComposer
    {
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
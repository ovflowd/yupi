using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Emulator.Game.Items.Interfaces;

namespace Yupi.Emulator.Game.Catalogs.Interfaces
{
    /// <summary>
    ///     Class CatalogItem.
    /// </summary>
     class CatalogItem
    {
        /// <summary>
        ///     The club only
        /// </summary>
         readonly bool ClubOnly;

        /// <summary>
        ///     The credits cost
        /// </summary>
         readonly uint CreditsCost;

        /// <summary>
        ///     The diamonds cost
        /// </summary>
         readonly uint DiamondsCost;

        /// <summary>
        ///     The duckets cost
        /// </summary>
         readonly uint DucketsCost;

        /// <summary>
        ///     The extra data
        /// </summary>
         readonly string ExtraData;

        /// <summary>
        ///     The have offer
        /// </summary>
         readonly bool HaveOffer;

        /// <summary>
        ///     The identifier
        /// </summary>
         readonly uint Id;

        /// <summary>
        ///     The is limited
        /// </summary>
         readonly bool IsLimited;

        /// <summary>
        ///     The item identifier string
        /// </summary>
         readonly string ItemNamesString;

        /// <summary>
        ///     The limited stack
        /// </summary>
         readonly uint LimitedStack;

        /// <summary>
        ///     The name
        /// </summary>
         readonly string Name;

        /// <summary>
        ///     The page identifier
        /// </summary>
         readonly uint PageId;

        /// <summary>
        ///     The song identifier
        /// </summary>
         readonly uint SongId;

        /// <summary>
        ///     The badge
        /// </summary>
         string Badge;

        /// <summary>
        ///     The base identifier
        /// </summary>
         uint BaseId;

        /// <summary>
        ///     The base name
        /// </summary>
         string BaseName;

        /// <summary>
        ///     The first amount
        /// </summary>
         uint FirstAmount;

        /// <summary>
        ///     The items
        /// </summary>
         Dictionary<Item, uint> Items;

        /// <summary>
        ///     The limited selled
        /// </summary>
         uint LimitedSelled;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CatalogItem" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="name">The name.</param>
         CatalogItem(DataRow row, string name)
        {
            // Item Id
            Id = (uint) row["id"];

            // Item Name
            Name = name;

            // Multiple Items
            ItemNamesString = (string) row["item_names"];
            Items = new Dictionary<Item, uint>();

            // String Arrays
            string[] itemNames = ItemNamesString.Split(';');
            string[] amounts = row["amounts"].ToString().Split(';');

            for (int i = 0; i < itemNames.Length; i++)
            {
                uint amount;

                Item item;

                if (!Yupi.GetGame().GetItemManager().GetItem(itemNames[i], out item))
                    continue;

                uint.TryParse(amounts[i], out amount);

                Items.Add(item, amount);
            }

            // Strings
            BaseName = Items.Keys.First().Name;
            Badge = (string) row["badge"];
            HaveOffer = (string) row["offer_active"] == "1";
            ClubOnly = (string) row["club_only"] == "1";
            ExtraData = (string) row["extradata"];

            // Positive Integers (Unsigned)
            PageId = (uint) row["page_id"];
            CreditsCost = (uint) row["cost_credits"];
            DiamondsCost = (uint) row["cost_diamonds"];
            DucketsCost = (uint) row["cost_duckets"];
            LimitedSelled = (uint) row["limited_sells"];
            LimitedStack = (uint) row["limited_stack"];
            BaseId = Items.Keys.First().ItemId;
            FirstAmount = Items.Values.First();
            SongId = (uint) row["song_id"];

            // Booleans
            IsLimited = LimitedStack > 0;
        }

        /// <summary>
        ///     Gets the base item.
        /// </summary>
        /// <param name="itemId">The item ids.</param>
        /// <returns>Item.</returns>
         Item GetBaseItem(uint itemId) => Yupi.GetGame().GetItemManager().GetItem(itemId);

        /// <summary>
        ///     Gets the first base item.
        /// </summary>
        /// <returns>Item.</returns>
         Item GetFirstBaseItem() => GetBaseItem(BaseId);
    }
}
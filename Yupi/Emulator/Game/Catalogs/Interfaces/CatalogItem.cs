using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.Catalogs.Interfaces
{
    /// <summary>
    ///     Class CatalogItem.
    /// </summary>
    internal class CatalogItem
    {
        /// <summary>
        ///     The club only
        /// </summary>
        internal readonly bool ClubOnly;

        /// <summary>
        ///     The credits cost
        /// </summary>
        internal readonly uint CreditsCost;

        /// <summary>
        ///     The diamonds cost
        /// </summary>
        internal readonly uint DiamondsCost;

        /// <summary>
        ///     The duckets cost
        /// </summary>
        internal readonly uint DucketsCost;

        /// <summary>
        ///     The extra data
        /// </summary>
        internal readonly string ExtraData;

        /// <summary>
        ///     The have offer
        /// </summary>
        internal readonly bool HaveOffer;

        /// <summary>
        ///     The identifier
        /// </summary>
        internal readonly uint Id;

        /// <summary>
        ///     The is limited
        /// </summary>
        internal readonly bool IsLimited;

        /// <summary>
        ///     The item identifier string
        /// </summary>
        internal readonly string ItemNamesString;

        /// <summary>
        ///     The limited stack
        /// </summary>
        internal readonly uint LimitedStack;

        /// <summary>
        ///     The name
        /// </summary>
        internal readonly string Name;

        /// <summary>
        ///     The page identifier
        /// </summary>
        internal readonly uint PageId;

        /// <summary>
        ///     The song identifier
        /// </summary>
        internal readonly uint SongId;

        /// <summary>
        ///     The badge
        /// </summary>
        internal string Badge;

        /// <summary>
        ///     The base identifier
        /// </summary>
        internal uint BaseId;

        /// <summary>
        ///     The base name
        /// </summary>
        internal string BaseName;

        /// <summary>
        ///     The first amount
        /// </summary>
        internal uint FirstAmount;

        /// <summary>
        ///     The items
        /// </summary>
        internal Dictionary<Item, uint> Items;

        /// <summary>
        ///     The limited selled
        /// </summary>
        internal uint LimitedSelled;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CatalogItem" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="name">The name.</param>
        internal CatalogItem(DataRow row, string name)
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
        internal Item GetBaseItem(uint itemId) => Yupi.GetGame().GetItemManager().GetItem(itemId);

        /// <summary>
        ///     Gets the first base item.
        /// </summary>
        /// <returns>Item.</returns>
        internal Item GetFirstBaseItem() => GetBaseItem(BaseId);
    }
}
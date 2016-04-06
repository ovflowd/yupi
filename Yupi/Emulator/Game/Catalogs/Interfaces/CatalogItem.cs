using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Emulator.Game.Items.Interfaces;

namespace Yupi.Emulator.Game.Catalogs.Interfaces
{
    /// <summary>
    ///     Class CatalogItem.
    /// </summary>
     public class CatalogItem
    {
        /// <summary>
        ///     The club only
        /// </summary>
     public readonly bool ClubOnly;

        /// <summary>
        ///     The credits cost
        /// </summary>
     public readonly uint CreditsCost;

        /// <summary>
        ///     The diamonds cost
        /// </summary>
     public readonly uint DiamondsCost;

        /// <summary>
        ///     The duckets cost
        /// </summary>
     public readonly uint DucketsCost;

        /// <summary>
        ///     The extra data
        /// </summary>
     public readonly string ExtraData;

        /// <summary>
        ///     The have offer
        /// </summary>
     public readonly bool HaveOffer;

        /// <summary>
        ///     The identifier
        /// </summary>
     public readonly uint Id;

        /// <summary>
        ///     The is limited
        /// </summary>
     public readonly bool IsLimited;

        /// <summary>
        ///     The item identifier string
        /// </summary>
     public readonly string ItemNamesString;

        /// <summary>
        ///     The limited stack
        /// </summary>
     public readonly uint LimitedStack;

        /// <summary>
        ///     The name
        /// </summary>
     public readonly string Name;

        /// <summary>
        ///     The page identifier
        /// </summary>
     public readonly uint PageId;

        /// <summary>
        ///     The song identifier
        /// </summary>
     public readonly uint SongId;

        /// <summary>
        ///     The badge
        /// </summary>
     public string Badge;

        /// <summary>
        ///     The base identifier
        /// </summary>
     public uint BaseId;

        /// <summary>
        ///     The base name
        /// </summary>
     public string BaseName;

        /// <summary>
        ///     The first amount
        /// </summary>
     public uint FirstAmount;

        /// <summary>
        ///     The items
        /// </summary>
     public Dictionary<Item, uint> Items;

        /// <summary>
        ///     The limited selled
        /// </summary>
     public uint LimitedSelled;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CatalogItem" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="name">The name.</param>
     public CatalogItem(DataRow row, string name)
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
     public Item GetBaseItem(uint itemId) => Yupi.GetGame().GetItemManager().GetItem(itemId);

        /// <summary>
        ///     Gets the first base item.
        /// </summary>
        /// <returns>Item.</returns>
     public Item GetFirstBaseItem() => GetBaseItem(BaseId);
    }
}
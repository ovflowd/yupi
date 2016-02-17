using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.Items
{
    /// <summary>
    ///     Class ClothesManagerManager.
    /// </summary>
    internal class ClothingManager
    {
        /// <summary>
        ///     The clothing items
        /// </summary>
        internal Dictionary<string, ClothingItem> ClothingItems;

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void Initialize(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT * FROM catalog_wearables");

            ClothingItems = new Dictionary<string, ClothingItem>();

            DataTable table = dbClient.GetTable();

            foreach (DataRow dataRow in table.Rows)
                ClothingItems.Add(dataRow["item_name"].ToString(), new ClothingItem(dataRow));
        }

        /// <summary>
        ///     Gets the clothes in furni.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>ClothingItem.</returns>
        internal ClothingItem GetClothesInFurni(string name) => ClothingItems.FirstOrDefault(p => p.Key == name).Value;
    }
}
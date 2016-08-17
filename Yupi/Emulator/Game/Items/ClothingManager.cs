using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Items.Interfaces;

namespace Yupi.Emulator.Game.Items
{
    /// <summary>
    ///     Class ClothesManagerManager.
    /// </summary>
     public class ClothingManager
    {
        /// <summary>
        ///     The clothing items
        /// </summary>
     public Dictionary<string, ClothingItem> ClothingItems;

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
     public void Initialize(IQueryAdapter dbClient)
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
     public ClothingItem GetClothesInFurni(string name) => ClothingItems.FirstOrDefault(p => p.Key == name).Value;
    }
}
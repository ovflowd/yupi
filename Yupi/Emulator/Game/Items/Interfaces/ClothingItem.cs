using System;
using System.Collections.Generic;
using System.Data;

namespace Yupi.Emulator.Game.Items.Interfaces
{
    /// <summary>
    ///     Class ClothingItem.
    /// </summary>
     public class ClothingItem
    {
        /// <summary>
        ///     The clothes
        /// </summary>
     public List<int> Clothes;

        /// <summary>
        ///     The identifier
        /// </summary>
     public uint Id;

        /// <summary>
        ///     The item name
        /// </summary>
     public string ItemName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClothingItem" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
     public ClothingItem(DataRow row)
        {
            Clothes = new List<int>();
            ItemName = Convert.ToString(row["item_name"]);
            Id = Convert.ToUInt32(row["id"]);

            string text = Convert.ToString(row["clothings"]);

            if (!string.IsNullOrEmpty(text))
            {
                if (text.Contains(","))
                {
                    string[] array = text.Split(',');

                    foreach (string value in array)
                    {
                        if (value != null)
                            Clothes.Add(string.IsNullOrWhiteSpace(value)
                                ? Convert.ToInt32(value.Replace(" ", string.Empty))
                                : Convert.ToInt32(value));
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(text))
                        text = text.Replace(" ", string.Empty);

                    if (!string.IsNullOrEmpty(text))
                        Clothes.Add(Convert.ToInt32(text));
                }
            }
        }
    }
}
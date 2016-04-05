using System;
using System.Collections.Generic;
using System.Data;

namespace Yupi.Emulator.Game.Items.Interfaces
{
    /// <summary>
    ///     Class PinataItem.
    /// </summary>
     class PinataItem
    {
        /// <summary>
        ///     The item base identifier
        /// </summary>
         uint ItemBaseId;

        /// <summary>
        ///     The rewards
        /// </summary>
         List<uint> Rewards;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PinataItem" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
         PinataItem(DataRow row)
        {
            Rewards = new List<uint>();
            ItemBaseId = Convert.ToUInt32(row["item_baseid"]);
            string text = row["rewards"].ToString();
            string[] array = text.Split(';');
            foreach (string value in array)
                Rewards.Add(Convert.ToUInt32(value));
        }
    }
}
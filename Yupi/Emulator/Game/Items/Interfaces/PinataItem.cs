using System;
using System.Collections.Generic;
using System.Data;

namespace Yupi.Emulator.Game.Items.Interfaces
{
    /// <summary>
    ///     Class PinataItem.
    /// </summary>
     public class PinataItem
    {
        /// <summary>
        ///     The item base identifier
        /// </summary>
     public uint ItemBaseId;

        /// <summary>
        ///     The rewards
        /// </summary>
     public List<uint> Rewards;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PinataItem" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
     public PinataItem(DataRow row)
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
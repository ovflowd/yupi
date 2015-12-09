using System;
using System.Collections.Generic;
using System.Data;

namespace Yupi.Game.Items.Interfaces
{
    /// <summary>
    ///     Class PinataItem.
    /// </summary>
    internal class PinataItem
    {
        /// <summary>
        ///     The item base identifier
        /// </summary>
        internal uint ItemBaseId;

        /// <summary>
        ///     The rewards
        /// </summary>
        internal List<uint> Rewards;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PinataItem" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
        internal PinataItem(DataRow row)
        {
            Rewards = new List<uint>();
            ItemBaseId = Convert.ToUInt32(row["item_baseid"]);
            var text = row["rewards"].ToString();
            var array = text.Split(';');
            foreach (var value in array)
                Rewards.Add(Convert.ToUInt32(value));
        }
    }
}
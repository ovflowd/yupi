using Yupi.Emulator.Game.Items.Interfaces;

namespace Yupi.Emulator.Game.Catalogs.Interfaces
{
    /// <summary>
    ///     Class EcotronReward.
    /// </summary>
     public class EcotronReward
    {
        /// <summary>
        ///     The base identifier
        /// </summary>
     public uint BaseId;

        /// <summary>
        ///     The display identifier
        /// </summary>
     public uint DisplayId;

        /// <summary>
        ///     The reward level
        /// </summary>
     public uint RewardLevel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EcotronReward" /> class.
        /// </summary>
        /// <param name="displayId">The display identifier.</param>
        /// <param name="baseId">The base identifier.</param>
        /// <param name="rewardLevel">The reward level.</param>
     public EcotronReward(uint displayId, uint baseId, uint rewardLevel)
        {
            DisplayId = displayId;
            BaseId = baseId;
            RewardLevel = rewardLevel;
        }

        /// <summary>
        ///     Gets the base item.
        /// </summary>
        /// <returns>Item.</returns>
     public Item GetBaseItem()
        {
            return Yupi.GetGame().GetItemManager().GetItem(BaseId);
        }
    }
}
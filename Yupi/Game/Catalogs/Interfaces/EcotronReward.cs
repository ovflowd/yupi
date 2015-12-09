using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.Catalogs.Interfaces
{
    /// <summary>
    ///     Class EcotronReward.
    /// </summary>
    internal class EcotronReward
    {
        /// <summary>
        ///     The base identifier
        /// </summary>
        internal uint BaseId;

        /// <summary>
        ///     The display identifier
        /// </summary>
        internal uint DisplayId;

        /// <summary>
        ///     The reward level
        /// </summary>
        internal uint RewardLevel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EcotronReward" /> class.
        /// </summary>
        /// <param name="displayId">The display identifier.</param>
        /// <param name="baseId">The base identifier.</param>
        /// <param name="rewardLevel">The reward level.</param>
        internal EcotronReward(uint displayId, uint baseId, uint rewardLevel)
        {
            DisplayId = displayId;
            BaseId = baseId;
            RewardLevel = rewardLevel;
        }

        /// <summary>
        ///     Gets the base item.
        /// </summary>
        /// <returns>Item.</returns>
        internal Item GetBaseItem()
        {
            return Yupi.GetGame().GetItemManager().GetItem(BaseId);
        }
    }
}
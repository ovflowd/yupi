namespace Yupi.Game.Achievements.Structs
{
    /// <summary>
    ///     Struct AchievementLevel
    /// </summary>
    internal struct AchievementLevel
    {
        /// <summary>
        ///     The level
        /// </summary>
        internal readonly int Level;

        /// <summary>
        ///     The reward pixels
        /// </summary>
        internal readonly int RewardPixels;

        /// <summary>
        ///     The reward points
        /// </summary>
        internal readonly int RewardPoints;

        /// <summary>
        ///     The requirement
        /// </summary>
        internal readonly int Requirement;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AchievementLevel" /> struct.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="rewardPixels">The reward pixels.</param>
        /// <param name="rewardPoints">The reward points.</param>
        /// <param name="requirement">The requirement.</param>
        public AchievementLevel(int level, int rewardPixels, int rewardPoints, int requirement)
        {
            Level = level;
            RewardPixels = rewardPixels;
            RewardPoints = rewardPoints;
            Requirement = requirement;
        }
    }
}
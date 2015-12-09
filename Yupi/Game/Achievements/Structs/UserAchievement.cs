namespace Yupi.Game.Achievements.Structs
{
    /// <summary>
    ///     Class UserAchievement.
    /// </summary>
    internal class UserAchievement
    {
        /// <summary>
        ///     The achievement group
        /// </summary>
        internal readonly string AchievementGroup;

        /// <summary>
        ///     The level
        /// </summary>
        internal int Level;

        /// <summary>
        ///     The progress
        /// </summary>
        internal int Progress;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserAchievement" /> class.
        /// </summary>
        /// <param name="achievementGroup">The achievement group.</param>
        /// <param name="level">The level.</param>
        /// <param name="progress">The progress.</param>
        internal UserAchievement(string achievementGroup, int level, int progress)
        {
            AchievementGroup = achievementGroup;
            Level = level;
            Progress = progress;
        }

        internal void SetLevel(int level)
        {
            Level = level;
        }

        internal void SetProgress(int progress)
        {
            Progress = progress;
        }
    }
}
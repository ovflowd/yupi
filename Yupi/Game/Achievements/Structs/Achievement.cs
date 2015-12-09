using System.Collections.Generic;

namespace Yupi.Game.Achievements.Structs
{
    /// <summary>
    ///     Class Achievement.
    /// </summary>
    internal class Achievement
    {
        /// <summary>
        ///     The identifier
        /// </summary>
        internal readonly uint Id;

        /// <summary>
        ///     The group name
        /// </summary>
        internal readonly string GroupName;

        /// <summary>
        ///     The category
        /// </summary>
        internal readonly string Category;

        /// <summary>
        ///     The levels
        /// </summary>
        internal readonly Dictionary<int, AchievementLevel> Levels;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Achievement" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="category">The category.</param>
        internal Achievement(uint id, string groupName, string category)
        {
            Id = id;
            GroupName = groupName;
            Category = category;
            Levels = new Dictionary<int, AchievementLevel>();
        }

        /// <summary>
        ///     Adds the level.
        /// </summary>
        /// <param name="level">The level.</param>
        internal void AddLevel(AchievementLevel level)
        {
            Levels.Add(level.Level, level);
        }

        internal bool CheckLevel(AchievementLevel level) => Levels.ContainsKey(level.Level);
    }
}
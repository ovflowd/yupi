/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System.Collections.Generic;

namespace Yupi.Game.Achievements.Structs
{
    /// <summary>
    ///     Class Achievement.
    /// </summary>
    internal class Achievement
    {
        /// <summary>
        ///     The category
        /// </summary>
        internal readonly string Category;

        /// <summary>
        ///     The group name
        /// </summary>
        internal readonly string GroupName;

        /// <summary>
        ///     The identifier
        /// </summary>
        internal readonly uint Id;

        /// <summary>
        ///     The levels
        /// </summary>
        internal readonly Dictionary<uint, AchievementLevel> Levels;

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

            Levels = new Dictionary<uint, AchievementLevel>();
        }

        /// <summary>
        ///     Adds the level.
        /// </summary>
        /// <param name="level">The level.</param>
        internal void AddLevel(AchievementLevel level) => Levels.Add(level.Level, level);

        internal bool CheckLevel(AchievementLevel level) => Levels.ContainsKey(level.Level);
    }
}
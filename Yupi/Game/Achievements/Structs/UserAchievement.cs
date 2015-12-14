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
        internal uint Level;

        /// <summary>
        ///     The progress
        /// </summary>
        internal uint Progress;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserAchievement" /> class.
        /// </summary>
        /// <param name="achievementGroup">The achievement group.</param>
        /// <param name="level">The level.</param>
        /// <param name="progress">The progress.</param>
        internal UserAchievement(string achievementGroup, uint level, uint progress)
        {
            AchievementGroup = achievementGroup;
            Level = level;
            Progress = progress;
        }

        internal void SetLevel(uint level) => Level = level;

        internal void SetProgress(uint progress) => Progress = progress;
    }
}
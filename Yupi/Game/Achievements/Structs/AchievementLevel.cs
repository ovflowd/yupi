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
    ///     Struct AchievementLevel
    /// </summary>
    internal struct AchievementLevel
    {
        /// <summary>
        ///     The level
        /// </summary>
        internal readonly uint Level;

        /// <summary>
        ///     The reward pixels
        /// </summary>
        internal readonly uint RewardPixels;

        /// <summary>
        ///     The reward points
        /// </summary>
        internal readonly uint RewardPoints;

        /// <summary>
        ///     The requirement
        /// </summary>
        internal readonly uint Requirement;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AchievementLevel" /> struct.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="rewardPixels">The reward pixels.</param>
        /// <param name="rewardPoints">The reward points.</param>
        /// <param name="requirement">The requirement.</param>
        public AchievementLevel(uint level, uint rewardPixels, uint rewardPoints, uint requirement)
        {
            Level = level;
            RewardPixels = rewardPixels;
            RewardPoints = rewardPoints;
            Requirement = requirement;
        }
    }
}
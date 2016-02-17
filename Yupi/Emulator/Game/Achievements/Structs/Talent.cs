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
    ///     Class Talent.
    /// </summary>
    internal struct Talent
    {
        /// <summary>
        ///     The identifier
        /// </summary>
        internal int Id;

        /// <summary>
        ///     The type
        /// </summary>
        internal string Type;

        /// <summary>
        ///     The parent category
        /// </summary>
        internal int ParentCategory;

        /// <summary>
        ///     The level
        /// </summary>
        internal int Level;

        /// <summary>
        ///     The achievement group
        /// </summary>
        internal string AchievementGroup;

        /// <summary>
        ///     The achievement level
        /// </summary>
        internal uint AchievementLevel;

        /// <summary>
        ///     The prize
        /// </summary>
        internal string Prize;

        /// <summary>
        ///     The prize base item
        /// </summary>
        internal uint PrizeBaseItem;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Talent" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="type">The type.</param>
        /// <param name="parentCategory">The parent category.</param>
        /// <param name="level">The level.</param>
        /// <param name="achId">The ach identifier.</param>
        /// <param name="achLevel">The ach level.</param>
        /// <param name="prize">The prize.</param>
        /// <param name="prizeBaseItem">The prize base item.</param>
        internal Talent(int id, string type, int parentCategory, int level, string achId, uint achLevel, string prize,
            uint prizeBaseItem)
        {
            Id = id;
            Type = type;
            ParentCategory = parentCategory;
            Level = level;
            AchievementGroup = achId;
            AchievementLevel = achLevel;
            Prize = prize;
            PrizeBaseItem = prizeBaseItem;
        }

        /// <summary>
        ///     Gets the achievement.
        /// </summary>
        /// <returns>Achievement.</returns>
        internal Achievement GetAchievement()
            =>
                string.IsNullOrEmpty(AchievementGroup) || ParentCategory == -1
                    ? null
                    : Yupi.GetGame().GetAchievementManager().GetAchievement(AchievementGroup);
    }
}
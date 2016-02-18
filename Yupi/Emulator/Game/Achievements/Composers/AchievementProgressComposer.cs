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

using Yupi.Emulator.Game.Achievements.Structs;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Achievements.Composers
{
    /// <summary>
    ///     Class AchievementProgressComposer.
    /// </summary>
    internal class AchievementProgressComposer
    {
        /// <summary>
        ///     Composes the specified achievement.
        /// </summary>
        /// <param name="achievement">The achievement.</param>
        /// <param name="targetLevel">The target level.</param>
        /// <param name="targetLevelData">The target level data.</param>
        /// <param name="totalLevels">The total levels.</param>
        /// <param name="userData">The user data.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal static SimpleServerMessageBuffer Compose(Achievement achievement, uint targetLevel,
            AchievementLevel targetLevelData, uint totalLevels, UserAchievement userData)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("AchievementProgressMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(achievement.Id);
            simpleServerMessageBuffer.AppendInteger(targetLevel);
            simpleServerMessageBuffer.AppendString($"{achievement.GroupName}{targetLevel}");
            simpleServerMessageBuffer.AppendInteger(targetLevelData.Requirement);
            simpleServerMessageBuffer.AppendInteger(targetLevelData.Requirement);
            simpleServerMessageBuffer.AppendInteger(targetLevelData.RewardPixels);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(userData.Progress);
            simpleServerMessageBuffer.AppendBool(userData.Level >= totalLevels);
            simpleServerMessageBuffer.AppendString(achievement.Category);
            simpleServerMessageBuffer.AppendString(string.Empty);
            simpleServerMessageBuffer.AppendInteger(totalLevels);
            simpleServerMessageBuffer.AppendInteger(0);

            return simpleServerMessageBuffer;
        }
    }
}
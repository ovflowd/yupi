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

using Yupi.Game.Achievements.Structs;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Achievements.Composers
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
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(Achievement achievement, uint targetLevel,
            AchievementLevel targetLevelData, uint totalLevels, UserAchievement userData)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("AchievementProgressMessageComposer"));

            serverMessage.AppendInteger(achievement.Id);
            serverMessage.AppendInteger(targetLevel);
            serverMessage.AppendString($"{achievement.GroupName}{targetLevel}");
            serverMessage.AppendInteger(targetLevelData.Requirement);
            serverMessage.AppendInteger(targetLevelData.Requirement);
            serverMessage.AppendInteger(targetLevelData.RewardPixels);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(userData.Progress);
            serverMessage.AppendBool(userData.Level >= totalLevels);
            serverMessage.AppendString(achievement.Category);
            serverMessage.AppendString(string.Empty);
            serverMessage.AppendInteger(totalLevels);
            serverMessage.AppendInteger(0);

            return serverMessage;
        }
    }
}
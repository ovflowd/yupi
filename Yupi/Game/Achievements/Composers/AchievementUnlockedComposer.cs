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
    ///     Class AchievementUnlockedComposer.
    /// </summary>
    internal class AchievementUnlockedComposer
    {
        /// <summary>
        ///     Composes the specified achievement.
        /// </summary>
        /// <param name="achievement">The achievement.</param>
        /// <param name="level">The level.</param>
        /// <param name="pointReward">The point reward.</param>
        /// <param name="pixelReward">The pixel reward.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(Achievement achievement, uint level, uint pointReward, uint pixelReward)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("UnlockAchievementMessageComposer"));

            serverMessage.AppendInteger(achievement.Id);
            serverMessage.AppendInteger(level);
            serverMessage.AppendInteger(144);
            serverMessage.AppendString($"{achievement.GroupName}{level}");
            serverMessage.AppendInteger(pointReward);
            serverMessage.AppendInteger(pixelReward);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(10);
            serverMessage.AppendInteger(21);
            serverMessage.AppendString(level > 1 ? $"{achievement.GroupName}{level - 1}" : string.Empty);
            serverMessage.AppendString(achievement.Category);
            serverMessage.AppendBool(true);

            return serverMessage;
        }
    }
}
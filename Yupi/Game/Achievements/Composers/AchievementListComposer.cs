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
using Yupi.Game.Achievements.Structs;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Achievements.Composers
{
    /// <summary>
    ///     Class AchievementListComposer.
    /// </summary>
    internal class AchievementListComposer
    {
        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="achievements">The achievements.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(GameClient session, List<Achievement> achievements)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("AchievementListMessageComposer"));

            serverMessage.AppendInteger(achievements.Count);

            foreach (Achievement achievement in achievements)
            {
                UserAchievement achievementData = session.GetHabbo().GetAchievementData(achievement.GroupName);

                uint i = achievementData?.Level + 1 ?? 1;

                uint count = (uint) achievement.Levels.Count;

                if (i > count)
                    i = count;

                AchievementLevel achievementLevel = achievement.Levels[i];

                AchievementLevel oldLevel = achievement.Levels.ContainsKey(i - 1) ? achievement.Levels[i - 1] : achievementLevel;

                serverMessage.AppendInteger(achievement.Id);
                serverMessage.AppendInteger(i);
                serverMessage.AppendString($"{achievement.GroupName}{i}");
                serverMessage.AppendInteger(oldLevel.Requirement);
                serverMessage.AppendInteger(achievementLevel.Requirement);
                serverMessage.AppendInteger(achievementLevel.RewardPoints);
                serverMessage.AppendInteger(0);
                serverMessage.AppendInteger(achievementData?.Progress ?? 0);
                serverMessage.AppendBool(!(achievementData == null || achievementData.Level < count));
                serverMessage.AppendString(achievement.Category);
                serverMessage.AppendString(string.Empty);
                serverMessage.AppendInteger(count);
                serverMessage.AppendInteger(0);
            }

            serverMessage.AppendString(string.Empty);

            return serverMessage;
        }
    }
}
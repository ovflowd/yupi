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
using Yupi.Emulator.Game.Achievements.Structs;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Achievements.Composers
{
    /// <summary>
    ///     Class AchievementListComposer.
    /// </summary>
     public class AchievementListComposer
    {
        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="achievements">The achievements.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public static SimpleServerMessageBuffer Compose(GameClient session, List<Achievement> achievements)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("AchievementListMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(achievements.Count);

            foreach (Achievement achievement in achievements)
            {
                UserAchievement achievementData = session.GetHabbo().GetAchievementData(achievement.GroupName);

                uint i = achievementData?.Level + 1 ?? 1;

                uint count = (uint) achievement.Levels.Count;

                if (i > count)
                    i = count;

                AchievementLevel achievementLevel = achievement.Levels[i];

                AchievementLevel oldLevel = achievement.Levels.ContainsKey(i - 1) ? achievement.Levels[i - 1] : achievementLevel;

                simpleServerMessageBuffer.AppendInteger(achievement.Id);
                simpleServerMessageBuffer.AppendInteger(i);
                simpleServerMessageBuffer.AppendString($"{achievement.GroupName}{i}");
                simpleServerMessageBuffer.AppendInteger(oldLevel.Requirement);
                simpleServerMessageBuffer.AppendInteger(achievementLevel.Requirement);
                simpleServerMessageBuffer.AppendInteger(achievementLevel.RewardPoints);
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendInteger(achievementData?.Progress ?? 0);
                simpleServerMessageBuffer.AppendBool(!(achievementData == null || achievementData.Level < count));
                simpleServerMessageBuffer.AppendString(achievement.Category);
                simpleServerMessageBuffer.AppendString(string.Empty);
                simpleServerMessageBuffer.AppendInteger(count);
                simpleServerMessageBuffer.AppendInteger(0);
            }

            simpleServerMessageBuffer.AppendString(string.Empty);

            return simpleServerMessageBuffer;
        }
    }
}
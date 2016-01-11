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

using System;
using System.Collections.Generic;
using System.Data;
using Yupi.Core.Io;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Achievements.Structs;

namespace Yupi.Game.Achievements.Factories
{
    /// <summary>
    ///     Class AchievementLevelFactory.
    /// </summary>
    internal class AchievementLevelFactory
    {
        /// <summary>
        ///     Gets the achievement levels.
        /// </summary>
        /// <param name="achievements">The achievements.</param>
        /// <param name="dbClient">The database client.</param>
        internal static void GetAchievementLevels(out Dictionary<string, Achievement> achievements,
            IQueryAdapter dbClient)
        {
            achievements = new Dictionary<string, Achievement>();

            dbClient.SetQuery("SELECT * FROM achievements_data");

            foreach (DataRow dataRow in dbClient.GetTable().Rows)
            {
                string achievementName = dataRow["achievement_name"].ToString();

                AchievementLevel level = new AchievementLevel((uint) dataRow["achievement_level"], (uint) dataRow["reward_pixels"],
                    (uint) dataRow["reward_points"], (uint) dataRow["progress_needed"]);

                if (!achievements.ContainsKey(achievementName))
                    achievements.Add(achievementName,
                        new Achievement((uint) dataRow["id"], achievementName,
                            dataRow["achievement_category"].ToString()));

                if (!achievements[achievementName].CheckLevel(level))
                    achievements[achievementName].AddLevel(level);
                else
                    Writer.WriteLine(
                        "Was Found a Duplicated Level for: " + achievementName + ", Level: " + level.Level,
                        "Yupi.Achievements", ConsoleColor.Cyan);
            }
        }
    }
}
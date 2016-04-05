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
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;

namespace Yupi.Emulator.Game.Achievements.Composers
{
    /// <summary>
    ///     Class AchievementTalentComposer.
    /// </summary>
     class AchievementTalentComposer
    {
        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="talent">The Talent.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
         static SimpleServerMessageBuffer Compose(GameClient session, Talent talent)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("TalentLevelUpMessageComposer"));

            simpleServerMessageBuffer.AppendString(talent.Type);
            simpleServerMessageBuffer.AppendInteger(talent.Level);
            simpleServerMessageBuffer.AppendInteger(0);

            if (talent.Type == "citizenship" && talent.Level == 4)
            {
                simpleServerMessageBuffer.AppendInteger(2);
                simpleServerMessageBuffer.AppendString("HABBO_CLUB_VIP_7_DAYS");
                simpleServerMessageBuffer.AppendInteger(7);
                simpleServerMessageBuffer.AppendString(talent.Prize);
                simpleServerMessageBuffer.AppendInteger(0);
            }
            else
            {
                simpleServerMessageBuffer.AppendInteger(1);
                simpleServerMessageBuffer.AppendString(talent.Prize);
                simpleServerMessageBuffer.AppendInteger(0);
            }

            return simpleServerMessageBuffer;
        }
    }
}
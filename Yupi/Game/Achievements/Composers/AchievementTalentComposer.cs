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
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Achievements.Composers
{
    /// <summary>
    ///     Class AchievementTalentComposer.
    /// </summary>
    internal class AchievementTalentComposer
    {
        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="talent">The Talent.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(GameClient session, Talent talent)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("TalentLevelUpMessageComposer"));

            serverMessage.AppendString(talent.Type);
            serverMessage.AppendInteger(talent.Level);
            serverMessage.AppendInteger(0);

            if (talent.Type == "citizenship" && talent.Level == 4)
            {
                serverMessage.AppendInteger(2);
                serverMessage.AppendString("HABBO_CLUB_VIP_7_DAYS");
                serverMessage.AppendInteger(7);
                serverMessage.AppendString(talent.Prize);
                serverMessage.AppendInteger(0);
            }
            else
            {
                serverMessage.AppendInteger(1);
                serverMessage.AppendString(talent.Prize);
                serverMessage.AppendInteger(0);
            }

            return serverMessage;
        }
    }
}
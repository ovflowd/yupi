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

using Yupi.Emulator.Game.Rooms.Competitions.Models;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Rooms.Competitions.Composers
{
    class RoomCompetitionVotingInfoComposer
    {
        internal static SimpleServerMessageBuffer Compose(RoomCompetition competition, SimpleServerMessageBuffer messageBuffer, Habbo user, int status = 0)
        {
            messageBuffer.Init(PacketLibraryManager.OutgoingHandler("CompetitionVotingInfoMessageComposer"));

            messageBuffer.AppendInteger(competition.Id);
            messageBuffer.AppendString(competition.Name);
            messageBuffer.AppendInteger(status); // 0 : vote - 1 : can't vote - 2 : you need the vote badge
            messageBuffer.AppendInteger(user.DailyCompetitionVotes);

            return messageBuffer;
        }
    }
}

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
using System.Data;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Rooms.Competitions.Composers;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Rooms.Competitions.Models
{
    /// <summary>
    ///     Class RoomCompetition.
    /// </summary>
    internal class RoomCompetition
    {
        /// <summary>
        ///     Competition Entries
        /// </summary>
        internal Dictionary<uint, RoomData> Entries;

        /// <summary>
        ///     Competition Id
        /// </summary>
        internal int Id;

        /// <summary>
        ///     Competition Name
        /// </summary>
        internal string Name;

        /// <summary>
        ///     Required Furniture Ids
        /// </summary>
        internal string[] RequiredFurnis;

        public RoomCompetition(int id, string name, string requiredFurnis)
        {
            Id = id;
            Name = name;
            RequiredFurnis = requiredFurnis.Split(';');

            Entries = new Dictionary<uint, RoomData>();

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM rooms_competitions_entries WHERE competition_id = @competition_id");
				queryReactor.AddParameter ("@competition_id", Id);

                DataTable table = queryReactor.GetTable();

                if (table == null)
                    return;

                foreach (DataRow row in table.Rows)
                {
                    uint roomId = (uint)row["room_id"];

                    RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId);

                    if (roomData == null)
                        return;

                    roomData.CompetitionStatus = (int)row["status"];
                    roomData.CompetitionVotes = (int)row["votes"];

                    if (Entries.ContainsKey(roomId))
                        return;

                    Entries.Add(roomId, roomData);
                }
            }
        }

        internal bool HasAllRequiredFurnis(Room room)
        {
            if (room == null)
                return false;

            return RequiredFurnis.All(furni => room.GetRoomItemHandler().HasFurniByItemName(furni));
        }

        internal SimpleServerMessageBuffer AppendEntrySubmitMessage(SimpleServerMessageBuffer messageBuffer, int status, Room room = null) => RoomCompetitionEntrySubmitResultComposer.Compose(this, messageBuffer, status, room);

        internal SimpleServerMessageBuffer AppendVoteMessage(SimpleServerMessageBuffer messageBuffer, Habbo user, int status = 0) => RoomCompetitionVotingInfoComposer.Compose(this, messageBuffer, user, status);
    }

}

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

using System.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Rooms.Competitions.Models;

namespace Yupi.Emulator.Game.Rooms.Competitions
{
    /// <summary>
    ///     Class RoomCompetitionManager.
    /// </summary>
    internal class RoomCompetitionManager
    {
        internal RoomCompetition Competition;

        public RoomCompetitionManager()
        {
            RefreshCompetitions();
        }

        public void RefreshCompetitions()
        {
            Competition = null;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM rooms_competitions WHERE enabled = '1' ORDER BY id DESC LIMIT 1");

                DataRow row = queryReactor.GetRow();

                if (row == null)
                    return;

                Competition = new RoomCompetition((int) row["id"], (string) row["name"], (string) row["required_furnis"]);
            }
        }
    }
}
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

using Yupi.Data.Base.Connections;
using Yupi.Data.Base.Sessions.Interfaces;

namespace Yupi.Data.Base
{
    public sealed class DatabaseManager
    {
        private readonly string _connectionStr;
        private readonly string _typer;

        public DatabaseManager(string connectionStr, string connType)
        {
            _connectionStr = connectionStr;
            _typer = connType;
        }

        public IQueryAdapter GetQueryReactor()
        {
            IDatabaseClient databaseClient = new DatabaseConnection(_connectionStr, _typer);

            databaseClient.Connect();
            databaseClient.Prepare();

            return databaseClient.GetQueryReactor();
        }
        
        public void Destroy()
        {
            // Nothing Implemented
        }
    }
}
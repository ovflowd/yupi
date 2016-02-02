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
using MySql.Data.MySqlClient;

namespace Yupi.Data.Base.Adapters.Handlers
{
    public class MySqlConnectionHandler
    {
        protected ConnectionState State;

        protected MySqlConnection Connection;

        public MySqlConnectionHandler(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);

            SetState(ConnectionState.Closed);
        }

        public void SetFetching() => SetState(ConnectionState.Fetching);

        public void SetExecuting() => SetState(ConnectionState.Executing);

        public void SetOpened() => SetState(ConnectionState.Open);

        public void SetClosed() => SetState(ConnectionState.Closed);

        public ConnectionState GetState() => SetState(State);

        public MySqlConnection GetConnection() => Connection;

        private ConnectionState SetState(ConnectionState state) => State = CheckState(state) ? state : Connection.State;

        private bool CheckState(ConnectionState state) => state == ConnectionState.Executing || state == ConnectionState.Fetching || state == Connection.State;  
    }
}
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
using Yupi.Emulator.Data.Base.Adapters;
using Yupi.Emulator.Data.Base.Adapters.Handlers;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Data.Base.Clients.Interfaces;

namespace Yupi.Emulator.Data.Base.Clients
{
    class BasicDatabaseClient : IDatabaseClient
    {
        private readonly MySqlConnection _mysqlConnection;
        private readonly IQueryAdapter _adapter;

        public BasicDatabaseClient(MySqlConnectionStringBuilder connectionStr)
        {
            _mysqlConnection = new MySqlConnection(connectionStr.ToString());

            _adapter = new BasicQueryAdapter(this);
        }

        public void Open()
        {
           if(_mysqlConnection.State == ConnectionState.Closed)
                _mysqlConnection.Open();
        }

        public void Close()
        {
            if(_mysqlConnection.State == ConnectionState.Open)
                _mysqlConnection.Close();
        }

        public void Dispose()
        {
            if (_mysqlConnection.State == ConnectionState.Open)
                _mysqlConnection.Close();
        }

        public void ReportDone()
        {
            Dispose();
        }

        public void Connect() => Open();

        public void Disconnect() => Close();

        public IQueryAdapter GetQueryReactor() => _adapter;

        public MySqlConnectionHandler GetConnectionHandler() => null;

        public MySqlCommand CreateCommand() => _mysqlConnection.CreateCommand();

        public bool IsAvailable() => true;
    }
}

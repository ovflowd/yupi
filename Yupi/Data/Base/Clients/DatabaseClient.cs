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
using Yupi.Data.Base.Adapters;
using Yupi.Data.Base.Adapters.Interfaces;

namespace Yupi.Data.Base.Clients
{
    public class DatabaseClient : IDatabaseClient
    {
        private readonly IQueryAdapter _adapter;
        private readonly MySqlConnection _mysqlConnection;

        public DatabaseClient(string connectionStr)
        {
            _mysqlConnection = new MySqlConnection(connectionStr);
            _adapter = new NormalQueryAdapter(this);
        }

        public void Dispose()
        {
            if (_mysqlConnection.State == ConnectionState.Open)
                _mysqlConnection.Close();
        }

        public void Connect() => Open();

        public void Disconnect() => Close();

        public IQueryAdapter GetQueryReactor() => _adapter;

        public bool IsAvailable() => _mysqlConnection.State == ConnectionState.Open;

        public void ReportDone() => Dispose();

        public MySqlCommand CreateNewCommandMySql() => _mysqlConnection.CreateCommand();

        public MySqlTransaction GetTransactionMySql() => _mysqlConnection.BeginTransaction();

        public void Open()
        {
            if (_mysqlConnection.State == ConnectionState.Closed)
                _mysqlConnection.Open();
        }

        public void Close()
        {
            if (_mysqlConnection.State == ConnectionState.Open)
                _mysqlConnection.Close();
        }
    }
}
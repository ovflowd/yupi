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
using Yupi.Data.Base.Sessions;
using Yupi.Data.Base.Sessions.Interfaces;

namespace Yupi.Data.Base.Connections
{
    public class DatabaseConnection : IDatabaseClient
    {
        private readonly MySqlConnection _mysqlConnection;
        private readonly IQueryAdapter _adapter;
        private readonly int _type;

        public DatabaseConnection(string connectionStr, string connType)
        {
            switch (connType.ToLower())
            {
                default: // MySQL
                    _mysqlConnection = new MySqlConnection(connectionStr);
                    _adapter = new NormalQueryReactor(this);
                    _type = 1;
                    break;
            }
        }

        public void Open()
        {
            if (_type == 1 && _mysqlConnection.State == ConnectionState.Closed)
                _mysqlConnection.Open();
        }

        public void Close()
        {
            if (_type == 1 && _mysqlConnection.State == ConnectionState.Open)
                _mysqlConnection.Close();
        }

        public void Dispose()
        {
            switch (_type)
            {
                case 1:
                    if (_mysqlConnection.State == ConnectionState.Open)
                        _mysqlConnection.Close();
                    break;
            }
        }

        public void Connect()
        {
            Open();
        }

        public void Disconnect()
        {
            Close();
        }

        public IQueryAdapter GetQueryReactor()
        {
            return _adapter;
        }

        public bool IsAvailable()
        {
            return false;
        }

        public void Prepare()
        {
        }

        public void ReportDone()
        {
            Dispose();
        }

        public MySqlCommand CreateNewCommandMySql()
        {
            return _mysqlConnection.CreateCommand();
        }

        public MySqlTransaction GetTransactionMySql()
        {
            return _mysqlConnection.BeginTransaction();
        }
    }
}
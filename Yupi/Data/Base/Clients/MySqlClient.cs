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

using System;
using MySql.Data.MySqlClient;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Data.Base.Managers;

namespace Yupi.Data.Base.Clients
{
    public class MySqlClient : IDatabaseClient
    {
        private readonly MySqlConnection _mySqlConnection;

        private IQueryAdapter _info;

        public MySqlClient(ConnectionManager dbManager)
        {
            _mySqlConnection = new MySqlConnection(dbManager.GetConnectionString());
        }

        MySqlCommand IDatabaseClient.CreateNewCommandMySql()
        {
            throw new NotImplementedException();
        }

        MySqlTransaction IDatabaseClient.GetTransactionMySql()
        {
            throw new NotImplementedException();
        }

        void IDatabaseClient.Connect()
        {
            throw new NotImplementedException();
        }

        void IDatabaseClient.Disconnect()
        {
            throw new NotImplementedException();
        }

        IQueryAdapter IDatabaseClient.GetQueryReactor()
        {
            throw new NotImplementedException();
        }

        bool IDatabaseClient.IsAvailable()
        {
            throw new NotImplementedException();
        }

        void IDatabaseClient.ReportDone()
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        public void Connect() => _mySqlConnection.Open();

        public void Disconnect() => _mySqlConnection.Close();

        public void Dispose()
        {
            _info = null;

            Disconnect();
        }

        public MySqlCommand GetNewCommandMySql() => _mySqlConnection.CreateCommand();

        public IQueryAdapter GetQueryReactor() => _info;

        public MySqlTransaction GetTransactionMySql() => _mySqlConnection.BeginTransaction();

        public bool IsAvailable() => _info == null;

        public void ReportDone() => Dispose();

        public MySqlCommand CreateNewCommandMySql() => _mySqlConnection.CreateCommand();
    }
}
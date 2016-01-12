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

using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Data.Base.Clients;
using Yupi.Data.Base.Exceptions;

namespace Yupi.Data.Base.Managers
{
    public class ConnectionManager
    {
        public static bool DbEnabled = true;

        private readonly uint _beginClientAmount;

        private readonly Queue _connections;
        private readonly uint _maxPoolSize;

        private string _connectionString;

        private List<MySqlClient> _databaseClients;

        private DatabaseServerDetails _serverDetails;

        public ConnectionManager(uint maxPoolSize, uint clientAmount)
        {
            if (maxPoolSize < clientAmount)
                throw new DatabaseException("The poolsize can not be larger than the client amount!");

            _beginClientAmount = clientAmount;
            _maxPoolSize = maxPoolSize;

            _connections = new Queue();
        }

        public void Destroy()
        {
            if (_databaseClients == null)
                return;

            foreach (MySqlClient current in _databaseClients)
            {
                if (!current.IsAvailable())
                    current.Dispose();

                current.Disconnect();
            }

            _databaseClients.Clear();
        }

        public string GetConnectionString() => _connectionString;

        public IQueryAdapter GetQueryReactor()
        {
            IDatabaseClient databaseClient = new MySqlClient(this);

            databaseClient.Connect();

            return databaseClient.GetQueryReactor();
        }

        public void FreeConnection(IDatabaseClient dbClient)
        {
            lock (_connections.SyncRoot)
                _connections.Enqueue(dbClient);
        }

        public void Init()
        {
            CreateNewConnectionString();

            _databaseClients = new List<MySqlClient>((int) _maxPoolSize);
        }

        public void SetServerDetails(string host, uint port, string username, string password, string databaseName)
            => _serverDetails = new DatabaseServerDetails(host, port, username, password, databaseName);

        private void CreateNewConnectionString()
        {
            MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = _serverDetails.GetHost(),
                Port = _serverDetails.GetPort(),
                UserID = _serverDetails.GetUserName(),
                Password = _serverDetails.GetPassword(),
                Database = _serverDetails.GetDatabaseName(),
                MinimumPoolSize = _beginClientAmount,
                MaximumPoolSize = _maxPoolSize,
                Pooling = true,
                AllowZeroDateTime = true,
                ConvertZeroDateTime = true,
                DefaultCommandTimeout = 300,
                ConnectionTimeout = 10
            };

            MySqlConnectionStringBuilder mySqlConnectionStringBuilder2 = mySqlConnectionStringBuilder;

            SetConnectionString(mySqlConnectionStringBuilder2.ToString());
        }

        private void SetConnectionString(string connectionString) => _connectionString = connectionString;
    }
}
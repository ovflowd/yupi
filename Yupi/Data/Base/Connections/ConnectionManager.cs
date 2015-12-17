#region

using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Yupi.Data.Base.Exceptions;
using Yupi.Data.Base.Sessions.Interfaces;

#endregion

namespace Yupi.Data.Base.Connections
{
    public class ConnectionManager
    {
        public static bool DbEnabled = true;

        private readonly uint _beginClientAmount;
        private readonly uint _maxPoolSize;

        private readonly Queue _connections;

        private string _connectionString;

        private List<MySqlClient> _databaseClients;

        private DatabaseServer _server;

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

            _databaseClients = new List<MySqlClient>((int)_maxPoolSize);
        }

        public void SetServerDetails(string host, uint port, string username, string password, string databaseName) => _server = new DatabaseServer(host, port, username, password, databaseName);

        private void CreateNewConnectionString()
        {
            MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = _server.GetHost(),
                Port = _server.GetPort(),
                UserID = _server.GetUserName(),
                Password = _server.GetPassword(),
                Database = _server.GetDatabaseName(),
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
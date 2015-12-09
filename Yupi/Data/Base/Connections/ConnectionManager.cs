#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MySql.Data.MySqlClient;
using Yupi.Data.Base.Exceptions;
using Yupi.Data.Base.Sessions.Interfaces;

#endregion

namespace Yupi.Data.Base.Connections
{
    public class ConnectionManager
    {
        public static string DatabaseConnectionType = "MySQL";
        public static bool DbEnabled = true;
        private readonly uint _beginClientAmount;
        private readonly uint _maxPoolSize;
        private readonly Queue _connections;
        private string _connectionString;
        private List<MySqlClient> _databaseClients;
        private bool _isConnected;
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
            var flag = false;
            try
            {
                Monitor.Enter(this, ref flag);
                _isConnected = false;
                if (_databaseClients == null)
                    return;
                foreach (var current in _databaseClients)
                {
                    if (!current.IsAvailable())
                        current.Dispose();
                    current.Disconnect();
                }
                _databaseClients.Clear();
            }
            finally
            {
                if (flag)
                    Monitor.Exit(this);
            }
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public IQueryAdapter GetQueryReactor()
        {
            IDatabaseClient databaseClient = new MySqlClient(this);
            databaseClient.Connect();
            databaseClient.Prepare();
            return databaseClient.GetQueryReactor();
        }

        public void FreeConnection(IDatabaseClient dbClient)
        {
            lock (_connections.SyncRoot)
            {
                _connections.Enqueue(dbClient);
            }
        }

        public void Init()
        {
            try
            {
                CreateNewConnectionString();
                _databaseClients = new List<MySqlClient>(((int)_maxPoolSize));
            }
            catch (MySqlException ex)
            {
                _isConnected = false;
                throw new Exception(string.Format("Could not connect the clients to the database: {0}", ex.Message));
            }
            _isConnected = true;
        }

        public bool IsConnectedToDatabase()
        {
            return _isConnected;
        }

        public bool SetServerDetails(string host, uint port, string username, string password, string databaseName)
        {
            bool result;
            try
            {
                _server = new DatabaseServer(host, port, username, password, databaseName);
                result = true;
            }
            catch (DatabaseException)
            {
                _isConnected = false;
                result = false;
            }
            return result;
        }

        private void CreateNewConnectionString()
        {
            var mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
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
            var mySqlConnectionStringBuilder2 = mySqlConnectionStringBuilder;
            SetConnectionString(mySqlConnectionStringBuilder2.ToString());
        }

        private void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
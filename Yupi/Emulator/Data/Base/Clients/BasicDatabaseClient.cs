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

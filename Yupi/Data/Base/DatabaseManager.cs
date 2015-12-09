#region

using Yupi.Data.Base.Connections;
using Yupi.Data.Base.Sessions.Interfaces;

#endregion

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
        }
    }
}
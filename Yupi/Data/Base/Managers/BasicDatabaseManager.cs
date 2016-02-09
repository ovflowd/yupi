using MySql.Data.MySqlClient;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Data.Base.Clients;
using Yupi.Data.Base.Clients.Interfaces;
using Yupi.Data.Base.Managers.Interfaces;

namespace Yupi.Data.Base.Managers
{
    class BasicDatabaseManager : IDatabaseManager
    {
        public MySqlConnectionStringBuilder GetConnectionStringBuilder() => _serverDetails;

        private readonly MySqlConnectionStringBuilder _serverDetails;

        public BasicDatabaseManager(MySqlConnectionStringBuilder serverDetails)
        {
            _serverDetails = serverDetails;
        }

        public IQueryAdapter GetQueryReactor()
        {
            IDatabaseClient databaseClient = new BasicDatabaseClient(_serverDetails);

            databaseClient.Connect();

            return databaseClient.GetQueryReactor();
        }

        public void Destroy()
        {

        }
    }
}

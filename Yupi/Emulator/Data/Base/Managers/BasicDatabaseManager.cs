using MySql.Data.MySqlClient;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Data.Base.Clients;
using Yupi.Emulator.Data.Base.Clients.Interfaces;
using Yupi.Emulator.Data.Base.Managers.Interfaces;

namespace Yupi.Emulator.Data.Base.Managers
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

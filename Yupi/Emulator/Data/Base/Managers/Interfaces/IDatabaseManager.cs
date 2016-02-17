using MySql.Data.MySqlClient;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Emulator.Data.Base.Managers.Interfaces
{
    interface IDatabaseManager
    {
        MySqlConnectionStringBuilder GetConnectionStringBuilder();

        IQueryAdapter GetQueryReactor();

        void Destroy();
    }
}

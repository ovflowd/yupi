using MySql.Data.MySqlClient;
using Yupi.Data.Base.Adapters.Interfaces;

namespace Yupi.Data.Base.Managers.Interfaces
{
    interface IDatabaseManager
    {
        MySqlConnectionStringBuilder GetConnectionStringBuilder();

        IQueryAdapter GetQueryReactor();

        void Destroy();
    }
}

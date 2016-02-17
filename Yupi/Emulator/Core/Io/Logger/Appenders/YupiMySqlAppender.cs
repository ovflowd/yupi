using log4net.Appender;
using MySql.Data.MySqlClient;

namespace Yupi.Core.Io.Logger.Appenders
{
    public class YupiMysqlAppender : AdoNetAppender
    {
        public YupiMysqlAppender()
        {
            MySqlConnectionStringBuilder stringBuilder = Yupi.GetDatabaseManager().GetConnectionStringBuilder();

            ConnectionType = "MySql.Data.MySqlClient.MySqlConnection, MySql.Data";

            CommandText = "INSERT INTO server_system_logs (date,thread,level,logger,message,exception) VALUES (?log_date, ?thread, ?log_level, ?logger, ?message, ?exception)";

            ConnectionString = $"Server={stringBuilder.Server};Database={stringBuilder.Database};Uid={stringBuilder.UserID};Pwd={stringBuilder.Password};Port={stringBuilder.Port};";
        }
    }
}
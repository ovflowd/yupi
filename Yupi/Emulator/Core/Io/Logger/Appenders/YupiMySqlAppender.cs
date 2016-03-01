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

using log4net.Appender;
using MySql.Data.MySqlClient;

namespace Yupi.Emulator.Core.Io.Logger.Appenders
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
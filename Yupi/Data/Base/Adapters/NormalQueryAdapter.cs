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

using System.Data;
using Yupi.Data.Base.Adapters.Enums;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Data.Base.Adapters.Models;
using Yupi.Data.Base.Clients.Interfaces;

namespace Yupi.Data.Base.Adapters
{
    public class NormalQueryAdapter : BaseQueryAdapter, IQueryAdapter
    {
        public NormalQueryAdapter(IDatabaseClient client)
        {
            Client = client;

            Command = Client?.CreateCommand();
        }

        public void AddParameter(string parameterName, object value)
        {
            lock (Command.Parameters)
            {
                if (!Command.Parameters.Contains(parameterName))
                    Command.Parameters.AddWithValue(parameterName, value);
            }
        }

        public int GetInteger()
        {
            if (!Client.IsAvailable())
                return 0;

            int result;

            int.TryParse(BaseFetchCommand(FetchType.Integer).ToString(), out result);

            return result;
        }

        public DataRow GetRow()
        {
            if (!Client.IsAvailable())
                return null;

            DataRow dataRow = null;

            DataSet dataSet = (DataSet)BaseFetchCommand(FetchType.Row);

            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count == 1)
                dataRow = dataSet.Tables[0].Rows[0];

            return dataRow;
        }

        public string GetString()
        {
            if (!Client.IsAvailable())
                return string.Empty;

            object stringResult = BaseFetchCommand(FetchType.String);

            if (stringResult != null)
                return stringResult.ToString();

            return string.Empty;
        }

        public DataTable GetTable()
        {
            if (!Client.IsAvailable())
                return null;

            DataTable dataTable = (DataTable)BaseFetchCommand(FetchType.Table);

            return dataTable;
        }

        public long InsertQuery()
        {
            if (!Client.IsAvailable())
                return 0L;

            long superLong;

            long.TryParse(BaseExecuteCommand(RunType.Insert).ToString(), out superLong);

            return superLong;
        }

        public void RunFastQuery(string query)
        {
            if (!Client.IsAvailable())
                return;

            SetQuery(query);

            RunQuery();
        }

        public void RunQuery()
        {
            if (!Client.IsAvailable())
                return;

            BaseExecuteCommand(RunType.Normal);
        }

        public void SetQuery(string query)
        {
            lock (Command.Parameters)
                Command.Parameters.Clear();

            Command.CommandText = query;
        }

        public void Dispose() => Command?.Dispose();
    }
}
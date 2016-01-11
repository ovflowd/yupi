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

using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Data.Base.Managers;

namespace Yupi.Data.Base.Adapters
{
    public class NormalQueryAdapter : IQueryAdapter
    {
        protected IDatabaseClient Client;
        protected MySqlCommand CommandMySql;

        public NormalQueryAdapter(IDatabaseClient client)
        {
            CommandMySql = client.CreateNewCommandMySql();
            Client = client;
        }

        private static bool DbEnabled => ConnectionManager.DbEnabled;

        public void AddParameter(string parameterName, object val)  => CommandMySql.Parameters.AddWithValue(parameterName, val);

        public bool FindsResult()
        {
            if (!DbEnabled)
                return false;

            bool hasRows;

            using (MySqlDataReader reader = CommandMySql.ExecuteReader())
                hasRows = reader.HasRows;

            return hasRows;
        }

        public int GetInteger()
        {
            if (!DbEnabled)
                return 0;

            int result = 0;

            object obj2 = CommandMySql.ExecuteScalar();

            if (obj2 != null)
                int.TryParse(obj2.ToString(), out result);

            return result;
        }

        public uint GetUInteger()
        {
            if (!DbEnabled)
                return 0;

            uint result = 0;

            object obj2 = CommandMySql.ExecuteScalar();

            if (obj2 != null)
                uint.TryParse(obj2.ToString(), out result);

            return result;
        }

        public DataRow GetRow()
        {
            if (!DbEnabled)
                return null;

            DataRow row = null;
            DataSet dataSet = new DataSet();

            using (MySqlDataAdapter adapter = new MySqlDataAdapter(CommandMySql))
                adapter.Fill(dataSet);

            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count == 1)
                row = dataSet.Tables[0].Rows[0];

            return row;
        }

        public string GetString()
        {
            if (!DbEnabled)
                return string.Empty;

            object obj = CommandMySql.ExecuteScalar();

            if (obj != null)
                return obj.ToString();

            return string.Empty;
        }

        public DataTable GetTable()
        {
            if (!DbEnabled)
                return null;

            DataTable dataTable = new DataTable();

            using (MySqlDataAdapter adapter = new MySqlDataAdapter(CommandMySql))
                adapter.Fill(dataTable);

            return dataTable;
        }

        public long InsertQuery()
        {
            if (!DbEnabled)
                return 0L;

            CommandMySql.ExecuteScalar();

            return CommandMySql.LastInsertedId;
        }

        public void RunFastQuery(string query)
        {
            if (!DbEnabled)
                return;

            SetQuery(query);
            RunQuery();
        }

        public void RunFastParameterQuery(string query, Dictionary<string, object> parameters)
        {
            if (!DbEnabled)
                return;

            SetQuery(query);

            foreach (KeyValuePair<string, object> parameter in parameters)
                AddParameter(parameter.Key, parameter.Value);

            RunQuery();
        }

        public void RunQuery()
        {
            if (!DbEnabled)
                return;

            CommandMySql.ExecuteNonQuery();
        }

        public void SetQuery(string query)
        {
            CommandMySql.Parameters.Clear();
            CommandMySql.CommandText = query;
        }

        public void Dispose()
        {
            CommandMySql.Dispose();
            Client.ReportDone();
        }

        public void AddParameter(string name, byte[] data)
            => CommandMySql.Parameters.Add(new MySqlParameter(name, MySqlDbType.Blob, data.Length));
    }
}
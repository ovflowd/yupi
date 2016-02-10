using System.Data;
using MySql.Data.MySqlClient;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Data.Base.Clients.Interfaces;

namespace Yupi.Data.Base.Adapters
{
    class BasicQueryAdapter : IQueryAdapter
    {
        protected IDatabaseClient Client;

        protected MySqlCommand Command;

        public BasicQueryAdapter(IDatabaseClient client)
        {
            Client = client;

            Command = client.CreateCommand();
        }

        public void AddParameter(string parameterName, object value)
        {
            //string parameterSafeName = Yupi.FilterInjectionChars(parameterName);

            //object parameterSafeValue = Yupi.FilterInjectionChars(value.ToString());

            if (!Command.Parameters.Contains(parameterName))
                Command.Parameters.AddWithValue(parameterName, value.ToString());
        }

        public void AddParameter(string parameterName, byte[] data)
        {
            //string parameterSafeName = Yupi.FilterInjectionChars(parameterName);

            if (!Command.Parameters.Contains(parameterName))
                Command.Parameters.Add(new MySqlParameter(parameterName, MySqlDbType.Blob, data.Length));
        }

        public int GetInteger()
        {
            if (!Client.IsAvailable())
                return 0;

            int result;

            object integerResult = Command.ExecuteScalar();

            if (integerResult == null)
                return 0;

            int.TryParse(integerResult.ToString(), out result);

            return result;
        }

        public DataRow GetRow()
        {
            if (!Client.IsAvailable())
                return null;

            DataRow dataRow = null;

            DataSet dataSet = new DataSet();

            using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(Command))
                dataAdapter.Fill(dataSet);

            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count == 1)
                dataRow = dataSet.Tables[0].Rows[0];

            return dataRow;
        }

        public string GetString()
        {
            if (!Client.IsAvailable())
                return string.Empty;

            object stringResult = Command.ExecuteScalar();

            if (stringResult != null)
                return stringResult.ToString();

            return string.Empty;
        }

        public DataTable GetTable()
        {
            if (!Client.IsAvailable())
                return null;

            DataTable dataTable = new DataTable();

            using (MySqlDataAdapter adapter = new MySqlDataAdapter(Command))
                adapter.Fill(dataTable);

            return dataTable;
        }

        public long InsertQuery()
        {
            if (!Client.IsAvailable())
                return 0L;

            Command.ExecuteScalar();

            return Command.LastInsertedId;
        }

        public void RunFastQuery(string query)
        {
            if (!Client.IsAvailable())
                return;

            //SetQuery(Yupi.FilterInjectionChars(query));

            SetQuery(query);

            RunQuery();
        }

        public void RunQuery()
        {
            if (!Client.IsAvailable())
                return;

            Command.ExecuteNonQuery();
        }

        public void SetQuery(string query)
        {
            Command.Parameters.Clear();

            Command.CommandText = query;
        }

        public void Dispose()
        {
            Command.Dispose();

            Client.ReportDone();
        }
    }
}

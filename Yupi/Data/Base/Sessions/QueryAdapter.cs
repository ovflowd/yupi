#region

using System;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using Ingres.Client;
using MySql.Data.MySqlClient;
using Npgsql;
using Yupi.Core.Io;
using Yupi.Data.Base.Connections;
using Yupi.Data.Base.Sessions.Interfaces;

#endregion

namespace Yupi.Data.Base.Sessions
{
    public class QueryAdapter : IRegularQueryAdapter
    {
        protected IDatabaseClient Client;
        protected MySqlCommand CommandMySql;
        protected FbCommand CommandFireBird;
        protected IngresCommand CommandIngress;
        protected NpgsqlCommand CommandPgSql;

        public QueryAdapter(IDatabaseClient client)
        {
            Client = client;
        }

        private static bool DbEnabled
        {
            get { return ConnectionManager.DbEnabled; }
        }

        public void AddParameter(string name, byte[] data)
        {
            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                case "firebird":
                    CommandFireBird.Parameters.Add(new FbParameter(name, FbDbType.Text, data.Length));
                    break;

                case "ingres":
                case "ingress":
                    CommandIngress.Parameters.Add(new IngresParameter(name, DbType.String, data.Length));
                    break;

                case "pgsql":
                    CommandPgSql.Parameters.Add(new NpgsqlParameter(name, DbType.String, data.Length));
                    break;

                default: // mySql
                    CommandMySql.Parameters.Add(new MySqlParameter(name, MySqlDbType.Blob, data.Length));
                    break;
            }
        }

        public void AddParameter(string parameterName, object val)
        {
            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                case "firebird":
                    CommandFireBird.Parameters.AddWithValue(parameterName, val);
                    break;

                case "ingres":
                case "ingress":
                    CommandIngress.Parameters.Add(parameterName, val);
                    break;

                case "pgsql":
                    CommandPgSql.Parameters.AddWithValue(parameterName, val);
                    break;

                default: // mySql
                    CommandMySql.Parameters.AddWithValue(parameterName, val);
                    break;
            }
        }

        public bool FindsResult()
        {
            if (!DbEnabled)
                return false;
            var hasRows = false;

            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                case "firebird":
                    try
                    {
                        using (var reader = CommandFireBird.ExecuteReader())
                            hasRows = reader.HasRows;
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandFireBird.CommandText);
                        throw exception;
                    }
                    break;

                case "ingres":
                case "ingress":
                    try
                    {
                        using (var reader = CommandIngress.ExecuteReader())
                            hasRows = reader.HasRows;
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandIngress.CommandText);
                        throw exception;
                    }
                    break;

                case "pgsql":
                    try
                    {
                        using (var reader = CommandPgSql.ExecuteReader())
                            hasRows = reader.HasRows;
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandPgSql.CommandText);
                        throw exception;
                    }
                    break;

                default:
                    try
                    {
                        using (var reader = CommandMySql.ExecuteReader())
                            hasRows = reader.HasRows;
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandMySql.CommandText);
                        throw exception;
                    }
                    break;
            }

            return hasRows;
        }

        public int GetInteger()
        {
            if (!DbEnabled)
                return 0;
            var result = 0;
            try
            {
                var obj2 = CommandMySql.ExecuteScalar();
                if (obj2 != null)
                    int.TryParse(obj2.ToString(), out result);
            }
            catch (Exception exception)
            {
                Writer.LogQueryError(exception, CommandMySql.CommandText);
                throw exception;
            }
            return result;
        }

        public DataRow GetRow()
        {
            if (!DbEnabled)
                return null;

            DataRow row = null;
            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                case "firebird":
                    try
                    {
                        var dataSet = new DataSet();
                        using (var adapter = new FbDataAdapter(CommandFireBird))
                            adapter.Fill(dataSet);
                        if ((dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count == 1))
                            row = dataSet.Tables[0].Rows[0];
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandFireBird.CommandText);
                        throw exception;
                    }
                    break;

                case "ingres":
                case "ingress":
                    try
                    {
                        var dataSet = new DataSet();
                        using (var adapter = new IngresDataAdapter(CommandIngress))
                            adapter.Fill(dataSet);
                        if ((dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count == 1))
                            row = dataSet.Tables[0].Rows[0];
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandIngress.CommandText);
                        throw exception;
                    }
                    break;

                case "pgsql":
                    try
                    {
                        var dataSet = new DataSet();
                        using (var adapter = new NpgsqlDataAdapter(CommandPgSql))
                            adapter.Fill(dataSet);
                        if ((dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count == 1))
                            row = dataSet.Tables[0].Rows[0];
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandPgSql.CommandText);
                        throw exception;
                    }
                    break;

                default:
                    try
                    {
                        var dataSet = new DataSet();
                        using (var adapter = new MySqlDataAdapter(CommandMySql))
                            adapter.Fill(dataSet);
                        if ((dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count == 1))
                            row = dataSet.Tables[0].Rows[0];
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandMySql.CommandText);
                        throw exception;
                    }
                    break;
            }

            return row;
        }

        public string GetString()
        {
            if (!DbEnabled)
                return string.Empty;
            var str = string.Empty;

            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                case "firebird":
                    try
                    {
                        var obj2 = CommandFireBird.ExecuteScalar();
                        if (obj2 != null)
                            str = obj2.ToString();
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandFireBird.CommandText);
                        throw exception;
                    }
                    break;

                case "ingres":
                case "ingress":
                    try
                    {
                        var obj2 = CommandIngress.ExecuteScalar();
                        if (obj2 != null)
                            str = obj2.ToString();
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandIngress.CommandText);
                        throw exception;
                    }
                    break;

                case "pgsql":
                    try
                    {
                        var obj2 = CommandPgSql.ExecuteScalar();
                        if (obj2 != null)
                            str = obj2.ToString();
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandPgSql.CommandText);
                        throw exception;
                    }
                    break;

                default:
                    try
                    {
                        var obj2 = CommandMySql.ExecuteScalar();
                        if (obj2 != null)
                            str = obj2.ToString();
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandMySql.CommandText);
                        throw exception;
                    }
                    break;
            }

            return str;
        }

        public DataTable GetTable()
        {
            var dataTable = new DataTable();
            if (!DbEnabled)
                return dataTable;

            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                case "firebird":
                    try
                    {
                        using (var adapter = new FbDataAdapter(CommandFireBird))
                            adapter.Fill(dataTable);
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandFireBird.CommandText);
                        throw exception;
                    }
                    break;

                case "ingres":
                case "ingress":
                    try
                    {
                        using (var adapter = new IngresDataAdapter(CommandIngress))
                            adapter.Fill(dataTable);
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandIngress.CommandText);
                        throw exception;
                    }
                    break;

                case "pgsql":
                    try
                    {
                        using (var adapter = new NpgsqlDataAdapter(CommandPgSql))
                            adapter.Fill(dataTable);
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandPgSql.CommandText);
                        throw exception;
                    }
                    break;

                default:
                    try
                    {
                        using (var adapter = new MySqlDataAdapter(CommandMySql))
                            adapter.Fill(dataTable);
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandMySql.CommandText);
                        throw exception;
                    }
                    break;
            }
            return dataTable;
        }

        public long InsertQuery()
        {
            if (!DbEnabled)
                return 0L;
            var lastInsertedId = 0L;

            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                case "firebird":
                    try
                    {
                        CommandFireBird.ExecuteScalar();
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandFireBird.CommandText);
                        throw exception;
                    }
                    break;

                case "ingres":
                case "ingress":
                    try
                    {
                        CommandIngress.ExecuteScalar();
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandIngress.CommandText);
                        throw exception;
                    }
                    break;

                case "pgsql":
                    try
                    {
                        CommandPgSql.ExecuteScalar();
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandPgSql.CommandText);
                        throw exception;
                    }
                    break;

                default:
                    try
                    {
                        CommandMySql.ExecuteScalar();
                        lastInsertedId = CommandMySql.LastInsertedId;
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandMySql.CommandText);
                        throw exception;
                    }
                    break;
            }

            return lastInsertedId;
        }

        public void RunFastQuery(string query)
        {
            if (!DbEnabled)
                return;
            SetQuery(query);
            RunQuery();
        }

        public void RunQuery()
        {
            if (!DbEnabled)
                return;

            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                case "firebird":
                case "FireBird":
                    try
                    {
                        CommandFireBird.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandFireBird.CommandText);
                        throw exception;
                    }
                    break;

                case "ingres":
                case "ingress":
                    try
                    {
                        CommandIngress.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandIngress.CommandText);
                        throw exception;
                    }
                    break;

                case "pgsql":
                    try
                    {
                        CommandPgSql.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandPgSql.CommandText);
                        throw exception;
                    }
                    break;

                default:
                    try
                    {
                        CommandMySql.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        Writer.LogQueryError(exception, CommandMySql.CommandText);
                        throw exception;
                    }
                    break;
            }
        }

        public void SetQuery(string query)
        {
            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                case "firebird":
                case "FireBird":
                    CommandFireBird.Parameters.Clear();
                    CommandFireBird.CommandText = query;
                    break;

                case "ingres":
                case "ingress":
                    CommandIngress.Parameters.Clear();
                    CommandIngress.CommandText = query;
                    break;

                case "pgsql":
                    CommandPgSql.Parameters.Clear();
                    CommandPgSql.CommandText = query;
                    break;

                default: // mySql
                    CommandMySql.Parameters.Clear();
                    CommandMySql.CommandText = query;
                    break;
            }
        }
    }
}
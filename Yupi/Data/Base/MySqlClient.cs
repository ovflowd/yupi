#region

using System;
using FirebirdSql.Data.FirebirdClient;
using Ingres.Client;
using MySql.Data.MySqlClient;
using Npgsql;
using Yupi.Data.Base.Connections;
using Yupi.Data.Base.Sessions;
using Yupi.Data.Base.Sessions.Interfaces;

#endregion

namespace Yupi.Data.Base
{
    public class MySqlClient : IDatabaseClient, IDisposable
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly FbConnection _fireBirdConnection;
        private readonly IngresConnection _inGressConnection;
        private readonly NpgsqlConnection _pgSqlConnection;
        private readonly ConnectionManager _dbManager;
        private IQueryAdapter _info;

        public MySqlClient(ConnectionManager dbManager)
        {
            _dbManager = dbManager;

            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                case "pgsql":
                    _pgSqlConnection = new NpgsqlConnection(dbManager.GetConnectionString());
                    break;

                case "ingress":
                case "ingres":
                    _inGressConnection = new IngresConnection(dbManager.GetConnectionString());
                    break;

                case "firebird":
                    _fireBirdConnection = new FbConnection(dbManager.GetConnectionString());
                    break;

                default: // mySql
                    _mySqlConnection = new MySqlConnection(dbManager.GetConnectionString());
                    break;
            }
        }

        public void Connect()
        {
            switch (ConnectionManager.DatabaseConnectionType.ToLower())
            {
                case "pgsql":
                    _pgSqlConnection.Open();
                    break;

                case "ingress":
                case "ingres":
                    _inGressConnection.Open();
                    break;

                case "firebird":
                    _fireBirdConnection.Open();
                    break;

                default: // mySql
                    _mySqlConnection.Open();
                    break;
            }
        }

        public void Disconnect()
        {
            try
            {
                switch (ConnectionManager.DatabaseConnectionType.ToLower())
                {
                    case "pgsql":
                        _pgSqlConnection.Close();
                        break;

                    case "ingress":
                    case "ingres":
                        _inGressConnection.Close();
                        break;

                    case "firebird":
                        _fireBirdConnection.Close();
                        break;

                    default: // mySql
                        _mySqlConnection.Close();
                        break;
                }
            }
            catch
            {
            }
        }

        public void Dispose()
        {
            _info = null;
            Disconnect();
        }

        public MySqlCommand GetNewCommandMySql()
        {
            return _mySqlConnection.CreateCommand();
        }

        public FbCommand GetNewCommandFireBird()
        {
            return _fireBirdConnection.CreateCommand();
        }

        public IngresCommand GetNewCommandIngress()
        {
            return _inGressConnection.CreateCommand();
        }

        public NpgsqlCommand GetNewCommandPgSql()
        {
            return _pgSqlConnection.CreateCommand();
        }

        public IQueryAdapter GetQueryReactor()
        {
            return _info;
        }

        public MySqlTransaction GetTransactionMySql()
        {
            return _mySqlConnection.BeginTransaction();
        }

        public NpgsqlTransaction GetTransactionPgSql()
        {
            return _pgSqlConnection.BeginTransaction();
        }

        public IngresTransaction GetTransactionIngress()
        {
            return _inGressConnection.BeginTransaction();
        }

        public FbTransaction GetTransactionFireBird()
        {
            return _fireBirdConnection.BeginTransaction();
        }

        public bool IsAvailable()
        {
            return _info == null;
        }

        public void Prepare()
        {
            _info = new NormalQueryReactor(this);
        }

        public void ReportDone()
        {
            Dispose();
        }

        public MySqlCommand CreateNewCommandMySql()
        {
            return _mySqlConnection.CreateCommand();
        }

        public FbCommand CreateNewCommandFireBird()
        {
            return _fireBirdConnection.CreateCommand();
        }

        public IngresCommand CreateNewCommandIngress()
        {
            return _inGressConnection.CreateCommand();
        }

        public NpgsqlCommand CreateNewCommandPgSql()
        {
            return _pgSqlConnection.CreateCommand();
        }

        MySqlCommand IDatabaseClient.CreateNewCommandMySql()
        {
            throw new NotImplementedException();
        }

        FbCommand IDatabaseClient.CreateNewCommandFireBird()
        {
            throw new NotImplementedException();
        }

        IngresCommand IDatabaseClient.CreateNewCommandIngress()
        {
            throw new NotImplementedException();
        }

        IngresTransaction IDatabaseClient.GetTransactionIngress()
        {
            throw new NotImplementedException();
        }

        NpgsqlCommand IDatabaseClient.CreateNewCommandPgSql()
        {
            throw new NotImplementedException();
        }

        NpgsqlTransaction IDatabaseClient.GetTransactionPgSql()
        {
            throw new NotImplementedException();
        }

        FbTransaction IDatabaseClient.GetTransactionFireBird()
        {
            throw new NotImplementedException();
        }

        MySqlTransaction IDatabaseClient.GetTransactionMySql()
        {
            throw new NotImplementedException();
        }

        void IDatabaseClient.Connect()
        {
            throw new NotImplementedException();
        }

        void IDatabaseClient.Disconnect()
        {
            throw new NotImplementedException();
        }

        IQueryAdapter IDatabaseClient.GetQueryReactor()
        {
            throw new NotImplementedException();
        }

        bool IDatabaseClient.IsAvailable()
        {
            throw new NotImplementedException();
        }

        void IDatabaseClient.Prepare()
        {
            throw new NotImplementedException();
        }

        void IDatabaseClient.ReportDone()
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
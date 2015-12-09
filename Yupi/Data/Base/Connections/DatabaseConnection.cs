#region

using System.Data;
using FirebirdSql.Data.FirebirdClient;
using Ingres.Client;
using MySql.Data.MySqlClient;
using Npgsql;
using Yupi.Data.Base.Sessions;
using Yupi.Data.Base.Sessions.Interfaces;

#endregion

namespace Yupi.Data.Base.Connections
{
    public class DatabaseConnection : IDatabaseClient
    {
        private readonly MySqlConnection _mysqlConnection;
        private readonly FbConnection _firebirdConnection;
        private readonly IngresConnection _ingressConnection;
        private readonly NpgsqlConnection _pgsqlConnection;
        private readonly IQueryAdapter _adapter;
        private readonly int _type;

        public DatabaseConnection(string connectionStr, string connType)
        {
            switch (connType.ToLower())
            {
                case "pgsql":
                    _pgsqlConnection = new NpgsqlConnection(connectionStr);
                    _adapter = new NormalQueryReactor(this);
                    _type = 4;
                    break;

                case "ingress":
                case "ingres":
                    _ingressConnection = new IngresConnection(connectionStr);
                    _adapter = new NormalQueryReactor(this);
                    _type = 3;
                    break;

                case "firebird":
                    _firebirdConnection = new FbConnection(connectionStr);
                    _adapter = new NormalQueryReactor(this);
                    _type = 2;
                    break;

                default: // mySql
                    _mysqlConnection = new MySqlConnection(connectionStr);
                    _adapter = new NormalQueryReactor(this);
                    _type = 1;
                    break;
            }
        }

        public void Open()
        {
            if (_type == 1 && _mysqlConnection.State == ConnectionState.Closed)
                _mysqlConnection.Open();
            else if (_type == 2 && _firebirdConnection.State == ConnectionState.Closed)
                _firebirdConnection.Open();
            else if (_type == 3 && _ingressConnection.State == ConnectionState.Closed)
                _ingressConnection.Open();
            else if (_type == 4 && _pgsqlConnection.State == ConnectionState.Closed)
                _pgsqlConnection.Open();
        }

        public void Close()
        {
            if (_type == 1 && _mysqlConnection.State == ConnectionState.Open)
                _mysqlConnection.Close();
            else if (_type == 2 && _firebirdConnection.State == ConnectionState.Open)
                _firebirdConnection.Close();
            else if (_type == 3 && _ingressConnection.State == ConnectionState.Open)
                _ingressConnection.Close();
            else if (_type == 4 && _pgsqlConnection.State == ConnectionState.Open)
                _pgsqlConnection.Close();
        }

        public void Dispose()
        {
            switch (_type)
            {
                case 1:
                    if (_mysqlConnection.State == ConnectionState.Open)
                    {
                        _mysqlConnection.Close();
                    }
                    break;

                case 2:
                    if (_firebirdConnection.State == ConnectionState.Open)
                    {
                        _firebirdConnection.Close();
                    }
                    _firebirdConnection.Dispose();
                    break;

                case 3:
                    if (_ingressConnection.State == ConnectionState.Open)
                    {
                        _ingressConnection.Close();
                    }
                    _ingressConnection.Dispose();
                    break;

                case 4:
                    if (_pgsqlConnection.State == ConnectionState.Open)
                    {
                        _pgsqlConnection.Close();
                    }
                    _pgsqlConnection.Dispose();
                    break;
            }
        }

        public void Connect()
        {
            Open();
        }

        public void Disconnect()
        {
            Close();
        }

        public IQueryAdapter GetQueryReactor()
        {
            return _adapter;
        }

        public bool IsAvailable()
        {
            return false;
        }

        public void Prepare()
        {
        }

        public void ReportDone()
        {
            Dispose();
        }

        public FbCommand CreateNewCommandFireBird()
        {
            return _firebirdConnection.CreateCommand();
        }

        public IngresCommand CreateNewCommandIngress()
        {
            return _ingressConnection.CreateCommand();
        }

        public NpgsqlCommand CreateNewCommandPgSql()
        {
            return _pgsqlConnection.CreateCommand();
        }

        public MySqlCommand CreateNewCommandMySql()
        {
            return _mysqlConnection.CreateCommand();
        }

        public MySqlTransaction GetTransactionMySql()
        {
            return _mysqlConnection.BeginTransaction();
        }

        public FbTransaction GetTransactionFireBird()
        {
            return _firebirdConnection.BeginTransaction();
        }

        public IngresTransaction GetTransactionIngress()
        {
            return _ingressConnection.BeginTransaction();
        }

        public NpgsqlTransaction GetTransactionPgSql()
        {
            return _pgsqlConnection.BeginTransaction();
        }
    }
}
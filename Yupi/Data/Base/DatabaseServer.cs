#region

using Yupi.Data.Base.Exceptions;

#endregion

namespace Yupi.Data.Base
{
    public class DatabaseServer
    {
        private readonly string _databaseName;
        private readonly string _host;
        private readonly string _passWord;
        private readonly uint _port;
        private readonly string _user;

        public DatabaseServer(string host, uint port, string userName, string passWord, string databaseName)
        {
            if (string.IsNullOrEmpty(host))
                throw new DatabaseException("No host was given");
            if (string.IsNullOrEmpty(userName))
                throw new DatabaseException("No username was given");
            if (string.IsNullOrEmpty(databaseName))
                throw new DatabaseException("No database name was given");

            _host = host;
            _port = port;
            _databaseName = databaseName;
            _user = userName;
            _passWord = string.IsNullOrEmpty(passWord) ? "" : passWord;
        }

        public string GetDatabaseName()
        {
            return _databaseName;
        }

        public string GetHost()
        {
            return _host;
        }

        public string GetPassword()
        {
            return _passWord;
        }

        public uint GetPort()
        {
            return _port;
        }

        public string GetUserName()
        {
            return _user;
        }

        public override string ToString()
        {
            return string.Format("{0}@{1}", _user, _host);
        }
    }
}
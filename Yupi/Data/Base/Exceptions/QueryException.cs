#region

using System;

#endregion

namespace Yupi.Data.Base.Exceptions
{
    public class QueryException : Exception
    {
        private readonly string _query;

        public QueryException(string message, string query) : base(message)
        {
            _query = query;
        }

        public string GetQuery()
        {
            return _query;
        }
    }
}
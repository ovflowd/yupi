#region

using System;

#endregion

namespace Yupi.Data.Base.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message)
        {
        }
    }
}
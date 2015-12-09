#region

using System;

#endregion

namespace Yupi.Data.Base.Exceptions
{
    public class TransactionException : Exception
    {
        public TransactionException(string message) : base(message)
        {
        }
    }
}
using System;

namespace Yupi.Net.Sockets.Exceptions
{
    /// <summary>
    /// Class SocketInitializationException.
    /// </summary>
    internal class SocketInitializationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SocketInitializationException(string message) : base(message)
        {
        }
    }
}
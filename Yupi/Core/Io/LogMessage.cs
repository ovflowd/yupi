namespace Yupi.Core.Io
{
    internal class LogMessage
    {
        internal string Message, Location;

        public LogMessage(string message, string location)
        {
            Message = message;
            Location = location;
        }

        internal void Dispose()
        {
            Message = null;
            Location = null;
        }
    }
}
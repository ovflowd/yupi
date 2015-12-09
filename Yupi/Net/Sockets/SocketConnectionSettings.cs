namespace Yupi.Net.Sockets
{
    /// <summary>
    /// Class SocketConnectionSettings.
    /// </summary>
    public class SocketConnectionSettings
    {
        /// <summary>
        /// The buffer size
        /// </summary>
        public static readonly int BufferSize = 4072; // habbo buffer size (JSON support - camera)

        /// <summary>
        /// The maximum packet size
        /// </summary>
        public static readonly int MaxPacketSize = (BufferSize - 4);
    }
}
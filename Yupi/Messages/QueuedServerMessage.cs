using System.Collections.Generic;
using Yupi.Net.Connection;

namespace Yupi.Messages
{
    /// <summary>
    ///     Class QueuedServerMessage.
    /// </summary>
    public class QueuedServerMessage
    {
        /// <summary>
        ///     The _packet
        /// </summary>
        private readonly List<byte> _packet;

        /// <summary>
        ///     The _user connection
        /// </summary>
        private ConnectionData _userConnection;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QueuedServerMessage" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public QueuedServerMessage(ConnectionData connection)
        {
            _userConnection = connection;
            _packet = new List<byte>();
        }

        /// <summary>
        ///     Gets the get packet.
        /// </summary>
        /// <value>The get packet.</value>
        internal byte[] GetPacket => _packet.ToArray();

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        internal void Dispose()
        {
            _packet.Clear();
            _userConnection = null;
        }

        /// <summary>
        ///     Appends the response.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void AppendResponse(ServerMessage message)
        {
            AppendBytes(message.GetReversedBytes());
        }

        /// <summary>
        ///     Adds the bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        internal void AddBytes(byte[] bytes)
        {
            AppendBytes(bytes);
        }

        /// <summary>
        ///     Sends the response.
        /// </summary>
        internal void SendResponse()
        {
            _userConnection?.SendData(_packet.ToArray());

            Dispose();
        }

        /// <summary>
        ///     Appends the bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        private void AppendBytes(IEnumerable<byte> bytes)
        {
            _packet.AddRange(bytes);
        }
    }
}
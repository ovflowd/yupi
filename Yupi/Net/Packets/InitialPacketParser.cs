using Yupi.Messages.Parsers;

namespace Yupi.Net.Packets
{
    /// <summary>
    /// Class InitialPacketParser.
    /// </summary>
    public class InitialPacketParser : IDataParser
    {
        /// <summary>
        /// Delegate NoParamDelegate
        /// </summary>
        public delegate void NoParamDelegate();

        /// <summary>
        /// Delegate with parameters
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="amountOfBytes">The amount of bytes.</param>
        public delegate void DualParamDelegate(byte[] packet, int amountOfBytes);

        /// <summary>
        /// Occurs when [policy request].
        /// </summary>
        public event NoParamDelegate PolicyRequest;

        /// <summary>
        /// Occurs when [switch parser request].
        /// </summary>
        public event DualParamDelegate SwitchParserRequest;

        /// <summary>
        /// Handles the packet data.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="amountOfBytes">The amount of bytes.</param>
        public void HandlePacketData(byte[] packet, int amountOfBytes)
        {
            if (Yupi.ShutdownStarted)
                return;

            if (packet[0] == 60 && PolicyRequest != null)
                PolicyRequest();
            else if (packet[0] != 67 || SwitchParserRequest == null)
                SwitchParserRequest?.Invoke(packet, amountOfBytes);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            PolicyRequest = null;
            SwitchParserRequest = null;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone() => new InitialPacketParser();
    }
}
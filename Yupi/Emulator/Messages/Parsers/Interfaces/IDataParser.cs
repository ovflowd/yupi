using System;

namespace Yupi.Emulator.Messages.Parsers.Interfaces
{
    /// <summary>
    ///     Interface IDataParser
    /// </summary>
    public interface IDataParser : IDisposable, ICloneable
    {
        /// <summary>
        ///     Handles the packet data.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="bytesReceived"></param>
        void HandlePacketData(byte[] packet, int bytesReceived);
    }
}
using System.Net;

namespace Yupi.NewNet.Settings
{
    internal class ServerFactorySettings
    {
        /// <summary>
        ///     Accepted Connection Count
        /// </summary>
        protected uint AcceptedConnections;

        /// <summary>
        ///     Server Buffer Size
        /// </summary>
        internal int BufferSize;

        /// <summary>
        ///     Worker Threads
        /// </summary>
        internal int WorkerThreads;

        /// <summary>
        ///     Server Port
        /// </summary>
        internal int ServerPort;

        /// <summary>
        ///     Allowed Addresses
        /// </summary>
        internal IPAddress AllowedAddresses;

        /// <summary>
        ///     Server Transport Type
        /// </summary>
        internal TransportType ServerTransportType;

        /// <summary>
        ///     Connection Delay
        /// </summary>
        internal bool ConnectionNoDelay;

        internal ServerFactorySettings(TransportType serverTransportType, IPAddress allowedAddress, int serverPort, int workerThreads, int bufferSize, bool noDelay)
        {
            ServerTransportType = serverTransportType;
            AllowedAddresses = allowedAddress;
            ServerPort = serverPort;
            WorkerThreads = workerThreads;
            BufferSize = bufferSize;
            ConnectionNoDelay = noDelay;
        }

        /// <summary>
        ///     Add Connection Id and Return
        /// </summary>
        internal uint AddConnection() => AcceptedConnections++;

        /// <summary>
        ///     Return Accepted Connections Count
        /// </summary>
        internal uint CountAcceptedConnections() => AcceptedConnections;
    }
}

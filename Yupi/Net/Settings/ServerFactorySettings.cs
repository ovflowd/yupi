using System.Net;

namespace Yupi.Net.Settings
{
    internal class ServerFactorySettings
    {
        /// <summary>
        ///     Accepted Connection Count
        /// </summary>
        protected static uint AcceptedConnections;

        /// <summary>
        ///     Server Buffer Size
        /// </summary>
        internal static int BufferSize;

        /// <summary>
        ///     Worker Threads
        /// </summary>
        internal static int WorkerThreads;

        /// <summary>
        ///     Server Port
        /// </summary>
        internal static int ServerPort;

        /// <summary>
        ///     Allowed Addresses
        /// </summary>
        internal static IPAddress AllowedAddresses;

        /// <summary>
        ///     Server Transport Type
        /// </summary>
        internal static TransportType ServerTransportType;

        /// <summary>
        ///     Connection Delay
        /// </summary>
        internal static bool ConnectionNoDelay;

        internal static void Init(TransportType serverTransportType, IPAddress allowedAddress, int serverPort, int workerThreads, int bufferSize, bool noDelay)
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
        internal static uint AddConnection() => AcceptedConnections++;

        /// <summary>
        ///     Return Accepted Connections Count
        /// </summary>
        internal static uint CountAcceptedConnections() => AcceptedConnections;
    }
}

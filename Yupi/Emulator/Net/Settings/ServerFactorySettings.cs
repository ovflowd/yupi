using System.Net;

namespace Yupi.Emulator.Net.Settings
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
        ///     Connection Delay
        /// </summary>
        internal static bool ConnectionNoDelay;

        internal static void Init(IPAddress allowedAddress, int serverPort, int bufferSize, bool noDelay, int boss, int worker)
        {
            AllowedAddresses = allowedAddress;
            ServerPort = serverPort;
            BufferSize = bufferSize;
            ConnectionNoDelay = noDelay;
            MaxBossSize = boss;
            MaxWorkerSize = worker;
        }

        /// <summary>
        ///    Max Boss Size
        /// </summary>
        internal static int MaxBossSize;

        /// <summary>
        ///     Max Worker Size
        /// </summary>
        internal static int MaxWorkerSize;
    }
}

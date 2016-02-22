using System.Net;

namespace Yupi.Emulator.Net.Settings
{
    internal class ServerFactorySettings
    {
        /// <summary>
        ///     Server Buffer Size
        /// </summary>
        internal static int BufferSize;

        /// <summary>
        ///     Max Connections By Address
        /// </summary>
        internal static int MaxConnectionsByAddress;

        /// <summary>
        ///     Enable Disconnecting when Reach Max Connections Amounts
        /// </summary>
        internal static bool EnableDisconnectWhenReachMaxConnectionsLimit;

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

        internal static void Init(IPAddress allowedAddress, int serverPort, int bufferSize, bool noDelay, int boss, int worker, bool dosAble, int conSize)
        {
            AllowedAddresses = allowedAddress;
            ServerPort = serverPort;
            BufferSize = bufferSize;
            ConnectionNoDelay = noDelay;
            MaxBossSize = boss;
            MaxWorkerSize = worker;
            EnableDisconnectWhenReachMaxConnectionsLimit = dosAble;
            MaxConnectionsByAddress = conSize;
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

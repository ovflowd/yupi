/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

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

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

using System.Collections.Generic;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Net.Settings;

namespace Yupi.Emulator.Net.Connection
{
    class ConnectionSecurity
    {
        /// <summary>
        ///     Server Connections By IP Address
        /// </summary>
        internal static Dictionary<string, uint> ServerClientConnectionsByAddress;

        /// <summary>
        ///     Blocked Client Connections By IP Address
        /// </summary>
        internal static List<string> BlockedClientConnectionsByAddress;

        /// <summary>
        ///     Initialize Security Manager
        /// </summary>
        internal static void Init()
        {
            ServerClientConnectionsByAddress = new Dictionary<string, uint>();

            BlockedClientConnectionsByAddress = new List<string>();
        }

        /// <summary>
        ///     Add new Client to List
        /// </summary>
        internal static void AddNewClient(string clientAddress)
            => ServerClientConnectionsByAddress.Add(clientAddress, 1);

        /// <summary>
        ///     Add Client Count
        /// </summary>
        internal static void AddClientCount(string clientAddress)
        {
            if (ServerClientConnectionsByAddress.ContainsKey(clientAddress))
                ServerClientConnectionsByAddress[clientAddress]++;
        }

        /// <summary>
        ///     Remove Client Count
        /// </summary>
        internal static void RemoveClientCount(string clientAddress)
        {
            if (ServerClientConnectionsByAddress.ContainsKey(clientAddress))
                ServerClientConnectionsByAddress[clientAddress]--;
        }

        /// <summary>
        ///     Get Client Count
        /// </summary>
        internal static uint GetClientCount(string clientAddress)
        {
            if (ServerClientConnectionsByAddress.ContainsKey(clientAddress))
                return ServerClientConnectionsByAddress[clientAddress];

            AddClientCount(clientAddress);

            return 1;
        }

        /// <summary>
        ///     Check Availability of New Connection
        /// </summary>
        internal static bool CheckAvailability(string clientAddress)
        {
            if (BlockedClientConnectionsByAddress.Contains(clientAddress))
                return false;

            if (!ServerFactorySettings.EnableDisconnectWhenReachMaxConnectionsLimit)
                return true;

            if (!ServerClientConnectionsByAddress.ContainsKey(clientAddress))
            {
                AddNewClient(clientAddress);

                return true;
            }

            if (GetClientCount(clientAddress) >= ServerFactorySettings.MaxConnectionsByAddress)
            {
                if (!BlockedClientConnectionsByAddress.Contains(clientAddress))
                {
                    BlockedClientConnectionsByAddress.Add(clientAddress);

                    YupiLogManager.LogWarning($"Connection with Address {clientAddress} is Banned. Due TCP Flooding, will be unbanned after next Restart.", "Yupi.DDoS", false);
                }

                return false;
            }

            AddClientCount(clientAddress);

            return true;
        }
    }
}

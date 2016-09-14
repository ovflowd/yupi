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

namespace Yupi.Net.DotNettyImpl
{
    internal class ConnectionSecurity
    {
        /// <summary>
        ///     Blocked Client Connections By IP Address
        /// </summary>
        private readonly List<string> BlockedClientConnectionsByAddress;

        /// <summary>
        ///     Server Connections By IP Address
        /// </summary>
        private readonly Dictionary<string, uint> ServerClientConnectionsByAddress;

        /// <summary>
        ///     Initialize Security Manager
        /// </summary>
        public ConnectionSecurity()
        {
            ServerClientConnectionsByAddress = new Dictionary<string, uint>();

            BlockedClientConnectionsByAddress = new List<string>();
        }

        /// <summary>
        ///     Add Client
        /// </summary>
        public void AddClient(string clientAddress)
        {
            if (ServerClientConnectionsByAddress.ContainsKey(clientAddress))
                ServerClientConnectionsByAddress[clientAddress]++;
            else ServerClientConnectionsByAddress.Add(clientAddress, 1);
        }

        /// <summary>
        ///     Remove Client
        /// </summary>
        public void RemoveClient(string clientAddress)
        {
            if (ServerClientConnectionsByAddress.ContainsKey(clientAddress))
                ServerClientConnectionsByAddress[clientAddress]--;
        }

        /// <summary>
        ///     Get Client Count
        /// </summary>
        private uint GetClientCount(string clientAddress)
        {
            if (ServerClientConnectionsByAddress.ContainsKey(clientAddress))
                return ServerClientConnectionsByAddress[clientAddress];

            return 0;
        }

        /// <summary>
        ///     Check Availability of New Connection
        /// </summary>
        public bool CheckClient(string clientAddress)
        {
            if (BlockedClientConnectionsByAddress.Contains(clientAddress))
                return false;

            if (!ServerClientConnectionsByAddress.ContainsKey(clientAddress))
            {
                AddClient(clientAddress);

                return true;
            }

            // TODO Reimplement max connections
            /*
            if (GetClientCount(clientAddress) >= ServerFactorySettings.MaxConnectionsByAddress)
            {
                // TODO Drop all connections of this IP ?
                if (!BlockedClientConnectionsByAddress.Contains(clientAddress))
                {
                    BlockedClientConnectionsByAddress.Add(clientAddress);

                    YupiLogManager.LogWarning($"Connection with Address {clientAddress} is Banned. Due TCP Flooding, will be unbanned after next Restart.", "Yupi.DDoS", false);
                }

                return false;
            }
            */
            AddClient(clientAddress);

            return true;
        }
    }
}
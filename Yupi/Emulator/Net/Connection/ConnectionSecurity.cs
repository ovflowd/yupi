using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Net.Settings;

namespace Yupi.Emulator.Net.Connection
{
    class ConnectionSecurity
    {
        internal static Dictionary<string, uint> ServerClientConnectionsByAddress;

        internal static List<string> BlockedClientConnectionsByAddress;

        internal static void Init()
        {
            ServerClientConnectionsByAddress = new Dictionary<string, uint>();

            BlockedClientConnectionsByAddress = new List<string>();
        }

        internal static void AddNewClient(string clientAddress)
            => ServerClientConnectionsByAddress.Add(clientAddress, 1);

        internal static void AddClientCount(string clientAddress)
        {
            if (ServerClientConnectionsByAddress.ContainsKey(clientAddress))
                ServerClientConnectionsByAddress[clientAddress]++;
        }

        internal static void RemoveClientCount(string clientAddress)
        {
            if (ServerClientConnectionsByAddress.ContainsKey(clientAddress))
                ServerClientConnectionsByAddress[clientAddress]--;
        }

        internal static uint GetClientCount(string clientAddress)
        {
            if (ServerClientConnectionsByAddress.ContainsKey(clientAddress))
                return ServerClientConnectionsByAddress[clientAddress];

            AddClientCount(clientAddress);

            return 1;
        }

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

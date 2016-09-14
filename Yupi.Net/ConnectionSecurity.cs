// ---------------------------------------------------------------------------------
// <copyright file="ConnectionSecurity.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Net.DotNettyImpl
{
    using System.Collections.Generic;

    class ConnectionSecurity
    {
        #region Fields

        /// <summary>
        ///     Blocked Client Connections By IP Address
        /// </summary>
        private List<string> BlockedClientConnectionsByAddress;

        /// <summary>
        ///     Server Connections By IP Address
        /// </summary>
        private Dictionary<string, uint> ServerClientConnectionsByAddress;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initialize Security Manager
        /// </summary>
        public ConnectionSecurity()
        {
            ServerClientConnectionsByAddress = new Dictionary<string, uint>();

            BlockedClientConnectionsByAddress = new List<string>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Add Client
        /// </summary>
        public void AddClient(string clientAddress)
        {
            if (ServerClientConnectionsByAddress.ContainsKey(clientAddress))
            {
                ServerClientConnectionsByAddress[clientAddress]++;
            }
            else
            {
                ServerClientConnectionsByAddress.Add(clientAddress, 1);
            }
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

        /// <summary>
        ///     Remove Client
        /// </summary>
        public void RemoveClient(string clientAddress)
        {
            if (ServerClientConnectionsByAddress.ContainsKey(clientAddress))
            {
                // TODO Should never get < 0
                ServerClientConnectionsByAddress[clientAddress]--;
            }
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

        #endregion Methods
    }
}
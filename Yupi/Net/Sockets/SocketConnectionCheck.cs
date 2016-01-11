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

using System;
using System.Linq;
using System.Net.Sockets;
using Yupi.Core.Io;

namespace Yupi.Net.Sockets
{
    /// <summary>
    ///     Class SocketConnectionCheck.
    /// </summary>
    internal class SocketConnectionCheck
    {
        /// <summary>
        ///     The _m connection storage
        /// </summary>
        private static string[] _mConnectionStorage;

        /// <summary>
        ///     The _m last ip blocked
        /// </summary>
        private static string _mLastIpBlocked;

        /// <summary>
        ///     Checks the connection.
        /// </summary>
        /// <param name="sock">The sock.</param>
        /// <param name="maxIpConnectionCount">The maximum ip connection count.</param>
        /// <param name="antiDDosStatus">if set to <c>true</c> [anti d dos status].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal static bool CheckConnection(Socket sock, int maxIpConnectionCount, bool antiDDosStatus)
        {
            if (!antiDDosStatus)
                return true;

            string iP = sock.RemoteEndPoint.ToString().Split(':')[0];

            if (iP == _mLastIpBlocked)
                return false;

            if (GetConnectionAmount(iP) > maxIpConnectionCount)
            {
                Writer.WriteLine(iP + " was banned by Anti-DDoS system.", "Yupi.Security", ConsoleColor.Blue);

                _mLastIpBlocked = iP;

                return false;
            }
            int freeConnectionId = GetFreeConnectionId();

            if (freeConnectionId < 0)
                return false;

            _mConnectionStorage[freeConnectionId] = iP;

            return true;
        }

        /// <summary>
        ///     Frees the connection.
        /// </summary>
        /// <param name="ip">The ip.</param>
        internal static void FreeConnection(string ip)
        {
            for (int i = 0; i < _mConnectionStorage.Length; i++)
                if (_mConnectionStorage[i] == ip)
                    _mConnectionStorage[i] = null;
        }

        /// <summary>
        ///     Gets the connection amount.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns>System.Int32.</returns>
        private static int GetConnectionAmount(string ip) => _mConnectionStorage.Count(t => t == ip);

        /// <summary>
        ///     Gets the free connection identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private static int GetFreeConnectionId()
        {
            for (int i = 0; i < _mConnectionStorage.Length; i++)
                if (_mConnectionStorage[i] == null)
                    return i;
            return -1;
        }


        /// <summary>
        ///     Setups the TCP authorization.
        /// </summary>
        /// <param name="connectionCount">The connection count.</param>
        internal static void SetupTcpAuthorization(int connectionCount)
        {
            _mConnectionStorage = new string[connectionCount];
        }
    }
}
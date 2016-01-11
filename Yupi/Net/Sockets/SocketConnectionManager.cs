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
using System.Net;
using System.Net.Sockets;
using Yupi.Messages.Parsers;
using Yupi.Net.Connection;
using Yupi.Net.Sockets.Exceptions;

namespace Yupi.Net.Sockets
{
    /// <summary>
    ///     Class SocketManager.
    /// </summary>
    public class SocketConnectionManager
    {
        /// <summary>
        ///     A client has connected (nothing has been sent or received yet)
        /// </summary>
        public delegate void OnClientConnectedEvent(ConnectionData connection);

        /// <summary>
        ///     A client has disconnected
        /// </summary>
        public delegate void OnClientDisconnectedEvent(ConnectionData connection, Exception exception);

        /// <summary>
        ///     The _disableNagleAlgorithm in connectios
        /// </summary>
        private bool _disableNagleAlgorithm;

        /// <summary>
        ///     The _connection listener
        /// </summary>
        private TcpListener _listener;

        /// <summary>
        ///     The _parser
        /// </summary>
        private IDataParser _parser;

        /// <summary>
        ///     The port to open socket
        /// </summary>
        private int _portInformation;

        /// <summary>
        ///     Count of accepeted connections
        /// </summary>
        public uint AcceptedConnections;

        /// <summary>
        ///     Gets or sets the maximum connections.
        /// </summary>
        /// <value>The maximum connections.</value>
        public int MaximumConnections { get; set; }

        /// <summary>
        ///     Gets or sets the maximum ip connection count.
        /// </summary>
        /// <value>The maximum ip connection count.</value>
        public int MaxIpConnectionCount { get; set; }

        /// <summary>
        ///     Gets or sets the AntiDDoS Status.
        /// </summary>
        public bool AntiDDosStatus { get; set; }

        public event OnClientConnectedEvent OnClientConnected = delegate { };

        public event OnClientDisconnectedEvent OnClientDisconnected = delegate { };

        /// <summary>
        ///     Initializes the specified port identifier.
        /// </summary>
        /// <param name="portId">The port identifier.</param>
        /// <param name="maxConnections">The maximum connections.</param>
        /// <param name="connectionsPerIp">The connections per ip.</param>
        /// <param name="antiDdoS">The antiDDoS status</param>
        /// <param name="parser">The parser.</param>
        /// <param name="disableNaglesAlgorithm">if set to <c>true</c> [disable nagles algorithm].</param>
        public void Init(int portId, int maxConnections, int connectionsPerIp, bool antiDdoS, IDataParser parser,
            bool disableNaglesAlgorithm)
        {
            _parser = parser;
            _disableNagleAlgorithm = disableNaglesAlgorithm;
            MaximumConnections = maxConnections;
            _portInformation = portId;
            AcceptedConnections = 0;
            MaxIpConnectionCount = connectionsPerIp;
            AntiDDosStatus = antiDdoS;
            if (_portInformation < 0)
                throw new ArgumentOutOfRangeException("port", _portInformation, "Port must be 0 or more.");
            if (_listener != null)
                throw new InvalidOperationException("Already listening.");
            PrepareConnectionDetails();
        }

        /// <summary>
        ///     Prepares the connection details.
        /// </summary>
        /// <exception cref="SocketInitializationException"></exception>
        private void PrepareConnectionDetails()
        {
            try
            {
                SocketConnectionCheck.SetupTcpAuthorization(20000);
                _listener = new TcpListener(IPAddress.Any, _portInformation);
                _listener.Start();
                _listener.BeginAcceptSocket(OnAcceptSocket, _listener);
            }
            catch (SocketException ex)
            {
                throw new SocketInitializationException(ex.Message);
            }
        }

        private void OnChannelDisconnect(ConnectionData connection, Exception exception)
        {
            connection.Disconnected = null;
            OnClientDisconnected(connection, exception);
            connection.Cleanup();
        }

        private void OnAcceptSocket(IAsyncResult ar)
        {
            try
            {
                Socket socket = _listener.EndAcceptSocket(ar);
                if (socket.Connected)
                {
                    if (SocketConnectionCheck.CheckConnection(socket, MaxIpConnectionCount, AntiDDosStatus))
                    {
                        socket.NoDelay = _disableNagleAlgorithm;
                        AcceptedConnections++;

                        ConnectionData connectionInfo = new ConnectionData(socket, _parser.Clone() as IDataParser,
                            AcceptedConnections) {Disconnected = OnChannelDisconnect};
                        OnClientConnected(connectionInfo);
                    }
                }
            }
            catch
            {
                // ignored
            }

            try
            {
                _listener?.BeginAcceptSocket(OnAcceptSocket, _listener);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        public void Destroy()
        {
            _listener.Stop();
        }
    }
}
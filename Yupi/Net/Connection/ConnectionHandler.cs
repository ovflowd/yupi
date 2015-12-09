using System;
using Yupi.Data;
using Yupi.Net.Packets;
using Yupi.Net.Sockets;

namespace Yupi.Net.Connection
{
    /// <summary>
    /// Class ConnectionHandler.
    /// </summary>
    public class ConnectionHandler
    {
        /// <summary>
        /// The manager
        /// </summary>
        public SocketConnectionManager Manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionHandler"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="maxConnections">The maximum connections.</param>
        /// <param name="connectionsPerIp">The connections per ip.</param>
        /// <param name="antiDdoS"></param>
        /// <param name="enabeNagles">if set to <c>true</c> [enabe nagles].</param>
        public ConnectionHandler(int port, int maxConnections, int connectionsPerIp, bool antiDdoS, bool enabeNagles)
        {
            Manager = new SocketConnectionManager();

            Manager.OnClientConnected += OnClientConnected;
            Manager.OnClientDisconnected += OnClientDisconnected;

            Manager.Init(port, maxConnections, connectionsPerIp, antiDdoS, new InitialPacketParser(), !enabeNagles);
        }

        /// <summary>
        /// Managers the connection event.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private static void OnClientConnected(ConnectionData connection)
        {
            try
            {
                Yupi.GetGame().GetClientManager().CreateAndStartClient(connection.GetConnectionId(), connection);
            }
            catch (Exception ex)
            {
                ServerLogManager.HandleException(ex, "Yupi.Configuration.ConnectionHandling");
            }
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="exception"></param>
        private static void OnClientDisconnected(ConnectionData connection, Exception exception)
        {
            try
            {
                Yupi.GetGame().GetClientManager().DisposeConnection(connection.GetConnectionId());
            }
            catch (Exception ex)
            {
                ServerLogManager.HandleException(ex, "Yupi.Configuration.ConnectionHandling");
            }
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        internal void Destroy()
        {
            Manager.Destroy();
        }
    }
}
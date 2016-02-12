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
using Helios.Exceptions;
using Helios.Net;
using Helios.Topology;
using Yupi.Core.Io.Logger;
using Yupi.Messages.Parsers.Interfaces;
using Yupi.Net.Packets;

namespace Yupi.NewNet.Connection
{
    /// <summary>
    ///     Class ConnectionHandler.
    /// </summary>
    public class ConnectionHandler
    {
        /// <summary>
        ///     The manager
        /// </summary>
        public ConnectionManager Manager;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConnectionHandler" /> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="maxConnections">The maximum connections.</param>
        /// <param name="connectionsPerIp">The connections per ip.</param>
        public ConnectionHandler(int port, int maxConnections, int connectionsPerIp)
        {
            Manager = new ConnectionManager();

            Manager.Init(port, maxConnections, connectionsPerIp, new InitialPacketParser());

            Manager.Reactor.OnConnection += OnConnection;

            Manager.Reactor.OnDisconnection += OnDisconnection;

            Manager.Reactor.OnError += OnError;

            Manager.Reactor.OnReceive += OnReceive;

            Manager.Reactor.Start();
        }

        private void OnReceive(NetworkData incomingData, IConnection responseChannel)
        {
            YupiWriterManager.WriteLine("Recebeu...", "Yupi.Net");

            responseChannel.BeginReceive();
        }

        private void OnDisconnection(HeliosConnectionException reason, IConnection closedChannel)
        {
            YupiWriterManager.WriteLine($"Disconnected: {reason}", "Yupi.Net");
        }

        private void OnConnection(INode remoteAddress, IConnection responseChannel)
        {
            YupiWriterManager.WriteLine("Opened Connection", "Yupi.Net");

            Manager.AcceptedConnections++;

            ConnectionData connectionInfo = new ConnectionData(remoteAddress, responseChannel, Manager.DataParser.Clone() as IDataParser, Manager.AcceptedConnections);

            Yupi.GetGame().GetClientManager().CreateAndStartClient(connectionInfo.GetConnectionId(), connectionInfo);
        }

        private void OnError(Exception ex, IConnection connection)
        {
            YupiWriterManager.WriteLine($"Error: {ex}", "Yupi.Net");
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        public void Destroy() => Manager.Destroy();
    }
}
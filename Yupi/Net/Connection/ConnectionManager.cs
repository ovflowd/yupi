using System;
using Helios.Exceptions;
using Helios.Net;
using Helios.Reactor;
using Helios.Reactor.Bootstrap;
using Helios.Topology;
using Yupi.Core.Io.Logger;
using Yupi.Messages.Parsers.Interfaces;
using Yupi.Net.Settings;

namespace Yupi.Net.Connection
{
    internal class ConnectionManager
    {
        /// <summary>
        ///     Connection Reactor
        /// </summary>
        private readonly IReactor _reactor;

        /// <summary>
        ///     Data Parser
        /// </summary>
        public IDataParser DataParser;

        /// <summary>
        ///     Add Connection to Count
        /// </summary>
        public uint AddConnection() => ServerFactorySettings.AddConnection();

        /// <summary>
        ///     Count Accepted Connections
        /// </summary>
        public uint CountAcceptedConnections() => ServerFactorySettings.CountAcceptedConnections();

        public ConnectionManager(IDataParser dataParser)
        {
            DataParser = dataParser;

            IServerFactory serverFactory = new ServerBootstrap()
                .SetTransport(ServerFactorySettings.ServerTransportType)
                .OnConnect(OnConnection)
                .OnDisconnect(OnDisconnection)
                .OnError(OnError)
                .WorkerThreads(ServerFactorySettings.WorkerThreads)
                .BufferSize(ServerFactorySettings.BufferSize)
                .Build();

            INode node = NodeBuilder.BuildNode()
                .Host(ServerFactorySettings.AllowedAddresses)
                .WithPort(ServerFactorySettings.ServerPort);

            _reactor = serverFactory.NewReactor(node);
        }

        public void Stop() => _reactor.Stop();

        public void Start() => _reactor.Start();

        private void OnDisconnection(HeliosConnectionException exception, IConnection closedChannel)
        {
            YupiWriterManager.WriteLine($"Disconnected: {exception}", "Yupi.Net");
        }

        private void OnConnection(INode remoteAddress, IConnection responseChannel)
        {
            YupiWriterManager.WriteLine($"Connected: {remoteAddress.Host}", "Yupi.Net");

            ConnectionHandler currentConnection = new ConnectionHandler(remoteAddress, responseChannel, DataParser.Clone() as IDataParser, AddConnection());

            Console.WriteLine("Handler OK.");

            Yupi.GetGame().GetClientManager().CreateAndStartClient(currentConnection.ConnectionId, currentConnection);

            Console.WriteLine("Client OK.");
        }

        private void OnError(Exception ex, IConnection connection) => YupiWriterManager.WriteLine($"Error: {ex}", "Yupi.Net");

        /// <summary>
        ///     Destroys the Connection Manager
        /// </summary>
        public void Destroy() => _reactor.Stop();
    }
}

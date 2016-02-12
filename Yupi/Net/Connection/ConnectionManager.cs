using System;
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
        ///     Server Settings
        /// </summary>
        private readonly ServerFactorySettings _serverSettings;

        /// <summary>
        ///     Add Connection to Count
        /// </summary>
        public uint AddConnection() => _serverSettings.AddConnection();

        /// <summary>
        ///     Count Accepted Connections
        /// </summary>
        public uint CountAcceptedConnections() => _serverSettings.CountAcceptedConnections();

        public ConnectionManager(IDataParser dataParser, ServerFactorySettings serverSettings)
        {
            DataParser = dataParser;

            _serverSettings = serverSettings;

            IServerFactory serverFactory = new ServerBootstrap()
                .SetTransport(_serverSettings.ServerTransportType)
                .OnConnect(OnConnection)
                .OnDisconnect(OnDisconnection)
                .OnError(OnError)
                .WorkerThreads(_serverSettings.WorkerThreads)
                .BufferSize(_serverSettings.BufferSize)
                .Build();

            INode node = NodeBuilder.BuildNode()
                .Host(_serverSettings.AllowedAddresses)
                .WithPort(_serverSettings.ServerPort);

            _reactor = serverFactory.NewReactor(node);
        }

        public void Stop() => _reactor.Stop();

        public void Start() => _reactor.Start();

        private void OnDisconnection(Exception exception, IConnection closedChannel)
        {
            YupiWriterManager.WriteLine($"Disconnected: {exception}", "Yupi.Net");

            closedChannel.Close();
        }

        private void OnConnection(INode remoteAddress, IConnection responseChannel)
        {
            YupiWriterManager.WriteLine($"Connected: {remoteAddress.Host}", "Yupi.Net");

            ConnectionHandler currentConnection = new ConnectionHandler(remoteAddress, responseChannel, DataParser.Clone() as IDataParser, AddConnection());

            Yupi.GetGame().GetClientManager().CreateAndStartClient(currentConnection.ConnectionId, currentConnection);
        }

        private void OnError(Exception ex, IConnection connection) => YupiWriterManager.WriteLine($"Error: {ex}", "Yupi.Net");

        /// <summary>
        ///     Destroys the Connection Manager
        /// </summary>
        public void Destroy() => _reactor.Stop();
    }
}

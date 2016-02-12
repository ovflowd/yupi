using System;
using Helios.Reactor;
using Helios.Reactor.Bootstrap;
using Helios.Topology;
using Yupi.Messages.Parsers.Interfaces;
using Yupi.Net.Settings;

namespace Yupi.Net.Connection
{
    public class ConnectionManager
    {
        /// <summary>
        ///     Connection Reactor
        /// </summary>
        public static IReactor Reactor;

        /// <summary>
        ///     Data Parser
        /// </summary>
        public static IDataParser DataParser;

        /// <summary>
        ///     Add Connection to Count
        /// </summary>
        public static uint AddConnection() => ServerFactorySettings.AddConnection();

        /// <summary>
        ///     Count Accepted Connections
        /// </summary>
        public static uint CountAcceptedConnections() => ServerFactorySettings.CountAcceptedConnections();

        public static void ServerPrint(INode node, string message) => Console.WriteLine("[{0}] {1}:{2}: {3}", DateTime.UtcNow, node.Host, node.Port, message);

        public static void Init(IDataParser dataParser)
        {
            DataParser = dataParser;

            IServerFactory serverFactory = new ServerBootstrap()
                .SetTransport(ServerFactorySettings.ServerTransportType)
                .WorkerThreads(ServerFactorySettings.WorkerThreads)
                .BufferSize(ServerFactorySettings.BufferSize)
                .Build();

            Reactor = serverFactory.NewReactor(NodeBuilder.BuildNode()
                .Host(ServerFactorySettings.AllowedAddresses)
                .WithPort(ServerFactorySettings.ServerPort));

            Reactor.OnConnection += (node, channel) =>
            {
                ServerPrint(node, $"Accepting connection from... {node.Host}:{node.Port}");

                ConnectionHandler currentConnection = new ConnectionHandler(node, channel, DataParser.Clone() as IDataParser, AddConnection());

                Yupi.GetGame().GetClientManager().CreateAndStartClient(currentConnection.ConnectionId, currentConnection);
            };

            Reactor.OnDisconnection += (reason, address) =>
            {
                ServerPrint(address.RemoteHost, $"Closed connection to... {address.RemoteHost.Host}:{address.RemoteHost.Port} [Reason:{reason.Type}]");
            };
        }

        public static void Stop() => Reactor.Stop();

        public static void Start() => Reactor.Start();
    }
}

using System;
using Helios.Net;
using Helios.Reactor;
using Helios.Reactor.Bootstrap;
using Helios.Topology;
using Helios.Util;
using Yupi.Core.Security;
using Yupi.Messages.Parsers.Interfaces;
using Yupi.Net.Packets;
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
        public static ServerPacketParser DataParser;

        /// <summary>
        ///     Add Connection to Count
        /// </summary>
        public static uint AddConnection() => ServerFactorySettings.AddConnection();

        /// <summary>
        ///     Count Accepted Connections
        /// </summary>
        public static uint CountAcceptedConnections() => ServerFactorySettings.CountAcceptedConnections();

        /// <summary>
        ///     Print Pretty Message
        /// </summary>
        public static void ServerPrint(INode node, string message) => Console.WriteLine("[{0}] {1}:{2}: {3}", DateTime.UtcNow, node.Host, node.Port, message);

        public static void Init(ServerPacketParser dataParser)
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

            Reactor.OnError += OnError;

            Reactor.OnConnection += (node, channel) =>
            {
                ServerPrint(channel.RemoteHost, $"Accepting connection from... {node.Host}:{node.Port}");

                uint currentConnection = AddConnection();

                ConnectionHandler connection = new ConnectionHandler(node, DataParser.Clone() as ServerPacketParser, currentConnection);

                Yupi.GetGame().GetClientManager().CreateAndStartClient(currentConnection, connection);

                connection.StartReceivingInitialData(channel);
            };

            Reactor.OnDisconnection += (reason, channel) =>
            {
                ServerPrint(channel.RemoteHost, $"Closed connection due reason:{reason.Type}]");
            };
        }

        public static void OnError(Exception reason, IConnection channel) => ServerPrint(channel.RemoteHost, $"Error in Connection... [Reason:{reason}]");

        public static void Stop() => Reactor.Stop();

        public static void Start() => Reactor.Start();
    }
}

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Yupi.Emulator.Messages.Parsers;
using Yupi.Emulator.Net.Handlers;
using Yupi.Emulator.Net.Settings;

namespace Yupi.Emulator.Net.Connection
{
    class ConnectionManager
    {
        /// <summary>
        ///     Data Parser
        /// </summary>
        public static ServerPacketParser DataParser;

        /// <summary>
        ///     Server Channel
        /// </summary>
        public static IChannel ServerChannel;

        /// <summary>
        ///     Main Server Worker
        /// </summary>
        public static IEventLoopGroup MainServerWorkers;

        /// <summary>
        ///     Child Server Workers
        /// </summary>
        public static IEventLoopGroup ChildServerWorkers;

        /// <summary>
        ///     Client Connection Actors
        /// </summary>
        public static ConcurrentDictionary<string, ConnectionActor> ClientConnections; 

        public static async Task RunServer()
        {      
            MainServerWorkers = ServerFactorySettings.MaxBossSize == 0 ? new MultithreadEventLoopGroup() : new MultithreadEventLoopGroup(ServerFactorySettings.MaxBossSize);

            ChildServerWorkers = ServerFactorySettings.MaxWorkerSize == 0 ? new MultithreadEventLoopGroup() : new MultithreadEventLoopGroup(ServerFactorySettings.MaxWorkerSize);

            ClientConnections = new ConcurrentDictionary<string, ConnectionActor>();

            try
            {
                ServerBootstrap server = new ServerBootstrap();

                server
                    .Group(MainServerWorkers, ChildServerWorkers)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.AutoRead, true)
                    .Option(ChannelOption.SoBacklog, 100)
                    .Option(ChannelOption.SoKeepalive, true)
                    .Option(ChannelOption.ConnectTimeout, TimeSpan.MaxValue)
                    .Option(ChannelOption.TcpNodelay, ServerFactorySettings.ConnectionNoDelay)
                    .Option(ChannelOption.SoRcvbuf, ServerFactorySettings.BufferSize)
                    .Handler(new LoggingHandler(LogLevel.INFO))
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        string clientAddress = (channel.RemoteAddress as IPEndPoint)?.Address.ToString();

                        if (ConnectionSecurity.CheckAvailability(clientAddress))
                        {
                            channel.Pipeline.AddLast(new ConnectionHandler());

                            ConnectionActor connectionActor = new ConnectionActor(DataParser.Clone() as ServerPacketParser, channel);

                            if (ClientConnections.ContainsKey(connectionActor.IpAddress))
                                connectionActor.HandShakePartialCompleted = true;

                            ClientConnections.AddOrUpdate(connectionActor.IpAddress, connectionActor, (key, value) => connectionActor);
                        }
                    }));

                ServerChannel = await server.BindAsync(ServerFactorySettings.AllowedAddresses, ServerFactorySettings.ServerPort);
            }
            catch
            {
               // ignored
            }     
        }

        public static async void Start() => await RunServer();

        public static async void Stop()
        {
            await ServerChannel.CloseAsync();

            await MainServerWorkers.ShutdownGracefullyAsync();

            await ChildServerWorkers.ShutdownGracefullyAsync();
        }
    }
}

using System;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Yupi.Net.Packets;
using Yupi.Net.Settings;

namespace Yupi.Net.Connection
{
    class NewConnectionManager
    {
        /// <summary>
        ///     Data Parser
        /// </summary>
        public static ServerPacketParser DataParser;

        /// <summary>
        ///     Conneection Server
        /// </summary>
        public static ServerBootstrap Server;

        /// <summary>
        ///     Server Channel
        /// </summary>
        public static IChannel ServerChannel;

        /// <summary>
        ///     Multi Thread Event
        /// </summary>
        public static IEventLoopGroup BossGrop;

        /// <summary>
        ///     Multi Thread Event
        /// </summary>
        public static IEventLoopGroup WorkerGroup;

        public static async void RunServer()
        {
            BossGrop = new MultithreadEventLoopGroup(1);

            WorkerGroup = new MultithreadEventLoopGroup(1);

            Server = new ServerBootstrap();

            Server
                .Group(BossGrop, WorkerGroup)
                .Channel<TcpServerSocketChannel>()
                .Option(ChannelOption.AutoRead, true)
                .Option(ChannelOption.SoBacklog, 100)
                .Option(ChannelOption.SoKeepalive, true)
                .Option(ChannelOption.ConnectTimeout, TimeSpan.MaxValue)
                .Option(ChannelOption.TcpNodelay, NewServerFactorySettings.ConnectionNoDelay)
                .Option(ChannelOption.SoRcvbuf, NewServerFactorySettings.BufferSize)
                .Handler(new LoggingHandler(LogLevel.INFO))
                .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    Console.WriteLine($"Conectado {channel.Id}");

                    channel.Pipeline.AddLast(new NewConnectionHandler());

                    ConnectionActor connection = new ConnectionActor(channel, DataParser.Clone() as ServerPacketParser);

                    Yupi.GetGame().GetClientManager().AddClient(connection.IpAddress.ToString(), connection);
                    
                })).Validate();

            ServerChannel = await Server.BindAsync(NewServerFactorySettings.AllowedAddresses, NewServerFactorySettings.ServerPort);
        }

        public static void Init(ServerPacketParser dataParser)
        {
            DataParser = dataParser;

            RunServer();
        }

        public static void Stop()
        {
            ServerChannel.CloseAsync();

            BossGrop.ShutdownGracefullyAsync();

            WorkerGroup.ShutdownGracefullyAsync();
        }
    }
}

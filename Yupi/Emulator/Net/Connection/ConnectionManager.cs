using System;
using System.Threading.Tasks;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Yupi.Net.Handlers;
using Yupi.Net.Packets;
using Yupi.Net.Settings;

namespace Yupi.Net.Connection
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

        public static IEventLoopGroup MainServerWorkers;

        public static IEventLoopGroup ChildServerWorkers;

        public static async Task RunServer()
        {
            MainServerWorkers = new MultithreadEventLoopGroup(1);

            ChildServerWorkers = new MultithreadEventLoopGroup();

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
                        Console.WriteLine($"Iniciando Conexao ID: {channel.Id}.");

                        channel.Pipeline.AddLast(new ConnectionHandler());

                        ConnectionActor connection = new ConnectionActor(DataParser.Clone() as ServerPacketParser, channel);

                        Yupi.GetGame().GetClientManager().AddClient(channel.Id.ToString(), connection);
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

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
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;
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

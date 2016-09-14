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
using System.Threading.Tasks;

namespace Yupi.Net.DotNettyImpl
{
    internal class ConnectionManager<T> : IServer<T>
    {
        /// <summary>
        ///     Child Server Workers
        /// </summary>
        private IEventLoopGroup ChildServerWorkers;

        private readonly CrossDomainSettings FlashPolicy;

        /// <summary>
        ///     Main Server Worker
        /// </summary>
        private IEventLoopGroup MainServerWorkers;

        /// <summary>
        ///     Server Channel
        /// </summary>
        private IChannel ServerChannel;

        private readonly IServerSettings Settings;

        public ConnectionManager(IServerSettings settings, CrossDomainSettings flashPolicy)
        {
            Settings = settings;
            FlashPolicy = flashPolicy;
        }

        public event MessageReceived<T> OnMessageReceived = delegate { };

        public event ConnectionOpened<T> OnConnectionOpened = delegate { };

        public event ConnectionClosed<T> OnConnectionClosed = delegate { };

        public bool Start()
        {
            MainServerWorkers = Settings.MaxIOThreads == 0
                ? new MultithreadEventLoopGroup()
                : new MultithreadEventLoopGroup(Settings.MaxIOThreads);

            ChildServerWorkers = Settings.MaxWorkingThreads == 0
                ? new MultithreadEventLoopGroup()
                : new MultithreadEventLoopGroup(Settings.MaxWorkingThreads);

            try
            {
                ServerBootstrap server = new ServerBootstrap();

                var headerDecoder = new HeaderDecoder();
                var flashHandler = new FlashPolicyHandler(FlashPolicy);

                server
                    .Group(MainServerWorkers, ChildServerWorkers)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.AutoRead, true)
                    .Option(ChannelOption.SoBacklog, 100)
                    .Option(ChannelOption.SoKeepalive, true)
                    .Option(ChannelOption.ConnectTimeout, TimeSpan.MaxValue)
                    .Option(ChannelOption.TcpNodelay, false)
                    .Option(ChannelOption.SoRcvbuf, Settings.BufferSize)
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        /*
                         * Note: we have to create a new MessageHandler for each 
                         * session because it has stateful properties.
                         */
                        var messageHandler = new MessageHandler<T>(channel, OnMessageReceived, OnConnectionClosed,
                            OnConnectionOpened);
                        channel.Pipeline.AddFirst(flashHandler);
                        channel.Pipeline.AddLast(headerDecoder, messageHandler);
                    }));

                Task<IChannel> task = server.BindAsync(Settings.IP, Settings.Port);
                task.Wait();
                ServerChannel = task.Result;
                return true;
            }
            catch
            {
                // TODO Store/print error
                return false;
            }
        }

        public void Stop()
        {
            DoStop().Wait();
        }

        private async Task DoStop()
        {
            await ServerChannel.CloseAsync();

            await MainServerWorkers.ShutdownGracefullyAsync();

            await ChildServerWorkers.ShutdownGracefullyAsync();
        }
    }
}
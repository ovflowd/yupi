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

namespace Yupi.Net.DotNettyImpl
{
	class ConnectionManager : IServer
	{
		public event MessageReceived OnMessageReceived;

		public event ConnectionOpened OnConnectionOpened;

		public event ConnectionClosed OnConnectionClosed;

		/// <summary>
		///     Server Channel
		/// </summary>
		private IChannel ServerChannel;

		/// <summary>
		///     Main Server Worker
		/// </summary>
		private IEventLoopGroup MainServerWorkers;

		/// <summary>
		///     Child Server Workers
		/// </summary>
		private IEventLoopGroup ChildServerWorkers;

		private IServerSettings Settings;
		private ConnectionSecurity Security;

		public ConnectionManager (IServerSettings settings)
		{
			this.Settings = settings;
			this.Security = new ConnectionSecurity ();
		}
		
		public bool Start()
		{      
			MainServerWorkers = this.Settings.MaxIOThreads == 0 ? new MultithreadEventLoopGroup() : new MultithreadEventLoopGroup(this.Settings.MaxIOThreads);

			ChildServerWorkers = this.Settings.MaxWorkingThreads == 0 ? new MultithreadEventLoopGroup() : new MultithreadEventLoopGroup(this.Settings.MaxWorkingThreads);

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
					.Option(ChannelOption.TcpNodelay, false)
					.Option(ChannelOption.SoRcvbuf, this.Settings.BufferSize)
					.ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
						{
							string clientAddress = (channel.RemoteAddress as IPEndPoint)?.Address.ToString();
							// TODO Move security up (into OnConnect callback)
							if (Security.CheckClient(clientAddress))
							{
								channel.Pipeline.AddLast(new ConnectionHandler(channel));
							}
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
			DoStop ().Wait();
		}

		private async Task DoStop() {
			await ServerChannel.CloseAsync();

			await MainServerWorkers.ShutdownGracefullyAsync();

			await ChildServerWorkers.ShutdownGracefullyAsync();
		}
	}
}

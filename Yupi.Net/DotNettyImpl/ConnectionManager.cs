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
		public event MessageReceived OnMessageReceived = delegate {};

		public event ConnectionOpened OnConnectionOpened = delegate {};

		public event ConnectionClosed OnConnectionClosed = delegate {};

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

		private CrossDomainSettings FlashPolicy;

		public ConnectionManager (IServerSettings settings, CrossDomainSettings flashPolicy)
		{
			this.Settings = settings;
			this.FlashPolicy = flashPolicy;
		}
		
		public bool Start()
		{      
			MainServerWorkers = this.Settings.MaxIOThreads == 0 ? new MultithreadEventLoopGroup() : new MultithreadEventLoopGroup(this.Settings.MaxIOThreads);

			ChildServerWorkers = this.Settings.MaxWorkingThreads == 0 ? new MultithreadEventLoopGroup() : new MultithreadEventLoopGroup(this.Settings.MaxWorkingThreads);

			try
			{
				ServerBootstrap server = new ServerBootstrap();

				HeaderDecoder headerDecoder = new HeaderDecoder();
				FlashPolicyHandler flashHandler = new FlashPolicyHandler(FlashPolicy);

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
							/*
							 * Note: we have to create a new MessageHandler for each 
							 * session because it has stateful properties.
							 */
							MessageHandler messageHandler = new MessageHandler(channel, OnMessageReceived, OnConnectionClosed, OnConnectionOpened);
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
			DoStop ().Wait();
		}

		private async Task DoStop() {
			await ServerChannel.CloseAsync();

			await MainServerWorkers.ShutdownGracefullyAsync();

			await ChildServerWorkers.ShutdownGracefullyAsync();
		}
	}
}

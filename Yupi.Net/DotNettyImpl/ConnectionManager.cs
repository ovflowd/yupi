// ---------------------------------------------------------------------------------
// <copyright file="ConnectionManager.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------


using System;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace Yupi.Net.DotNettyImpl
{
    class ConnectionManager<T> : IServer<T>
    {
        public event MessageReceived<T> OnMessageReceived;

        public event ConnectionOpened<T> OnConnectionOpened;

        public event ConnectionClosed<T> OnConnectionClosed;

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

        public ConnectionManager(IServerSettings settings, CrossDomainSettings flashPolicy)
        {
            OnConnectionClosed = delegate { };
            OnConnectionOpened = delegate { };
            OnMessageReceived = delegate { };

            this.Settings = settings;
            this.FlashPolicy = flashPolicy;
        }

        public bool Start()
        {
            MainServerWorkers = this.Settings.MaxIOThreads == 0
                ? new MultithreadEventLoopGroup()
                : new MultithreadEventLoopGroup(this.Settings.MaxIOThreads);

            ChildServerWorkers = this.Settings.MaxWorkingThreads == 0
                ? new MultithreadEventLoopGroup()
                : new MultithreadEventLoopGroup(this.Settings.MaxWorkingThreads);

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
                        MessageHandler<T> messageHandler = new MessageHandler<T>(channel, OnMessageReceived,
                            OnConnectionClosed, OnConnectionOpened);
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
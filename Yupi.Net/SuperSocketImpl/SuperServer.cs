// ---------------------------------------------------------------------------------
// <copyright file="SuperServer.cs" company="https://github.com/sant0ro/Yupi">
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
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using DotNetty.Transport.Bootstrapping;
using SuperSocket.SocketBase.Logging;

namespace Yupi.Net.SuperSocketImpl
{
    public class SuperServer<T> : AppServer<Session<T>, RequestInfo>, IServer<T>
    {
        public event MessageReceived<T> OnMessageReceived = delegate { };

        public event ConnectionOpened<T> OnConnectionOpened = delegate { };

        public event ConnectionClosed<T> OnConnectionClosed = delegate { };

        private CrossDomainSettings FlashPolicy;

        public SuperServer(IServerSettings settings, CrossDomainSettings flashPolicy)
            : base(new DefaultReceiveFilterFactory<FlashReceiveFilter, RequestInfo>())
        {
            FlashPolicy = flashPolicy;

            IRootConfig rootConfig = CreateRootConfig(settings);

            IServerConfig config = CreateServerConfig(settings);

            Setup(rootConfig, config, logFactory: new Log4NetLogFactory());

            base.NewRequestReceived += HandleRequest;

            base.NewSessionConnected += (Session<T> session) => OnConnectionOpened(session);

            base.SessionClosed += (Session<T> session, CloseReason value) => OnConnectionClosed(session);
        }

        private void HandleRequest(Session<T> session, RequestInfo requestInfo)
        {
            if (requestInfo.IsFlashRequest)
            {
                session.Send(FlashPolicy.GetBytes());
                session.Disconnect();
            }
            else
            {
                OnMessageReceived(session, requestInfo.Body);
            }
        }

        private IServerConfig CreateServerConfig(IServerSettings settings)
        {
            ServerConfig config = new ServerConfig();
            config.Ip = settings.IP;
            config.Port = settings.Port;
            config.ReceiveBufferSize = settings.BufferSize;
            config.SendBufferSize = settings.BufferSize;
            config.ListenBacklog = settings.Backlog;
            config.MaxConnectionNumber = settings.MaxConnections;

            return config;
        }

        private IRootConfig CreateRootConfig(IServerSettings settings)
        {
            RootConfig rootConfig = new RootConfig();
            if (settings.MaxWorkingThreads != 0)
                rootConfig.MaxWorkingThreads = settings.MaxWorkingThreads;

            if (settings.MinWorkingThreads != 0)
                rootConfig.MinWorkingThreads = settings.MinWorkingThreads;

            if (settings.MaxIOThreads != 0)
                rootConfig.MaxCompletionPortThreads = settings.MaxIOThreads;

            if (settings.MinIOThreads != 0)
                rootConfig.MinCompletionPortThreads = settings.MinIOThreads;

            return rootConfig;
        }
    }
}
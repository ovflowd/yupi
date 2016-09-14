// ---------------------------------------------------------------------------------
// <copyright file="MessageHandler.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Net.DotNettyImpl
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using DotNetty.Buffers;
    using DotNetty.Transport.Channels;

    public class MessageHandler<T> : ChannelHandlerAdapter, ISession<T>
    {
        #region Fields

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IChannel Channel;
        private ConnectionClosed<T> OnConnectionClosed;
        private ConnectionOpened<T> OnConnectionOpened;
        private MessageReceived<T> OnMessage;

        #endregion Fields

        #region Properties

        public IPAddress RemoteAddress
        {
            get { return ((IPEndPoint) Channel.RemoteAddress).Address; }
        }

        public T UserData
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public MessageHandler(IChannel channel, MessageReceived<T> onMessage, ConnectionClosed<T> onConnectionClosed,
            ConnectionOpened<T> onConnectionOpened)
        {
            this.Channel = channel;
            this.OnMessage = onMessage;
            this.OnConnectionClosed = onConnectionClosed;
            this.OnConnectionOpened = onConnectionOpened;
        }

        #endregion Constructors

        #region Methods

        public override void ChannelActive(IChannelHandlerContext context)
        {
            OnConnectionOpened(this);
            base.ChannelActive(context);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            OnConnectionClosed(this);
            base.ChannelInactive(context);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            IByteBuffer dataBuffer = message as IByteBuffer;

            byte[] data = new byte[dataBuffer.ReadableBytes];

            dataBuffer.ReadBytes(data);

            OnMessage(this, data);

            dataBuffer.Release();
        }

        public void Disconnect()
        {
            Channel.DisconnectAsync();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Logger.Warn("A networking error occured", exception);
            context.CloseAsync();
        }

        public void Send(byte[] data)
        {
            this.Channel.WriteAndFlushAsync(data);
        }

        public void Send(ArraySegment<byte> data)
        {
            byte[] buffer = new byte[data.Count];
            Array.Copy(data.Array, data.Offset, buffer, 0, data.Count);
            Send(buffer);
        }

        #endregion Methods
    }
}
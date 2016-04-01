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
using System.Net;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace Yupi.Net.DotNettyImpl
{
	public class MessageHandler : ChannelHandlerAdapter, ISession
    {
		private IChannel Channel;
		private MessageReceived OnMessage;
		private ConnectionClosed OnConnectionClosed;
		private ConnectionOpened OnConnectionOpened;

		public IPAddress RemoteAddress {
			get {
				return ((IPEndPoint)Channel.RemoteAddress).Address;
			}
		}

		public MessageHandler (IChannel channel, MessageReceived onMessage, ConnectionClosed onConnectionClosed, ConnectionOpened onConnectionOpened)
		{
			this.Channel = channel;
			this.OnMessage = onMessage;
			this.OnConnectionClosed = onConnectionClosed;
			this.OnConnectionOpened = onConnectionOpened;
		}

		public void Disconnect() {
			Channel.DisconnectAsync ();
		}

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            IByteBuffer dataBuffer = message as IByteBuffer;

			if (dataBuffer.ReadableBytes < 2) {
				// Invalid message (had length, but no id)
				Disconnect();
				return;
			}

			int id = dataBuffer.ReadShort ();

			byte[] data = new byte[dataBuffer.ReadableBytes];

			dataBuffer.ReadBytes (data);

			OnMessage (this, id, data);

			dataBuffer.Release ();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
			context.CloseAsync();
			// TODO Log warning
        }

		public void Send (byte[] data)
		{
			this.Channel.WriteAndFlushAsync (data);
		}

		public void Send (ArraySegment<byte> data)
		{
			byte[] buffer = new byte[data.Count];
			Array.Copy (data.Array, data.Offset, buffer, 0, data.Count);
			Send (buffer);
		}
			
		public override void ChannelActive (IChannelHandlerContext context)
		{
			OnConnectionOpened (this);
			base.ChannelActive (context);
		}

		public override void ChannelInactive (IChannelHandlerContext context)
		{
			OnConnectionClosed (this);
			base.ChannelInactive (context);
		}
    }
}

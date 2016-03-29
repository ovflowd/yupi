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
	public class ConnectionHandler : ChannelHandlerAdapter, ISession
    {
		private IChannel Channel;

		public System.Net.IPAddress RemoteAddress {
			get {
				return (Channel.RemoteAddress as IPEndPoint)?.Address;
			}
		}

		public ConnectionHandler(IChannel channel) {
			this.Channel = channel;
		}

		public void Close() {
			Channel.CloseAsync ();
		}

        public override Task CloseAsync(IChannelHandlerContext context)
        {
            string clientAddress = (context.Channel.RemoteAddress as IPEndPoint)?.Address.ToString();

            if (clientAddress != null)
            {
				/*
				if (client?.GetConnection()?.HandShakeCompleted == true && client.GetConnection()?.ConnectionId == context.Channel?.Id?.ToString())
					client.CompleteDisconnect("Left Game", true);

				ConnectionSecurity.RemoveClient(clientAddress);

                 GetClient().RemoveClient(clientAddress);   
				// TODO Close callback
				*/
            }

            return context.CloseAsync();
        }

		// TODO Should be handled by application logic, not network logic
		/*
        public void ChannelInitialRead(IChannelHandlerContext context, byte[] dataBytes)
        {
			if (dataBytes [0] == 60) {
				WriteAsync (context, CrossDomainSettings.GetXML());
				// TODO CLOSE
			} else if (dataBytes [0] != 67) {
				client?.InitHandler ();
			}

        }*/
		
		// TODO Use callback
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            IByteBuffer dataBuffer = message as IByteBuffer;

            if (dataBuffer != null)
            {
              //  string clientAddress = (context.Channel.RemoteAddress as IPEndPoint)?.Address.ToString();

				/*
                GameClient client = GetClient().GetClientByAddress(clientAddress);

                if (client?.GetConnection() != null)
                {
                    byte[] dataBytes = dataBuffer.ToArray();

                    if (!client.GetConnection().HandShakeCompleted || !client.GetConnection().HandShakePartialCompleted)
                        ChannelInitialRead(context, client, dataBytes);

                    if (!client.GetConnection().HandShakePartialCompleted)
                        client.GetConnection().Close();

                    if (client.GetConnection().HandShakeCompleted)
                        client.GetConnection().DataParser.HandlePacketData(dataBytes, dataBytes.Length);

                    return;
                }*/
            }

            context.WriteAndFlushAsync(message);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
			context.CloseAsync();
			// TODO Log warning
        }

		public override void ChannelReadComplete(IChannelHandlerContext context)
		{
			context.Flush();
		}

		public void Send (byte[] data)
		{
			this.Channel.WriteAsync (data);
		}

		public void Send (ArraySegment<byte> data)
		{
			Send (data);
		}

        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            if (message is IByteBuffer)
                return context.WriteAndFlushAsync(message);

            IByteBuffer buffer = context.Allocator.Buffer().WriteBytes(message as byte[]);

            return context.WriteAndFlushAsync(buffer);
        }

        public override void ChannelRegistered(IChannelHandlerContext context)
        {
			// TODO Callback
			/*
            string clientAddress = (context.Channel.RemoteAddress as IPEndPoint)?.Address.ToString();

            if (clientAddress != null)
            {
                    Yupi.GetGame().GetClientManager()?.AddOrUpdateClient(clientAddress, connectionActor);
            }*/
        }
    }
}

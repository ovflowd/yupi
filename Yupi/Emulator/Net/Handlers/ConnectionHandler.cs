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
using Yupi.Emulator.Core.Security;
using Yupi.Emulator.Game.GameClients;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Net.Connection;

namespace Yupi.Emulator.Net.Handlers
{
    public class ConnectionHandler : ChannelHandlerAdapter
    {
        internal static GameClientManager GetClient() => Yupi.GetGame().GetClientManager();

        public override Task CloseAsync(IChannelHandlerContext context)
        {
            string clientAddress = (context.Channel.RemoteAddress as IPEndPoint)?.Address.ToString();

            if (clientAddress != null)
            {
                GetClient().RemoveClient(clientAddress);

                ConnectionActor connectionActor;

                ConnectionManager.ClientConnections.TryRemove(clientAddress, out connectionActor);
            }

            return context.CloseAsync();
        }

        public override void ChannelUnregistered(IChannelHandlerContext context)
        {
            string clientAddress = (context.Channel.RemoteAddress as IPEndPoint)?.Address.ToString();

            if (clientAddress != null)
            {
                GameClient client = GetClient().GetClientByAddress(clientAddress);

                if (client?.GetConnection()?.HandShakeCompleted == true && client.GetConnection()?.ConnectionId == context.Channel?.Id?.ToString())
                    client.CompleteDisconnect("Left Game", true);

                ConnectionSecurity.RemoveClientCount(clientAddress);
            }
        }

        public void ChannelInitialRead(IChannelHandlerContext context, GameClient client, byte[] dataBytes)
        {
            if (dataBytes[0] == 60)
                WriteAsync(context, CrossDomainSettings.XmlPolicyBytes);
            else if (dataBytes[0] != 67)
                client?.InitHandler();
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            IByteBuffer dataBuffer = message as IByteBuffer;

            if (dataBuffer != null)
            {
                string clientAddress = (context.Channel.RemoteAddress as IPEndPoint)?.Address.ToString();

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
                }
            }

            context.WriteAndFlushAsync(message);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            // ignored.
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            if (message is IByteBuffer)
                return context.WriteAndFlushAsync(message);

            IByteBuffer buffer = context.Allocator.Buffer().WriteBytes(message as byte[]);

            return context.WriteAndFlushAsync(buffer);
        }

        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            string clientAddress = (context.Channel.RemoteAddress as IPEndPoint)?.Address.ToString();

            if (clientAddress != null)
            {
                ConnectionActor connectionActor;

                ConnectionManager.ClientConnections.TryGetValue(clientAddress, out connectionActor);

                if (connectionActor != null)
                {
                    connectionActor.SameHandledCount++;

                    Yupi.GetGame().GetClientManager()?.AddOrUpdateClient(clientAddress, connectionActor);

                    Yupi.GetGame().GetClientManager()?.LogClonesOut(clientAddress, connectionActor.ConnectionId);
                }
            }
        }
    }
}

using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using MySql.Data.MySqlClient.Memcached;
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

            GetClient().GetClientByAddress(clientAddress)?.GetConnection()?.Close();

            return context.CloseAsync();
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            IByteBuffer dataBuffer = message as IByteBuffer;

            string clientAddress = (context.Channel.RemoteAddress as IPEndPoint)?.Address.ToString();

            GameClient clientClient = GetClient().GetClientByAddress(clientAddress);

            if (dataBuffer != null)
            {
                ConnectionActor clientActor = clientClient.GetConnection();

                if (clientActor != null)
                {
                    byte[] dataBytes = dataBuffer.ToArray();

                    if (!clientActor.HandShakeCompleted)
                    {
                        if (dataBytes[0] == 60 && !clientActor.HandShakePartialCompleted)
                        {
                            WriteAsync(context, CrossDomainSettings.XmlPolicyBytes);

                            clientActor.HandShakePartialCompleted = true;

                            return;
                        }

                        if (dataBytes[0] != 67 && clientActor.HandShakePartialCompleted)
                        {
                            clientActor.HandShakeCompleted = true;

                            clientClient.InitHandler();

                            clientActor.DataParser.HandlePacketData(dataBytes, dataBytes.Length);

                            return;
                        }
                    }

                    clientActor.DataParser.HandlePacketData(dataBytes, dataBytes.Length);

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

                Yupi.GetGame().GetClientManager().AddOrUpdateClient(clientAddress, connectionActor);
            }
        }
    }
}

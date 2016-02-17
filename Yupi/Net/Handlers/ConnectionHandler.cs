using System;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Yupi.Core.Security;
using Yupi.Game.GameClients;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Net.Connection;
using Yupi.Net.Packets;

namespace Yupi.Net.Handlers
{
    public class ConnectionHandler : ChannelHandlerAdapter
    {
        internal static GameClientManager GetClient() => Yupi.GetGame().GetClientManager();

        public override Task DisconnectAsync(IChannelHandlerContext context)
        {
            GetClient().RemoveClient(context.Channel.Id.ToString());

            return context.DisconnectAsync();
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            IByteBuffer dataBuffer = message as IByteBuffer;

            ConnectionActor clientActor = GetClient().GetClientByConnectionId(context.Channel.Id.ToString())?.GetConnection();

            Console.WriteLine($"Conexao de ID: {context.Channel.Id} Lendo.");

            if (dataBuffer != null && clientActor != null)
            {
                byte[] dataBytes = dataBuffer.ToArray();

                Console.WriteLine("Eu Li " + dataBuffer.ToString(Encoding.UTF8) + $" Da Conexao de ID {context.Channel.Id}.");

                if (!clientActor.HandShakeCompleted)
                {
                    if (dataBytes[0] == 60 && !clientActor.HandShakePartialCompleted)
                    {
                        WriteAsync(context, CrossDomainSettings.XmlPolicyBytes);

                        clientActor.HandShakePartialCompleted = true;

                        return;
                    }

                    if (dataBytes[0] != 67 && clientActor.HandShakePartialCompleted)
                        clientActor.HandShakeCompleted = true;
                }

                clientActor.DataParser.HandlePacketData(dataBytes, dataBytes.Length);
            }

            context.WriteAndFlushAsync(message);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            //context.CloseAsync();
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            Console.WriteLine($"Conexao de ID: {context.Channel.Id} Leu.");

            context.Flush();
        }

        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            Console.WriteLine($"Conexao de ID: {context.Channel.Id} Escrevendo.");

            if (message is IByteBuffer)
                return context.WriteAndFlushAsync(message);

            IByteBuffer buffer = context.Allocator.Buffer().WriteBytes(message as byte[]);

            return context.WriteAndFlushAsync(buffer);
        }

        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            Console.WriteLine($"Conexao de ID: {context.Channel.Id} Estabelecida.");
        }
    }
}

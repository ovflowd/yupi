using System;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Yupi.Game.GameClients;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Net.Connection
{
    public class NewConnectionHandler : ChannelHandlerAdapter
    {
        internal static GameClientManager GetClient() => Yupi.GetGame().GetClientManager();

        internal static void UpdateChannel(IChannelHandlerContext context) => GetClient().GetClientByAddress(context.Channel.RemoteAddress.ToString()).GetConnection().Channel = context.Channel;

        public override Task DisconnectAsync(IChannelHandlerContext context)
        {
            UpdateChannel(context);

            GetClient().RemoveClient(context.Channel.RemoteAddress.ToString());

            return context.DisconnectAsync();
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            UpdateChannel(context);

            GetClient().GetClientByAddress(context.Channel.RemoteAddress.ToString())?.GetConnection().OnReceive((IByteBuffer) message);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);

            context.CloseAsync();
        }
    }
}

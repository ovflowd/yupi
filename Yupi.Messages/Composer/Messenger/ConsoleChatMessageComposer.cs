using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Messenger
{
	public class ConsoleChatMessageComposer : Yupi.Messages.Contracts.ConsoleChatMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, MessengerMessage msg)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(msg.From);
				message.AppendString(msg.Text);
				message.AppendInteger(msg.Diff().TotalSeconds);
				session.Send (message);
			}
		}
	}
}


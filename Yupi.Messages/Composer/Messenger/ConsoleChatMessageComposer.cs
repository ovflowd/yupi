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
				message.AppendInteger(msg.From.Id);
				message.AppendString(msg.Text);
				message.AppendInteger((int)msg.Diff().TotalSeconds);
				session.Send (message);
			}
		}
	}
}


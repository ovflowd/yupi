using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
	public class ConsoleChatErrorMessageComposer : AbstractComposer<int, uint>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int errorId, uint conversationId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(errorId);
				message.AppendInteger(conversationId);
				message.AppendString(string.Empty);
				session.Send (message);
			}
		}
	}
}


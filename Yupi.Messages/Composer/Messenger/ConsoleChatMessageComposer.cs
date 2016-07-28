using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
	public class ConsoleChatMessageComposer : AbstractComposer<uint, string, int>
	{
		public override void Compose ( Yupi.Protocol.ISender session, uint converstationId, string text, int timeDiff = 0)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(converstationId);
				message.AppendString(text);
				message.AppendInteger(timeDiff);
				session.Send (message);
			}
		}
	}
}


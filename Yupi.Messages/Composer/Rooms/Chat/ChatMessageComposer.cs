using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Chat
{
	public class ChatMessageComposer : AbstractComposer
	{
		// TODO Use enum for color
		public void Compose ( Yupi.Protocol.ISender session, uint entityId, string msg, int color, int count = 0)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(entityId);
				message.AppendString(msg);
				message.AppendInteger(ChatEmotions.GetEmotionsForText(msg));
				message.AppendInteger(color);
				message.AppendInteger(0);
				message.AppendInteger(count);
				session.Send (message);
			}
		}
	}
}


using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Chat
{
	public class ChatMessageComposer : Yupi.Messages.Contracts.ChatMessageComposer
	{
		// TODO Use enum for color
		public override void Compose ( Yupi.Protocol.ISender session, uint entityId, string msg, int color, int count = 0)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(entityId);
				message.AppendString(msg);
				throw new NotImplementedException ();
				//message.AppendInteger(ChatEmotions.GetEmotionsForText(msg));
				message.AppendInteger(color);
				message.AppendInteger(0);
				message.AppendInteger(count);
				session.Send (message);
			}
		}
	}
}


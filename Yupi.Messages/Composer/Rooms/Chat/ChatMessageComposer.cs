using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;


namespace Yupi.Messages.Chat
{
	public class ChatMessageComposer : Yupi.Messages.Contracts.ChatMessageComposer
	{
		private ChatEmotionHelper ChatEmotions;

		public ChatMessageComposer ()
		{
			ChatEmotions = DependencyFactory.Resolve<ChatEmotionHelper> ();
		}

		public override void Compose ( Yupi.Protocol.ISender session, int entityId, string msg, ChatBubbleStyle color, int count = 0)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(entityId);
				message.AppendString(msg);
				message.AppendInteger((int)ChatEmotions.GetEmotionForText(msg));
				message.AppendInteger(color.Value);
				message.AppendInteger(0); // TODO Unknown
				message.AppendInteger(count);
				session.Send (message);
			}
		}
	}
}


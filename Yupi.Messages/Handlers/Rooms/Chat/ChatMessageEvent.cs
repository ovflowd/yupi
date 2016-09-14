using System;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Controller;



namespace Yupi.Messages.Chat
{
	public class ChatMessageEvent : AbstractHandler
	{
		private ChatController Chat;

		public ChatMessageEvent ()
		{
			Chat = DependencyFactory.Resolve<ChatController> ();
		}

		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.RoomEntity == null)
				return;

			string message = request.GetString ();
			int bubbleId = request.GetInteger ();
			int count = request.GetInteger ();

			ChatBubbleStyle bubble;

			if (ChatBubbleStyle.TryFromInt32 (bubbleId, out bubble)) {
				Chat.Chat (session, message, bubble);
			}
		}
	}
}


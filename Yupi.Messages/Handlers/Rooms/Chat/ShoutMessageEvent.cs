using System;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;



namespace Yupi.Messages.Chat
{
	public class ShoutMessageEvent : AbstractHandler
	{
		private ChatController Chat;

		public ShoutMessageEvent ()
		{
			Chat = DependencyFactory.Resolve<ChatController> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.RoomEntity == null)
				return;

			string message = request.GetString ();
			int bubbleId = request.GetInteger ();

			ChatBubbleStyle bubble;

			if (ChatBubbleStyle.TryFromInt32 (bubbleId, out bubble)) {
				Chat.Shout (session, message, bubble);
			}
		}
	}
}


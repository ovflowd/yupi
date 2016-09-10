using System;



namespace Yupi.Messages.Chat
{
	public class ShoutMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.RoomEntity == null)
				return;

			string message = request.GetString ();
			int bubbleId = request.GetInteger ();

			//roomUserByHabbo.Chat(session, msg, true, -1, bubble);
		}
	}
}


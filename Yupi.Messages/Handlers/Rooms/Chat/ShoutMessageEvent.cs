using System;



namespace Yupi.Messages.Chat
{
	public class ShoutMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			if (session.RoomEntity == null)
				return;

			string msg = request.GetString();

			int bubble = request.GetInteger();

			if (!roomUserByHabbo.IsBot)
			{
				// TODO This looks a lot like copy & paste here and in whisper / shout
				if (bubble == 2 || (bubble == 23 && !session.Info.HasPermission("fuse_mod")) || bubble < 0 ||
					bubble > 29)
					bubble = roomUserByHabbo.LastBubble;
			}

			roomUserByHabbo.Chat(session, msg, true, -1, bubble);*/
			throw new NotImplementedException ();
		}
	}
}


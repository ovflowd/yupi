using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Messages.Chat
{
	public class ChatMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			RoomUser roomUser = room?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (roomUser == null)
				return;

			string message = request.GetString();
			int bubble = request.GetInteger();
			int count = request.GetInteger();

			if (!roomUser.IsBot && (bubble == 2 || (bubble == 23 && !session.GetHabbo().HasFuse("fuse_mod")) || bubble < 0 ||
				bubble > 29))
				bubble = roomUser.LastBubble;

			roomUser.Chat(session, message, false, count, bubble);
		}
	}
}


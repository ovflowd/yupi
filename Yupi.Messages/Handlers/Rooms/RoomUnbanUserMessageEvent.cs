using System;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.Rooms
{
	public class RoomUnbanUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint userId = request.GetUInt32();
			uint roomId = request.GetUInt32();
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

			if (room == null)
				return;

			room.Unban(userId);

			router.GetComposer<RoomUnbanUserMessageComposer> ().Compose (session, roomId, userId);
		}
	}
}


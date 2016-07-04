using System;
using Yupi.Emulator.Game.Rooms;
using System.Collections.Generic;

namespace Yupi.Messages.Rooms
{
	public class GetRoomBannedUsersMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint roomId = request.GetUInt32();

			Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

			if (room == null)
				return;

			router.GetComposer<RoomBannedListMessageComposer> ().Compose (session, roomId, room.BannedUsers());
		}
	}
}


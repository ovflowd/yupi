using System;
using Yupi.Emulator.Game.Rooms.Data.Models;

namespace Yupi.Messages.Navigator
{
	public class NavigatorGetFeaturedRoomsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint roomId = request.GetUInt32();

			RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId);

			if (roomData == null)
				return;

			router.GetComposer<OfficialRoomsMessageComposer> ().Compose (session, roomData);
		}
	}
}


using System;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Rooms
{
	public class SetHomeRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint roomId = request.GetUInt32();

			RoomData data = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId);

			if (roomId != 0 && data == null)
			{
				session.GetHabbo().HomeRoom = roomId;

				using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
					queryReactor.RunFastQuery(string.Concat("UPDATE users SET home_room = ", roomId,
						" WHERE id = ", session.GetHabbo().Id));

				router.GetComposer<HomeRoomMessageComposer> ().Compose (session, roomId);
			}
		}
	}
}


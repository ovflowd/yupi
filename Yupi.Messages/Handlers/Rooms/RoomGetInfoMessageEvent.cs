using System;
using Yupi.Emulator.Game.Rooms;
using System.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Rooms
{
	public class RoomGetInfoMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint id = request.GetUInt32();

			// TODO num & num2 ???
			int num = request.GetInteger();
			int num2 = request.GetInteger();

			Room room = Yupi.GetGame().GetRoomManager().LoadRoom(id);

			if (room == null)
				return;

			if (room.RoomData == null)
				return;
			
			bool show = !(num == 0 && num2 == 1);

			room.RoomData.SerializeRoomData(GetResponse(), session, true, null, show);
			SendResponse();

			router.GetComposer<LoadRoomRightsListMessageComposer> ().Compose (session, room);
		}
	}
}


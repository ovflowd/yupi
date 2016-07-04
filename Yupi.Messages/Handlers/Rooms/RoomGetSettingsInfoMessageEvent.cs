using System;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.Rooms
{
	public class RoomGetSettingsInfoMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(request.GetUInt32());
			if (room == null)
				return;

			router.GetComposer<RoomSettingsDataMessageComposer> ().Compose (session, room);
		}
	}
}


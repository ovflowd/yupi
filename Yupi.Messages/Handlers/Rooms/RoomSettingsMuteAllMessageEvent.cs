using System;


namespace Yupi.Messages.Rooms
{
	public class RoomSettingsMuteAllMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room currentRoom = session.GetHabbo().CurrentRoom;

			if (currentRoom == null || !currentRoom.CheckRights(session, true))
				return;

			currentRoom.RoomMuted = !currentRoom.RoomMuted;

			router.GetComposer<RoomMuteStatusMessageComposer> ().Compose (currentRoom, currentRoom.RoomMuted);
		}
	}
}


using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.SoundMachine;

namespace Yupi.Messages.Music
{
	public class GetJukeboxPlaylistsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			if (session.GetHabbo().CurrentRoom == null)
				return;

			Room currentRoom = session.GetHabbo().CurrentRoom;

			if (!currentRoom.GotMusicController())
				return;

			SoundMachineManager roomMusicController = currentRoom.GetRoomMusicController();

			router.GetComposer<JukeboxPlaylistMessageComposer> ().Compose (session, roomMusicController);
		}
	}
}


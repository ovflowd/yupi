using System;



namespace Yupi.Messages.Music
{
	public class GetJukeboxPlaylistsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo().CurrentRoom == null)
				return;

			Yupi.Messages.Rooms currentRoom = session.GetHabbo().CurrentRoom;

			if (!currentRoom.GotMusicController())
				return;

			SoundMachineManager roomMusicController = currentRoom.GetRoomMusicController();

			router.GetComposer<JukeboxPlaylistMessageComposer> ().Compose (session, roomMusicController);
		}
	}
}


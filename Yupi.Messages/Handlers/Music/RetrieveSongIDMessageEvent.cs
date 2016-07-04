using System;
using Yupi.Emulator.Game.SoundMachine;

namespace Yupi.Messages.Music
{
	public class RetrieveSongIDMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			string name = message.GetString();

			uint songId = SoundMachineSongManager.GetSongId(name);

			if (songId != 0) {
				router.GetComposer<RetrieveSongIDMessageComposer> ().Compose (session, name, songId);
			}
		}
	}
}


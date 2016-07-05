using System;


namespace Yupi.Messages.Music
{
	public class RetrieveSongIDMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string name = message.GetString();

			uint songId = SoundMachineSongManager.GetSongId(name);

			if (songId != 0) {
				router.GetComposer<RetrieveSongIDMessageComposer> ().Compose (session, name, songId);
			}
		}
	}
}


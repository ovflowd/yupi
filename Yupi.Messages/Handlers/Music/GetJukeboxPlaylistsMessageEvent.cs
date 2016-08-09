using System;



namespace Yupi.Messages.Music
{
	public class GetJukeboxPlaylistsMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (session.UserData.Room == null)
				return;

			router.GetComposer<JukeboxPlaylistMessageComposer> ().Compose (session, session.UserData.Room.Data.SongMachine);
		}
	}
}


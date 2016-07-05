using System;

namespace Yupi.Messages.Music
{
	public class LoadJukeboxDiscsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo().GetInventoryComponent() == null)
				return;

			if (session.GetHabbo().GetInventoryComponent().SongDisks.Count == 0)
				return;

			router.GetComposer<SongsLibraryMessageComposer> ().Compose (session, session.GetHabbo ().GetInventoryComponent ().SongDisks);
		}
	}
}


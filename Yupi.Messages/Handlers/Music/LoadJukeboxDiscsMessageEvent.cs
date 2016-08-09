using System;
using Yupi.Model.Domain;
using System.Linq;

namespace Yupi.Messages.Music
{
	public class LoadJukeboxDiscsMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			SongItem[] items = session.UserData.Info.Inventory.FloorItems.OfType<SongItem> ().ToArray ();

			router.GetComposer<SongsLibraryMessageComposer> ().Compose (session, items);
		}
	}
}


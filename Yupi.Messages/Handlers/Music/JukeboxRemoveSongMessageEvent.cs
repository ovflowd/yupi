using System;
using Yupi.Model.Domain.Components;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Messages.Contracts;
using System.Linq;

namespace Yupi.Messages.Music
{
	public class JukeboxRemoveSongMessageEvent : AbstractHandler
	{
		private Repository<SongItem> ItemRepository;

		public JukeboxRemoveSongMessageEvent ()
		{
			ItemRepository = DependencyFactory.Resolve<Repository<SongItem>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (session.UserData.Room == null)
				return;

			int itemId = message.GetInteger();

			SongMachineComponent songMachine = session.UserData.Room.Data.SongMachine;

			SongItem item = songMachine.Find (itemId);

			if (item == null) {
				return;
			}

			songMachine.Remove (item);
			session.UserData.Info.Inventory.FloorItems.Add (item);
			ItemRepository.Save (item);

			SongItem[] items = session.UserData.Info.Inventory.FloorItems.OfType<SongItem> ().ToArray ();

			router.GetComposer<UpdateInventoryMessageComposer> ().Compose (session);
			router.GetComposer<SongsLibraryMessageComposer> ().Compose (session, items);
			router.GetComposer<JukeboxPlaylistMessageComposer> ().Compose (session, songMachine);
		}
	}
}


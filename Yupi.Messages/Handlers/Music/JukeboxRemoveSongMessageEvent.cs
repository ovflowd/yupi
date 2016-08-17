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
		private IRepository<SongItem> ItemRepository;

		public JukeboxRemoveSongMessageEvent ()
		{
			ItemRepository = DependencyFactory.Resolve<IRepository<SongItem>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (session.Room == null)
				return;

			int itemId = message.GetInteger();

			SongMachineComponent songMachine = session.Room.Data.SongMachine;

			SongItem item = songMachine.Find (itemId);

			if (item == null) {
				return;
			}

			songMachine.Remove (item);
			session.Info.Inventory.FloorItems.Add (item);
			ItemRepository.Save (item);

			SongItem[] items = session.Info.Inventory.FloorItems.OfType<SongItem> ().ToArray ();

			router.GetComposer<UpdateInventoryMessageComposer> ().Compose (session);
			router.GetComposer<SongsLibraryMessageComposer> ().Compose (session, items);
			router.GetComposer<JukeboxPlaylistMessageComposer> ().Compose (session, songMachine);
		}
	}
}


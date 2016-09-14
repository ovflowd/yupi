using System;
using Yupi.Model.Domain;
using Yupi.Model.Domain.Components;
using Yupi.Messages.Contracts;
using Yupi.Model.Repository;
using Yupi.Model;






namespace Yupi.Messages.Music
{
	public class JukeboxAddPlaylistItemMessageEvent : AbstractHandler
	{
		private IRepository<SongItem> ItemRepository;

		public JukeboxAddPlaylistItemMessageEvent ()
		{
			ItemRepository = DependencyFactory.Resolve<IRepository<SongItem>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (session.Room == null
				|| !session.Room.Data.HasOwnerRights(session.Info)) {
				return;
			}
				
			int itemId = message.GetInteger();

			SongItem item = session.Info.Inventory.GetFloorItem(itemId) as SongItem;

			if (item == null)
				return;

			SongMachineComponent songMachine = session.Room.Data.SongMachine;

			if (songMachine.TryAdd (item)) {
				session.Info.Inventory.FloorItems.Remove (item);
				router.GetComposer<RemoveInventoryObjectMessageComposer> ().Compose (session, item.Id);

				ItemRepository.Save (item);
			}

			router.GetComposer<JukeboxPlaylistMessageComposer> ().Compose (session, songMachine);
		}
	}
}


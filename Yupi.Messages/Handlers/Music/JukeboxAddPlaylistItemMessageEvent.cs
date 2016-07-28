using System;






namespace Yupi.Messages.Music
{
	public class JukeboxAddPlaylistItemMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			// TODO Replace with IsInRoom...
			if (session.GetHabbo ().CurrentRoom == null) {
				return;
			}

			Yupi.Messages.Rooms currentRoom = session.GetHabbo().CurrentRoom;

			if (!currentRoom.CheckRights(session, true))
				return;

			SoundMachineManager roomMusicController = currentRoom.GetRoomMusicController();

			if (roomMusicController.PlaylistSize >= roomMusicController.PlaylistCapacity)
				return;

			uint itemId = message.GetUInt32();

			UserItem item = session.GetHabbo().GetInventoryComponent().GetItem(itemId);

			if (item == null || item.BaseItem.InteractionType != Interaction.MusicDisc)
				return;
			
			SongItem songItem = new SongItem(item);

			int playlistCount = roomMusicController.AddDisk(songItem);

			if (playlistCount < 0)
				return;

			songItem.SaveToDatabase(currentRoom.RoomId);

			session.GetHabbo().GetInventoryComponent().RemoveItem(itemId, true);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("UPDATE items_rooms SET user_id='0' WHERE id=@item LIMIT 1");
				queryReactor.AddParameter ("item", itemId);
				queryReactor.RunQuery ();
			}

			router.GetComposer<JukeboxPlaylistMessageComposer> ().Compose (session, roomMusicController);
		}
	}
}


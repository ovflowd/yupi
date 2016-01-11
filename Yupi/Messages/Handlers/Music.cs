using System.Collections.Generic;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.SoundMachine;
using Yupi.Game.SoundMachine.Composers;
using Yupi.Game.SoundMachine.Songs;
using Yupi.Messages.Parsers;

namespace Yupi.Messages.Handlers
{
    /// <summary>
    ///     Class GameClientMessageHandler.
    /// </summary>
    internal partial class GameClientMessageHandler
    {
        /// <summary>
        ///     Retrieves the song identifier.
        /// </summary>
        internal void RetrieveSongId()
        {
            string text = Request.GetString();

            uint songId = SoundMachineSongManager.GetSongId(text);

            if (songId != 0u)
            {
                ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RetrieveSongIDMessageComposer"));
                serverMessage.AppendString(text);
                serverMessage.AppendInteger(songId);
                Session.SendMessage(serverMessage);
            }
        }

        /// <summary>
        ///     Gets the music data.
        /// </summary>
        internal void GetMusicData()
        {
            int num = Request.GetInteger();

            List<SongData> list = new List<SongData>();

            for (int i = 0; i < num; i++)
            {
                SongData song = SoundMachineSongManager.GetSong(Request.GetUInteger());

                if (song != null)
                    list.Add(song);
            }

            Session.SendMessage(SoundMachineComposer.Compose(list));

            list.Clear();
        }

        /// <summary>
        ///     Adds the playlist item.
        /// </summary>
        internal void AddPlaylistItem()
        {
            if (Session?.GetHabbo() == null || Session.GetHabbo().CurrentRoom == null)
                return;

            Room currentRoom = Session.GetHabbo().CurrentRoom;

            if (!currentRoom.CheckRights(Session, true))
                return;

            SoundMachineManager roomMusicController = currentRoom.GetRoomMusicController();

            if (roomMusicController.PlaylistSize >= roomMusicController.PlaylistCapacity)
                return;

            uint num = Request.GetUInteger();

            UserItem item = Session.GetHabbo().GetInventoryComponent().GetItem(num);

            if (item == null || item.BaseItem.InteractionType != Interaction.MusicDisc)
                return;

            SongItem songItem = new SongItem(item);

            int num2 = roomMusicController.AddDisk(songItem);

            if (num2 < 0)
                return;

            songItem.SaveToDatabase(currentRoom.RoomId);

            Session.GetHabbo().GetInventoryComponent().RemoveItem(num, true);

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery($"UPDATE items_rooms SET user_id='0' WHERE id={num} LIMIT 1");

            Session.SendMessage(SoundMachineComposer.Compose(roomMusicController.PlaylistCapacity,
                roomMusicController.Playlist.Values.ToList()));
        }

        /// <summary>
        ///     Removes the playlist item.
        /// </summary>
        internal void RemovePlaylistItem()
        {
            if (Session?.GetHabbo() == null || Session.GetHabbo().CurrentRoom == null)
                return;

            Room currentRoom = Session.GetHabbo().CurrentRoom;

            if (!currentRoom.GotMusicController())
                return;

            SoundMachineManager roomMusicController = currentRoom.GetRoomMusicController();

            SongItem songItem = roomMusicController.RemoveDisk(Request.GetInteger());

            if (songItem == null)
                return;

            songItem.RemoveFromDatabase();

            Session.GetHabbo()
                .GetInventoryComponent()
                .AddNewItem(songItem.ItemId, songItem.BaseItem.Name, songItem.ExtraData, 0u, false, true, 0, 0,
                    songItem.SongCode);
            Session.GetHabbo().GetInventoryComponent().UpdateItems(false);

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(
                    $"UPDATE items_rooms SET user_id='{Session.GetHabbo().Id}' WHERE id='{songItem.ItemId}' LIMIT 1;");

            Session.SendMessage(
                SoundMachineComposer.SerializeSongInventory(Session.GetHabbo().GetInventoryComponent().SongDisks));
            Session.SendMessage(SoundMachineComposer.Compose(roomMusicController.PlaylistCapacity,
                roomMusicController.Playlist.Values.ToList()));
        }

        /// <summary>
        ///     Gets the disks.
        /// </summary>
        internal void GetDisks()
        {
            if (Session?.GetHabbo() == null || Session.GetHabbo().GetInventoryComponent() == null)
                return;

            if (Session.GetHabbo().GetInventoryComponent().SongDisks.Count == 0)
                return;

            Session.SendMessage(
                SoundMachineComposer.SerializeSongInventory(Session.GetHabbo().GetInventoryComponent().SongDisks));
        }

        /// <summary>
        ///     Gets the Play lists.
        /// </summary>
        internal void GetPlaylists()
        {
            if (Session?.GetHabbo() == null || Session.GetHabbo().CurrentRoom == null)
                return;

            Room currentRoom = Session.GetHabbo().CurrentRoom;

            if (!currentRoom.GotMusicController())
                return;

            SoundMachineManager roomMusicController = currentRoom.GetRoomMusicController();
            Session.SendMessage(SoundMachineComposer.Compose(roomMusicController.PlaylistCapacity,
                roomMusicController.Playlist.Values.ToList()));
        }
    }
}
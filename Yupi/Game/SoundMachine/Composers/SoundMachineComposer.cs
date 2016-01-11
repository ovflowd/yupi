using System.Collections.Generic;
using System.Collections.Specialized;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.SoundMachine.Songs;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.SoundMachine.Composers
{
    /// <summary>
    ///     Class JukeboxComposer.
    /// </summary>
    internal class SoundMachineComposer
    {
        /// <summary>
        ///     Composes the specified songs.
        /// </summary>
        /// <param name="songs">The songs.</param>
        /// <returns>ServerMessage.</returns>
        public static ServerMessage Compose(List<SongData> songs)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("SongsMessageComposer"));
            serverMessage.AppendInteger(songs.Count);

            foreach (SongData current in songs)
            {
                serverMessage.AppendInteger(current.Id);
                serverMessage.AppendString(current.CodeName);
                serverMessage.AppendString(current.Name);
                serverMessage.AppendString(current.Data);
                serverMessage.AppendInteger(current.LengthMiliseconds);
                serverMessage.AppendString(current.Artist);
            }

            return serverMessage;
        }

        /// <summary>
        ///     Composes the playing composer.
        /// </summary>
        /// <param name="songId">The song identifier.</param>
        /// <param name="playlistItemNumber">The playlist item number.</param>
        /// <param name="syncTimestampMs">The synchronize timestamp ms.</param>
        /// <returns>ServerMessage.</returns>
        public static ServerMessage ComposePlayingComposer(uint songId, int playlistItemNumber, int syncTimestampMs)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("JukeboxNowPlayingMessageComposer"));

            if (songId == 0u)
            {
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(0);
            }
            else
            {
                serverMessage.AppendInteger(songId);
                serverMessage.AppendInteger(playlistItemNumber);
                serverMessage.AppendInteger(songId);
                serverMessage.AppendInteger(0);
                serverMessage.AppendInteger(syncTimestampMs);
            }

            return serverMessage;
        }

        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(GameClient session)
            => session.GetHabbo().GetInventoryComponent().SerializeMusicDiscs();

        /// <summary>
        ///     Composes the specified playlist capacity.
        /// </summary>
        /// <param name="playlistCapacity">The playlist capacity.</param>
        /// <param name="playlist">The playlist.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(int playlistCapacity, List<SongInstance> playlist)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("JukeboxPlaylistMessageComposer"));

            serverMessage.AppendInteger(playlistCapacity);
            serverMessage.AppendInteger(playlist.Count);

            foreach (SongInstance current in playlist)
            {
                serverMessage.AppendInteger(current.DiskItem.ItemId);
                serverMessage.AppendInteger(current.SongData.Id);
            }

            return serverMessage;
        }

        /// <summary>
        ///     Composes the specified song identifier.
        /// </summary>
        /// <param name="songId">The song identifier.</param>
        /// <param name="playlistItemNumber">The playlist item number.</param>
        /// <param name="syncTimestampMs">The synchronize timestamp ms.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(uint songId, int playlistItemNumber, int syncTimestampMs)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("JukeboxNowPlayingMessageComposer"));

            if (songId == 0u)
            {
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(0);
            }
            else
            {
                serverMessage.AppendInteger(songId);
                serverMessage.AppendInteger(playlistItemNumber);
                serverMessage.AppendInteger(songId);
                serverMessage.AppendInteger(0);
                serverMessage.AppendInteger(syncTimestampMs);
            }

            return serverMessage;
        }

        /// <summary>
        ///     Serializes the song inventory.
        /// </summary>
        /// <param name="songs">The songs.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage SerializeSongInventory(HybridDictionary songs)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("SongsLibraryMessageComposer"));

            if (songs == null)
            {
                serverMessage.AppendInteger(0);

                return serverMessage;
            }

            serverMessage.StartArray();

            foreach (UserItem userItem in songs.Values)
            {
                if (userItem == null)
                {
                    serverMessage.Clear();
                    continue;
                }

                serverMessage.AppendInteger(userItem.Id);

                SongData song = SoundMachineSongManager.GetSong(userItem.SongCode);
                serverMessage.AppendInteger(song?.Id ?? 0);

                serverMessage.SaveArray();
            }

            serverMessage.EndArray();
            return serverMessage;
        }
    }
}
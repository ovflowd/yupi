using System.Collections.Generic;
using System.Collections.Specialized;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.SoundMachine.Songs;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.SoundMachine.Composers
{
    /// <summary>
    ///     Class JukeboxComposer.
    /// </summary>
     class SoundMachineComposer
    {
        /// <summary>
        ///     Composes the specified songs.
        /// </summary>
        /// <param name="songs">The songs.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
        public static SimpleServerMessageBuffer Compose(List<SongData> songs)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SongsMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(songs.Count);

            foreach (SongData current in songs)
            {
                simpleServerMessageBuffer.AppendInteger(current.Id);
                simpleServerMessageBuffer.AppendString(current.CodeName);
                simpleServerMessageBuffer.AppendString(current.Name);
                simpleServerMessageBuffer.AppendString(current.Data);
                simpleServerMessageBuffer.AppendInteger(current.LengthMiliseconds);
                simpleServerMessageBuffer.AppendString(current.Artist);
            }

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Composes the playing composer.
        /// </summary>
        /// <param name="songId">The song identifier.</param>
        /// <param name="playlistItemNumber">The playlist item number.</param>
        /// <param name="syncTimestampMs">The synchronize timestamp ms.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
        public static SimpleServerMessageBuffer ComposePlayingComposer(uint songId, int playlistItemNumber, int syncTimestampMs)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("JukeboxNowPlayingMessageComposer"));

            if (songId == 0u)
            {
                simpleServerMessageBuffer.AppendInteger(-1);
                simpleServerMessageBuffer.AppendInteger(-1);
                simpleServerMessageBuffer.AppendInteger(-1);
                simpleServerMessageBuffer.AppendInteger(-1);
                simpleServerMessageBuffer.AppendInteger(0);
            }
            else
            {
                simpleServerMessageBuffer.AppendInteger(songId);
                simpleServerMessageBuffer.AppendInteger(playlistItemNumber);
                simpleServerMessageBuffer.AppendInteger(songId);
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendInteger(syncTimestampMs);
            }

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
         static SimpleServerMessageBuffer Compose(GameClient session)
            => session.GetHabbo().GetInventoryComponent().SerializeMusicDiscs();

        /// <summary>
        ///     Composes the specified playlist capacity.
        /// </summary>
        /// <param name="playlistCapacity">The playlist capacity.</param>
        /// <param name="playlist">The playlist.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
         static SimpleServerMessageBuffer Compose(int playlistCapacity, List<SongInstance> playlist)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("JukeboxPlaylistMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(playlistCapacity);
            simpleServerMessageBuffer.AppendInteger(playlist.Count);

            foreach (SongInstance current in playlist)
            {
                simpleServerMessageBuffer.AppendInteger(current.DiskItem.ItemId);
                simpleServerMessageBuffer.AppendInteger(current.SongData.Id);
            }

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Composes the specified song identifier.
        /// </summary>
        /// <param name="songId">The song identifier.</param>
        /// <param name="playlistItemNumber">The playlist item number.</param>
        /// <param name="syncTimestampMs">The synchronize timestamp ms.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
         static SimpleServerMessageBuffer Compose(uint songId, int playlistItemNumber, int syncTimestampMs)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("JukeboxNowPlayingMessageComposer"));

            if (songId == 0u)
            {
                simpleServerMessageBuffer.AppendInteger(-1);
                simpleServerMessageBuffer.AppendInteger(-1);
                simpleServerMessageBuffer.AppendInteger(-1);
                simpleServerMessageBuffer.AppendInteger(-1);
                simpleServerMessageBuffer.AppendInteger(0);
            }
            else
            {
                simpleServerMessageBuffer.AppendInteger(songId);
                simpleServerMessageBuffer.AppendInteger(playlistItemNumber);
                simpleServerMessageBuffer.AppendInteger(songId);
                simpleServerMessageBuffer.AppendInteger(0);
                simpleServerMessageBuffer.AppendInteger(syncTimestampMs);
            }

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Serializes the song inventory.
        /// </summary>
        /// <param name="songs">The songs.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
         static SimpleServerMessageBuffer SerializeSongInventory(HybridDictionary songs)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SongsLibraryMessageComposer"));

            if (songs == null)
            {
                simpleServerMessageBuffer.AppendInteger(0);

                return simpleServerMessageBuffer;
            }

            simpleServerMessageBuffer.StartArray();

            foreach (UserItem userItem in songs.Values)
            {
                if (userItem == null)
                {
                    simpleServerMessageBuffer.Clear();
                    continue;
                }

                simpleServerMessageBuffer.AppendInteger(userItem.Id);

                SongData song = SoundMachineSongManager.GetSong(userItem.SongCode);
                simpleServerMessageBuffer.AppendInteger(song?.Id ?? 0);

                simpleServerMessageBuffer.SaveArray();
            }

            simpleServerMessageBuffer.EndArray();
            return simpleServerMessageBuffer;
        }
    }
}
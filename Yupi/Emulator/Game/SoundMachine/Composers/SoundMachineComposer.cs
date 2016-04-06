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
     public class SoundMachineComposer
    {
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
     public static SimpleServerMessageBuffer Compose(GameClient session)
            => session.GetHabbo().GetInventoryComponent().SerializeMusicDiscs();

        /// <summary>
        ///     Composes the specified song identifier.
        /// </summary>
        /// <param name="songId">The song identifier.</param>
        /// <param name="playlistItemNumber">The playlist item number.</param>
        /// <param name="syncTimestampMs">The synchronize timestamp ms.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public static SimpleServerMessageBuffer Compose(uint songId, int playlistItemNumber, int syncTimestampMs)
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


    }
}
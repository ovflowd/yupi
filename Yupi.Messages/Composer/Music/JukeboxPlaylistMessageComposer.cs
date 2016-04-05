using System;
using Yupi.Emulator.Game.SoundMachine;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.SoundMachine.Songs;

namespace Yupi.Messages.Music
{
	public class JukeboxPlaylistMessageComposer : AbstractComposer<SoundMachineManager>
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, SoundMachineManager jukebox)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				
				message.AppendInteger(jukebox.PlaylistCapacity);

				message.AppendInteger(jukebox.Playlist.Count);

				foreach (SongInstance current in jukebox.Playlist.Values)
				{
					message.AppendInteger(current.DiskItem.ItemId);
					message.AppendInteger(current.SongData.Id);
				}
				session.Send (message);
			}
		}
	}
}


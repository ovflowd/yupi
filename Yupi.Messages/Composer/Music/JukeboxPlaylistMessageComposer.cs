using System;

using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Music
{
	public class JukeboxPlaylistMessageComposer : AbstractComposer<SoundMachineManager>
	{
		public override void Compose (Yupi.Protocol.ISender session, SoundMachineManager jukebox)
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


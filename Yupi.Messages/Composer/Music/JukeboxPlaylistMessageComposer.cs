using System;

using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;


namespace Yupi.Messages.Music
{
	public class JukeboxPlaylistMessageComposer : AbstractComposer<int, IList<SongItem>>
	{
		public override void Compose ( Yupi.Protocol.ISender session, int capacity, IList<SongItem> playlist)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				
				message.AppendInteger(capacity);

				message.AppendInteger(playlist.Count);

				foreach (SongItem current in playlist)
				{
					message.AppendInteger(current.Id);
					message.AppendInteger(current.Song.Id);
				}
				session.Send (message);
			}
		}
	}
}


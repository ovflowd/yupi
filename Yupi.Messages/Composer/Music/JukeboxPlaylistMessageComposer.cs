using System;

using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Domain.Components;


namespace Yupi.Messages.Music
{
	public class JukeboxPlaylistMessageComposer : Yupi.Messages.Contracts.JukeboxPlaylistMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, SongMachineComponent songMachine)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				
				message.AppendInteger(songMachine.Capacity);

				message.AppendInteger(songMachine.SongItems.Count);

				foreach (SongItem current in songMachine.SongItems)
				{
					message.AppendInteger(current.Id);
					message.AppendInteger(current.Song.Id);
				}
				session.Send (message);
			}
		}
	}
}


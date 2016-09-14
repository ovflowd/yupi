using System;
using System.Collections.Specialized;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;



namespace Yupi.Messages.Music
{
	public class SongsLibraryMessageComposer : Yupi.Messages.Contracts.SongsLibraryMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, SongItem[] songs)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {

				message.AppendInteger (songs.Length);
				foreach (SongItem userItem in songs)
				{
					message.AppendInteger(userItem.Id);
					message.AppendInteger(userItem.Song.Id);
				}
				session.Send (message);
			}
		}
	}
}


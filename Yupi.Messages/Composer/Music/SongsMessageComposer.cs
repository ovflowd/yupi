using System;
using System.Collections.Generic;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Music
{
	public class SongsMessageComposer : AbstractComposer<List<SongData>>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, List<SongData> songs)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (songs.Count);


				foreach (SongData current in songs)
				{
					message.AppendInteger(current.Id);
					message.AppendString(current.CodeName);
					message.AppendString(current.Name);
					message.AppendString(current.Data);
					message.AppendInteger(current.LengthMiliseconds);
					message.AppendString(current.Artist);
				}

				session.Send (message);
			}
		}
	}
}


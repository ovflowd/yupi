using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Music
{
	// TODO Refactor?
	public class JukeboxNowPlayingMessageComposer : AbstractComposer<uint, int, int>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, uint songId, int playlistPosition, int songPosition)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(songId);
				message.AppendInteger(playlistPosition);
				message.AppendInteger(songId);
				message.AppendInteger(0);
				message.AppendInteger(songPosition); // songPosition in ms
				session.Send (message);
			}
		}
	}
}


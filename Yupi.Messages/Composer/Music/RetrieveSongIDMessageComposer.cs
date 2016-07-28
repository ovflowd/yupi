using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Music
{
	public class RetrieveSongIDMessageComposer : AbstractComposer
	{
		public void Compose(Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, string name, int songId) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(name);
				message.AppendInteger(songId);
				session.Send (message);
			}
		}
	}
}


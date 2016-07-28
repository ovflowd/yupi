using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomLoadFilterMessageComposer : AbstractComposer<List<string>>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, List<string> wordlist)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(wordlist.Count);

				foreach (string word in wordlist) {
					message.AppendString (word);
				}

				session.Send (message);
			}
		}
	}
}


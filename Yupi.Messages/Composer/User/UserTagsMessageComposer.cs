using System;
using Yupi.Protocol.Buffers;
using Yupi.Net;
using System.Collections.Generic;


namespace Yupi.Messages.User
{
	public class UserTagsMessageComposer : AbstractComposer
	{
		public void Compose(Yupi.Protocol.ISender session, int userId, List<string> tags) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (userId);
				message.Append (tags);
				session.Send (message);
			}
		}
	}
}


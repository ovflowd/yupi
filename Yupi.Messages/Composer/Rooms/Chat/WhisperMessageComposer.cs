using System;
using Yupi.Protocol.Buffers;
using Yupi.Messages.Encoders;
using Yupi.Model.Domain;

namespace Yupi.Messages.Chat
{
	public class WhisperMessageComposer : Contracts.WhisperMessageComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, ChatlogEntry msg, int count = -1)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.Append (msg, count);
				session.Send (message);
			}
		}
	}
}


using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Chat
{
	public class WhisperMessageComposer : AbstractComposer<uint, string, int>
	{
		public override void Compose (Yupi.Protocol.ISender session, uint entityId, string msg, int color)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(entityId);
				message.AppendString(msg);
				message.AppendInteger(0);
				message.AppendInteger(color);
				message.AppendInteger(0);
				message.AppendInteger(-1);
				session.Send (message);
			}
		}
	}
}


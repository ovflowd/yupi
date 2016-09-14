using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Notification
{
	public class BroadcastNotifMessageComposer : Yupi.Messages.Contracts.BroadcastNotifMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, string text)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (text);
				message.AppendString (string.Empty);
				session.Send (message);
			}
		}
	}
}


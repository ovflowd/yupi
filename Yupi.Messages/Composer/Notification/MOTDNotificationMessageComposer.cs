﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Notification
{
	public class MOTDNotificationMessageComposer : Yupi.Messages.Contracts.MOTDNotificationMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, string text)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendString (text);
				session.Send (message);
			}
		}
	}
}

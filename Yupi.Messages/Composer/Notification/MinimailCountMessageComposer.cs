﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Notification
{
	public class MinimailCountMessageComposer : AbstractComposer<int>
	{
		public override void Compose (Yupi.Protocol.ISender session, int count)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(count);
				session.Send (message);
			}
		}
	}
}


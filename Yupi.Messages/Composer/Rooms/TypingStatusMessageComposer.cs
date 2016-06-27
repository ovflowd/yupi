﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class TypingStatusMessageComposer : AbstractComposer<uint, bool>
	{
		public override void Compose (Yupi.Protocol.ISender session, uint virtualId, bool isTyping)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(virtualId);
				message.AppendInteger (isTyping);
				session.Send (message);
			}
		}
	}
}

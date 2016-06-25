﻿using System;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class AddFloorItemMessageComposer
	{
		public override void Compose (Yupi.Protocol.ISender room, RoomItem value)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				item.Serialize(message);
				message.AppendString(room.RoomData.Owner);
				room.Send (message);
			}
		}
	}
}

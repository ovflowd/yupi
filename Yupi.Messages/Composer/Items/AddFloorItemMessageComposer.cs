﻿using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
	public class AddFloorItemMessageComposer : Yupi.Messages.Contracts.AddFloorItemMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender room, IFloorItem item)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				item.Serialize(message);
				message.AppendString(room.RoomData.Owner);
				room.Send (message);
			}
		}
	}
}


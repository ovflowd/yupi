using System;
using Yupi.Emulator.Game.Items.Interfaces;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Wired
{
	public class WiredTriggerMessageComposer : AbstractComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomItem item, List<RoomItem> items, int delay, string extraInfo, int unknown)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(false);
				message.AppendInteger(unknown);
				message.AppendInteger(items.Count);
				// TODO Won't work properly. Must implement composer correctly...
				foreach (RoomItem current in items) {
					message.AppendInteger (current.Id);
				}

				message.AppendInteger(item.GetBaseItem().SpriteId);
				message.AppendInteger(item.Id);
				message.AppendString(extraInfo);

				message.AppendInteger(1);
				message.AppendInteger(delay);
				message.AppendInteger(1);
				message.AppendInteger(3);
				message.AppendInteger(0);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}


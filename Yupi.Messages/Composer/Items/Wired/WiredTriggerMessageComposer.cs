using System;

using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Wired
{
	public class WiredTriggerMessageComposer : AbstractComposer
	{
		public void Compose ( Yupi.Protocol.ISender session, FloorItem item, List<FloorItem> items, int delay, string extraInfo, int unknown)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(false);
				message.AppendInteger(unknown);
				message.AppendInteger(items.Count);
				// TODO Won't work properly. Must implement composer correctly...
				foreach (FloorItem current in items) {
					message.AppendInteger (current.Id);
				}

				message.AppendInteger(item.BaseItem.SpriteId);
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


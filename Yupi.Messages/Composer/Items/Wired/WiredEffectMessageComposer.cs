using System;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.Wired
{
	public class WiredEffectMessageComposer : AbstractComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomItem item, string extraInfo, int delay, List<RoomItem> list = null)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(false);
				message.AppendInteger(5);
				// TODO Probably won't work correctly. Must rewrite the entire composer...
				if (list == null) {
					message.AppendInteger (0);
				} else {
					message.AppendInteger (list.Count);
					foreach (RoomItem current in list) 
						message.AppendInteger(current.Id);
				}
				message.AppendInteger(item.GetBaseItem().SpriteId);
				message.AppendInteger(item.Id);
				message.AppendString(extraInfo);
				message.AppendInteger(1);
				message.AppendInteger(delay);
				message.AppendInteger(0);
				message.AppendInteger(20);
				message.AppendInteger(0);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}


using System;

using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Wired
{
	public class WiredEffectMessageComposer : AbstractComposer
	{
		public void Compose ( Yupi.Protocol.ISender session, FloorItem item, string extraInfo, int delay, List<FloorItem> list = null)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(false);
				message.AppendInteger(5);
				// TODO Probably won't work correctly. Must rewrite the entire composer...
				if (list == null) {
					message.AppendInteger (0);
				} else {
					message.AppendInteger (list.Count);
					foreach (FloorItem current in list) 
						message.AppendInteger(current.Id);
				}
				message.AppendInteger(item.BaseItem.SpriteId);
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


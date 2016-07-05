using System;

using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Wired
{
	public class WiredConditionMessageComposer : AbstractComposer<RoomItem, List<RoomItem>, string>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomItem item, List<RoomItem> list, string extraString)
		{// TODO Won't work properly. Must implement composer correctly...
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(false);
				message.AppendInteger(5);

				if (list == null) {
					list = new List<RoomItem> ();
				}
				message.AppendInteger(list.Count);
				foreach (RoomItem current20 in list) message.AppendInteger(current20.Id);
				message.AppendInteger(item.GetBaseItem().SpriteId);
				message.AppendInteger(item.Id);
				message.AppendString(extraString);
				message.AppendInteger(3);


					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendInteger(0);

				message.AppendInteger(0);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}


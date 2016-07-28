using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Wired
{
	public class WiredConditionMessageComposer : AbstractComposer<FloorItem, List<FloorItem>, string>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, FloorItem item, List<FloorItem> list, string extraString)
		{// TODO Won't work properly. Must implement composer correctly...
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(false);
				message.AppendInteger(5);

				if (list == null) {
					list = new List<FloorItem> ();
				}
				message.AppendInteger(list.Count);
				foreach (FloorItem current20 in list) message.AppendInteger(current20.Id);
				message.AppendInteger(item.BaseItem.SpriteId);
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


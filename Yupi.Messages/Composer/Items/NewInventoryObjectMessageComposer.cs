using System;

using Yupi.Protocol.Buffers;
using System.Collections.Generic;


namespace Yupi.Messages.Items
{ // TODO Refactor
	public class NewInventoryObjectMessageComposer : AbstractComposer<Item, List<UserItem>>
	{
		public void Compose (Yupi.Protocol.ISender session, uint itemId) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendInteger(1);
				message.AppendInteger(1);
				message.AppendInteger(itemId);
				session.Send (message);
			}
		}

		public override void Compose (Yupi.Protocol.ISender session, Item item, List<UserItem> list)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);

				// TODO Use enums (with values)
				int i = 2;

				if (item != null && item.Type == 's')
					i = item.InteractionType == Interaction.Pet ? 3 : 1;

				message.AppendInteger(i);
				message.AppendInteger(list.Count);

				foreach (UserItem current in list)
					message.AppendInteger(current.Id);
				session.Send (message);
			}
		}
	}
}


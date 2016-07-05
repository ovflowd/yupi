using System;


using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class LoadPostItMessageComposer : AbstractComposer<RoomItem>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomItem item)
		{
			if (item == null || item.GetBaseItem().InteractionType != Interaction.PostIt)
				return;

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(item.Id.ToString());
				message.AppendString(item.ExtraData);
				session.Send (message);
			}
		}
	}
}


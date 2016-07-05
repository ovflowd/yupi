using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class AddWallItemMessageComposer : AbstractComposer<RoomItem>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomItem item)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				item.Serialize(message);
				message.AppendString(session.GetHabbo().UserName);
				session.Send (message);
			}
		}
	}
}


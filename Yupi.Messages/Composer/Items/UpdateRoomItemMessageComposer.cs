using System;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class UpdateRoomItemMessageComposer : AbstractComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomItem item)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				item.Serialize(message);
				session.Send (message);
			}
		}
	}
}


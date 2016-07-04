using System;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class PickUpWallItemMessageComposer : AbstractComposer<RoomItem, int>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomItem item, int pickerId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(item.Id.ToString());
				message.AppendInteger(pickerId);
				session.Send (message);
			}
		}
	}
}


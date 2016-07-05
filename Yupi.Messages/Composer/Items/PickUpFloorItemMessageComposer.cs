using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class PickUpFloorItemMessageComposer : AbstractComposer<RoomItem, int>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomItem item, int pickerId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(item.Id.ToString());
				message.AppendBool(false); //expired
				message.AppendInteger(pickerId); 
				message.AppendInteger(0); // delay
				session.Send (message);
			}
		}
	}
}


using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
	public class UpdateRoomItemMessageComposer : AbstractComposer<FloorItem>
	{
		public override void Compose ( Yupi.Protocol.ISender session, FloorItem item)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				item.Serialize(message);
				session.Send (message);
			}
		}
	}
}


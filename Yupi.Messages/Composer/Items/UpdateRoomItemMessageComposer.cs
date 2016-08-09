using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
	public class UpdateRoomItemMessageComposer : Yupi.Messages.Contracts.UpdateRoomItemMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, IFloorItem item)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				item.Serialize(message);
				session.Send (message);
			}
		}
	}
}


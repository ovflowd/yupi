using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
	public class PickUpWallItemMessageComposer : AbstractComposer<WallItem, int>
	{
		public override void Compose ( Yupi.Protocol.ISender session, WallItem item, int pickerId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(item.Id.ToString());
				message.AppendInteger(pickerId);
				session.Send (message);
			}
		}
	}
}


using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class RemoveInventoryObjectMessageComposer : Contracts.RemoveInventoryObjectMessageComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, int itemId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (itemId);
				session.Send (message);
			}
		}
	}
}


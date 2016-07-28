using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class UpdateTileStackMagicHeight : AbstractComposer<int, int>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int itemId, int z)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(itemId);
				message.AppendInteger(Convert.ToUInt32(z*100));
				session.Send (message);
			}
		}
	}
}


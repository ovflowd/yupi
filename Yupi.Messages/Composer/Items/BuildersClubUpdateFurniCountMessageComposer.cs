using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class BuildersClubUpdateFurniCountMessageComposer : AbstractComposer<int>
	{
		public override void Compose (Yupi.Protocol.ISender session, int itemsUsed)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (itemsUsed);
				session.Send (message);
			}
		}
	}
}


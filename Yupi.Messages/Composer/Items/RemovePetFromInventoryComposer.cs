using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class RemovePetFromInventoryComposer : AbstractComposer<uint>
	{
		public override void Compose ( Yupi.Protocol.ISender session, uint petId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (petId);
				session.Send (message);
			}
		}
	}
}


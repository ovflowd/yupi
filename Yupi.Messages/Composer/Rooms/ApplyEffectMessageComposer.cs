using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class ApplyEffectMessageComposer : AbstractComposer<int, int>
	{
		public override void Compose ( Yupi.Protocol.ISender session, int entityId, int effectId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (entityId);
				message.AppendInteger(effectId);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}


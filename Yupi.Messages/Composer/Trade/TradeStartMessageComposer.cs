using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Trade
{
	public class TradeStartMessageComposer : AbstractComposer<uint, uint>
	{
		public override void Compose (Yupi.Protocol.ISender session, uint firstUserId, uint secondUserId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(firstUserId);
				message.AppendInteger(1);
				message.AppendInteger(secondUserId);
				message.AppendInteger(1);
				session.Send (message);
			}
		}
	}
}


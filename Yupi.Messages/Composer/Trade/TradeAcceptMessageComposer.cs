using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Trade
{
	public class TradeAcceptMessageComposer : Yupi.Messages.Contracts.TradeAcceptMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, uint userId, bool accepted)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(userId);
				message.AppendInteger(accepted);
				session.Send (message);
			}
		}
	}
}


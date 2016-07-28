using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Trade
{
	public class TradeCloseMessageComposer : AbstractComposer<uint>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, uint closedById)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(closedById);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}


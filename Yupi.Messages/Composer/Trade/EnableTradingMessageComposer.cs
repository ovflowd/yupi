using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Trade
{
	public class EnableTradingMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(true); 
				session.Send (message);
			}
		}
	}
}


using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Trade
{
	public class EnableTradingMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(true); 
				session.Send (message);
			}
		}
	}
}


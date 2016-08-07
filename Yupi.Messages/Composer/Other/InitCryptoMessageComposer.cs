using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
	public class InitCryptoMessageComposer : Yupi.Messages.Contracts.InitCryptoMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				// TODO What about public networks?
				message.AppendString("Yupi");
				message.AppendString("Disabled Crypto");
				session.Send (message);
			}
		}
	}
}


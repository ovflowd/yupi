using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
	public class ReloadEcotronMessageComposer : Yupi.Messages.Contracts.ReloadEcotronMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			// TODO Hardcoded message
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}


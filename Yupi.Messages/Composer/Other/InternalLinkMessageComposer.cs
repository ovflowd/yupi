using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
	public class InternalLinkMessageComposer : AbstractComposer<string>
	{
		public override void Compose (Yupi.Protocol.ISender session, string link)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(link);
				session.Send (message);
			}
		}
	}
}


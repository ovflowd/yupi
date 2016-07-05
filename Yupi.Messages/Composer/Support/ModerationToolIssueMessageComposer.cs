using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
	public class ModerationToolIssueMessageComposer : AbstractComposer<SupportTicket>
	{
		public override void Compose (Yupi.Protocol.ISender session, SupportTicket ticket)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				ticket.Serialize(message);
			}
		}
	}
}


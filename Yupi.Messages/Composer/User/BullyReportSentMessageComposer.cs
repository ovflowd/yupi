using System;
using Yupi.Protocol.Buffers;
using Yupi.Net;


namespace Yupi.Messages.User
{
	public class BullyReportSentMessageComposer : AbstractComposerVoid
	{
		public void Compose(Yupi.Protocol.ISender session) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (0); // TODO What does 0 mean?
				session.Send (message);
			}
		}
	}
}


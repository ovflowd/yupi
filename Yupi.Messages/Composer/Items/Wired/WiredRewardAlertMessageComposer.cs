using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Wired
{
	public class WiredRewardAlertMessageComposer : AbstractComposer<bool>
	{
		public override void Compose (Yupi.Protocol.ISender session, bool success)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(success ? 7 : 1); // TODO Strange values...
				session.Send (message);
			}
		}
	}
}


using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Notification
{
	public class GeneralErrorHabboMessageComposer : Yupi.Messages.Contracts.GeneralErrorHabboMessageComposer
	{
		// TODO Replace errorId with enum
		public override void Compose ( Yupi.Protocol.ISender session, int errorId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (errorId);
				session.Send (message);
			}
		}
	}
}


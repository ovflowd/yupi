using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Notification
{
	public class GeneralErrorHabboMessageComposer : AbstractComposer<int>
	{
		// TODO Replace errorId with enum
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int errorId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (errorId);
				session.Send (message);
			}
		}
	}
}


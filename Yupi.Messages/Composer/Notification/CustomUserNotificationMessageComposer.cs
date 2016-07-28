using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Notification
{
	public class CustomUserNotificationMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(3);
				session.Send (message);
			}
		}
	}
}


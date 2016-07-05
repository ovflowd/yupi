using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Notification
{
	public class UsersClassificationMessageComposer : AbstractComposer<Habbo, string>
	{
		public override void Compose (Yupi.Protocol.ISender session, Habbo habbo, string word)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendInteger(habbo.Id);
				message.AppendString(habbo.UserName);
				message.AppendString("BadWord: " + word);
				session.Send(message);
			}
		}
	}
}


using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Notification
{
	public class UsersClassificationMessageComposer : Yupi.Messages.Contracts.UsersClassificationMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, UserInfo habbo, string word)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendInteger(habbo.Id);
				message.AppendString(habbo.Name);
				message.AppendString("BadWord: " + word);
				session.Send(message);
			}
		}
	}
}


using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Messenger
{
	public class ConsoleSendFriendRequestMessageComposer : AbstractComposer<MessengerRequest>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, MessengerRequest request)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(request.From.Id);
				message.AppendString(request.From.UserName);
				message.AppendString(request.From.Look);
				session.Send (message);
			}
		}
	}
}


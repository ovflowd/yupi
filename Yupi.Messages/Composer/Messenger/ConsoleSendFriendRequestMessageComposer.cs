using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Messenger
{
	public class ConsoleSendFriendRequestMessageComposer : AbstractComposer<MessengerRequest>
	{
		public override void Compose (Yupi.Protocol.ISender session, MessengerRequest request)
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


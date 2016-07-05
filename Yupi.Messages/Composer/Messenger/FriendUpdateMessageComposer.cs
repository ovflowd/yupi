using System;

using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Messenger
{
	public class FriendUpdateMessageComposer : AbstractComposer<MessengerBuddy, GameClient>
	{
		public override void Compose (Yupi.Protocol.ISender session, MessengerBuddy friend, GameClient client)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(0);
				message.AppendInteger(1);
				message.AppendInteger(0);
				friend.Serialize(message, client);
				message.AppendBool(false);
				session.Send (message);
			}
		}
	}
}


using System;
using System.Collections.Generic;
using Yupi.Emulator.Game.Users.Messenger.Structs;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
	public class LoadFriendsMessageComposer : AbstractComposer<Dictionary<uint, MessengerBuddy>, GameClient>
	{
		public override void Compose (Yupi.Protocol.ISender session, Dictionary<uint, MessengerBuddy> friends, GameClient client)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendInteger(0);
				message.AppendInteger(friends.Count);

				foreach (MessengerBuddy current in friends.Values)
				{
					current.UpdateUser();
					current.Serialize(message, client);
				}

				session.Send (message);
			}
		}
	}
}


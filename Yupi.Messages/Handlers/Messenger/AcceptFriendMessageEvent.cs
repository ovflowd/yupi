using System;


namespace Yupi.Messages.Messenger
{
	public class AcceptFriendMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			if (session.GetHabbo().GetMessenger() == null) return;
			int count = request.GetInteger();
			for (int i = 0; i < count; i++)
			{
				uint userId = request.GetUInt32();
				MessengerRequest friendRequest = session.GetHabbo().GetMessenger().GetRequest(userId);

				if (friendRequest == null || friendRequest.To != session.GetHabbo().Id) 
					continue;
				
				if (!session.GetHabbo().GetMessenger().FriendshipExists(friendRequest.To))
					session.GetHabbo().GetMessenger().CreateFriendship(friendRequest.From);
				
				session.GetHabbo().GetMessenger().HandleRequest(userId);
			}
		}
	}
}


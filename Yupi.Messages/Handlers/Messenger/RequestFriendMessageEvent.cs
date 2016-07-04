using System;

namespace Yupi.Messages.Messenger
{
	public class RequestFriendMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			if (session.GetHabbo().GetMessenger() == null)
				return;

			session.GetHabbo().GetMessenger().RequestBuddy(request.GetString());
		}
	}
}


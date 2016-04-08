using System;

namespace Yupi.Messages.Messenger
{
	public class FriendListUpdateMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			session.GetHabbo().GetMessenger();
		}
	}
}


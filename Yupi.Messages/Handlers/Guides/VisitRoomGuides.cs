using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Messages.User;

namespace Yupi.Messages.Guides
{
	public class VisitRoomGuides : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			if (session.GetHabbo().GuideOtherUser == null)
				return;

			GameClient requester = session.GetHabbo().GuideOtherUser;

			router.GetComposer<RoomForwardMessageComposer> ().Compose (session, requester.GetHabbo ().CurrentRoomId);
		}
	}
}


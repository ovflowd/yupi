using System;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class LoadBadgeInventoryMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			// TODO Refactor
			session.Send(session.GetHabbo().GetBadgeComponent().Serialize());
		}
	}
}


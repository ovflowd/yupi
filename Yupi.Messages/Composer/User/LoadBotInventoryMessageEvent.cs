using System;

namespace Yupi.Messages.User
{
	public class LoadBotInventoryMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			// TODO Refactor
			session.Send(session.GetHabbo().GetInventoryComponent().SerializeBotInventory());
		}
	}
}


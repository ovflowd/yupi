using System;

namespace Yupi.Messages.User
{
	public class LoadItemsInventoryMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			// TODO move here!
			session.GetHabbo().GetInventoryComponent().SerializeFloorItemInventory()
		}
	}
}


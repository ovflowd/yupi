using System;

namespace Yupi.Messages.Groups
{
	public class GetGroupPurchasingInfoMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			router.GetComposer<GroupPurchasePartsMessageComposer> ().Compose (session);
		}
	}
}


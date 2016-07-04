using System;

namespace Yupi.Messages.Catalog
{
	public class ReloadRecyclerMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			router.GetComposer<ReloadEcotronMessageComposer> ().Compose (session);
		}
	}
}


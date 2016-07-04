using System;

namespace Yupi.Messages.Pets
{
	public class GetSellablePetBreedsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			string type = request.GetString();
			router.GetComposer<SellablePetBreedsMessageComposer> ().Compose (type);
		}
	}
}


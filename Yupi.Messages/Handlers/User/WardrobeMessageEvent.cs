using System;
using System.Data;


namespace Yupi.Messages.User
{
	public class WardrobeMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadWardrobeMessageComposer> ().Compose (session);
		}
	}
}


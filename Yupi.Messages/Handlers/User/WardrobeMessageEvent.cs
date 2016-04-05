using System;
using System.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.User
{
	public class WardrobeMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			router.GetComposer<LoadWardrobeMessageComposer> ().Compose (session);
		}
	}
}


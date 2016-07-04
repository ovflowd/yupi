using System;

namespace Yupi.Messages.Other
{
	public class InitCryptoMessageEvent : AbstractHandler
	{
		public bool RequireUser {
			get { 
				return false; 
			}
		}

		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			router.GetComposer<InitCryptoMessageComposer> ().Compose (session);
		}
	}
}


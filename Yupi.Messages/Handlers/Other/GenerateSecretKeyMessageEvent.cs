using System;

namespace Yupi.Messages.Other
{
	public class GenerateSecretKeyMessageEvent : AbstractHandler
	{
		public override bool RequireUser {
			get { 
				return false; 
			}
		}

		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			request.GetString(); // TODO unused

			router.GetComposer<SecretKeyMessageComposer> ().Compose (session);
		}
	}
}


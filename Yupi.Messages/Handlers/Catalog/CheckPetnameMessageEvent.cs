using System;

namespace Yupi.Messages.Catalog
{
	public class CheckPetnameMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			string petName = message.GetString();
			int statusCode = 0;

			// TODO Use enum

			if (petName.Length > 15)
				statusCode = 1;
			else if (petName.Length < 3)
				statusCode = 2;
			else if (!Yupi.Emulator.Yupi.IsValidAlphaNumeric(petName))
				statusCode = 3;

			router.GetComposer<CheckPetNameMessageComposer> ().Compose (session, statusCode, petName);
		}
	}
}


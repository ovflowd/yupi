using System;


namespace Yupi.Messages.User
{
	public class RetrieveCitizenshipStatus : AbstractHandler
	{
		public void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			string whatever = message.GetString (); // TODO What does the string contain?
			router.GetComposer<CitizenshipStatusMessageComposer> ().Compose (session, whatever);
		}
	}
}


using System;


namespace Yupi.Messages.User
{
	public class RetrieveCitizenshipStatus : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string whatever = message.GetString (); // TODO What does the string contain?
			router.GetComposer<CitizenshipStatusMessageComposer> ().Compose (session, whatever);
		}
	}
}


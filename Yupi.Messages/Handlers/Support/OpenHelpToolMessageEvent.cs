using System;

namespace Yupi.Messages.Support
{
	public class OpenHelpToolMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<OpenHelpToolMessageComposer> ().Compose (session, session.GetHabbo ());
		}
	}
}


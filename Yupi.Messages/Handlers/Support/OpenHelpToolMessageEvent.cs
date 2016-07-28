using System;

namespace Yupi.Messages.Support
{
	public class OpenHelpToolMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<OpenHelpToolMessageComposer> ().Compose (session, session.GetHabbo ());
		}
	}
}


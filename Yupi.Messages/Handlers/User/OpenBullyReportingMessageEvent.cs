using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.User
{
	public class OpenBullyReportingMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<OpenBullyReportMessageComposer> ().Compose (session);
		}
	}
}


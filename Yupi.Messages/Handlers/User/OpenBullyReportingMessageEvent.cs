using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.User
{
	public class OpenBullyReportingMessageEvent : AbstractHandler
	{
		public void HandleMessage (GameClient session, ClientMessage message, Router router)
		{
			router.GetComposer<OpenBullyReportMessageComposer> ().Compose (session);
		}
	}
}


using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class OpenBullyReportingMessageEvent : AbstractHandler
	{
		public void HandleMessage (ISession<GameClient> session, ClientMessage message, Router router)
		{
			router.GetComposer<OpenBullyReportMessageComposer> ().Compose (session);
		}
	}
}


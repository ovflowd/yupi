using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;
using Yupi.Messages.User;
using Yupi.Emulator.Game.GameClients.Interfaces;
using System.Collections.Generic;

namespace Yupi.Messages.Handlers.User
{
	public class SendBullyReportMessageEvent : AbstractHandler
	{
		public void HandleMessage (GameClient session, ClientMessage message, Router router)
		{
			uint reportedId = message.GetUInt32();
			// TODO Refactor
			Yupi.GetGame()
				.GetModerationTool()
				.SendNewTicket(session, 104, 9, reportedId, string.Empty, new List<string>());

			router.GetComposer<BullyReportSentMessageComposer> ().Compose (session);
		}
	}
}


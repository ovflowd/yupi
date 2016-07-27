using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;
using Yupi.Messages.User;
using System.Collections.Generic;

namespace Yupi.Messages.Handlers.User
{
	public class SendBullyReportMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, ClientMessage message, Yupi.Protocol.IRouter router)
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


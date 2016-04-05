using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class OpenQuestsMessageEvent : AbstractHandler
	{
		public void HandleMessage (GameClient session, ClientMessage message, Router router)
		{
			router.GetComposer<QuestListMessageComposer> ().Compose (session);
		}
	}
}
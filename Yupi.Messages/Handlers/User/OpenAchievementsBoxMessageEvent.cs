using System;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class OpenAchievementsBoxMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			Yupi.GetGame ().GetAchievementManager ().GetList (session);
		}
	}
}


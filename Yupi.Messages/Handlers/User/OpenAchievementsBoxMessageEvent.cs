using System;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class OpenAchievementsBoxMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			session.Router.GetComposer<AchievementListMessageComposer> ().Compose (session, session.GetHabbo (),
				Yupi.GetGame ().GetAchievementManager ().Achievements.Values.ToList ());
		}
	}
}


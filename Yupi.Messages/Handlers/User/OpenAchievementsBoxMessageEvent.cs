using System;


namespace Yupi.Messages.User
{
	public class OpenAchievementsBoxMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			session.Router.GetComposer<AchievementListMessageComposer> ().Compose (session, session.GetHabbo (),
				Yupi.GetGame ().GetAchievementManager ().Achievements.Values.ToList ());
		}
	}
}


﻿using System;


namespace Yupi.Messages.User
{
	public class OpenAchievementsBoxMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			session.Router.GetComposer<AchievementListMessageComposer> ().Compose (session, 
				session.Info.Achievements);
		}
	}
}

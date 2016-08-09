using System;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.User
{
	public class CompleteSafetyQuizMessageEvent : AbstractHandler
	{
		private AchievementManager AchievementManager;

		public CompleteSafetyQuizMessageEvent ()
		{
			AchievementManager = DependencyFactory.Resolve<AchievementManager> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			AchievementManager.ProgressUserAchievement(session.UserData, "ACH_SafetyQuizGraduate", 1);
		}
	}
}


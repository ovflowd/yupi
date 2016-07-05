using System;

namespace Yupi.Messages.User
{
	public class CompleteSafetyQuizMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(session, "ACH_SafetyQuizGraduate", 1);
		}
	}
}


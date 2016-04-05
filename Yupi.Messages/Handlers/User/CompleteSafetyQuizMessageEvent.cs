using System;

namespace Yupi.Messages.User
{
	public class CompleteSafetyQuizMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(session, "ACH_SafetyQuizGraduate", 1);
		}
	}
}


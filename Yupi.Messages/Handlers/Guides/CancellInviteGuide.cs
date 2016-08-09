using System;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.Guides
{
	// TODO Rename
	public class CancellInviteGuide : AbstractHandler
	{
		private AchievementManager AchievementManager;

		public CancellInviteGuide ()
		{
			AchievementManager = DependencyFactory.Resolve<AchievementManager> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<OnGuideSessionDetachedMessageComposer> ().Compose (session, 2);

			AchievementManager.ProgressUserAchievement(session.UserData, "ACH_GuideFeedbackGiver", 1);
		}
	}
}


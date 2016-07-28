using System;

namespace Yupi.Messages.Guides
{
	// TODO Rename
	public class CancellInviteGuide : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<OnGuideSessionDetachedMessageComposer> ().Compose (session, 2);

			Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_GuideFeedbackGiver", 1);
		}
	}
}


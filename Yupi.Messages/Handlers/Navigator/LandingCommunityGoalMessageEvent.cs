using System;

namespace Yupi.Messages.Navigator
{
	public class LandingCommunityGoalMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			int onlineFriends = session.GetHabbo().GetMessenger().Friends.Count(x => x.Value.IsOnline);
			router.GetComposer<LandingCommunityChallengeMessageComposer> ().Compose (session, onlineFriends);
		}
	}
}


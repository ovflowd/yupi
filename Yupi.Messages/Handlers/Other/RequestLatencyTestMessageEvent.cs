using System;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.Other
{
	public class RequestLatencyTestMessageEvent : AbstractHandler
	{
		private AchievementManager AchievementManager;

		public RequestLatencyTestMessageEvent ()
		{
			AchievementManager = DependencyFactory.Resolve<AchievementManager> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			// TODO Doesn't seem right here! Could easily be faked by wrong packets!
			AchievementManager.ProgressUserAchievement(session, "ACH_AllTimeHotelPresence", 1);

			session.TimePingReceived = DateTime.Now;
		}
	}
}


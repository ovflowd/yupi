using System;

namespace Yupi.Messages.Other
{
	public class RequestLatencyTestMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(session, "ACH_AllTimeHotelPresence", 1, true);

			session.TimePingedReceived = DateTime.Now;
		}
	}
}


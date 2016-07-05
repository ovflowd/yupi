using System;


namespace Yupi.Messages.User
{
	public class GetCurrencyBalanceMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			session.GetHabbo().UpdateCreditsBalance();
			session.GetHabbo().UpdateSeasonalCurrencyBalance();
		}
	}
}


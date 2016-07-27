using System;


namespace Yupi.Messages.User
{
	public class GetCurrencyBalanceMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			session.GetHabbo().UpdateCreditsBalance();
			session.GetHabbo().UpdateSeasonalCurrencyBalance();
		}
	}
}


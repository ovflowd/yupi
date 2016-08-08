using System;


namespace Yupi.Messages.User
{
	public class GetCurrencyBalanceMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<CreditsBalanceMessageComposer>().Compose(session, session.UserData.Info.Wallet.Credits);
			router.GetComposer<ActivityPointsMessageComposer>().Compose(session, session.UserData.Info.Wallet);
		}
	}
}


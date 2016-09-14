namespace Yupi.Messages.User
{
    using System;

    public class GetCurrencyBalanceMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<CreditsBalanceMessageComposer>().Compose(session, session.Info.Wallet.Credits);
            router.GetComposer<ActivityPointsMessageComposer>().Compose(session, session.Info.Wallet);
        }

        #endregion Methods
    }
}
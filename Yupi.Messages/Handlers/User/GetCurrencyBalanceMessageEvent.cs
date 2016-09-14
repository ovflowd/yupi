using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class GetCurrencyBalanceMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            router.GetComposer<CreditsBalanceMessageComposer>().Compose(session, session.Info.Wallet.Credits);
            router.GetComposer<ActivityPointsMessageComposer>().Compose(session, session.Info.Wallet);
        }
    }
}
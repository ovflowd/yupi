using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class GetSubscriptionDataMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            router.GetComposer<SubscriptionStatusMessageComposer>().Compose(session, session.Info.Subscription);
            // TODO Implement
            router.GetComposer<UserClubRightsMessageComposer>().Compose(session, false, session.Info.Rank, false);
        }
    }
}
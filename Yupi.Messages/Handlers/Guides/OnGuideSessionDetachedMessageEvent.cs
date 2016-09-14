using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
    public class OnGuideSessionDetachedMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var state = message.GetBool();
            // TODO What?!
            if (!state)
                return;

            var requester = session.GuideOtherUser;

            // TODO SessionStarted on Detach???
            router.GetComposer<OnGuideSessionStartedMessageComposer>().Compose(session, requester.Info);
            router.GetComposer<OnGuideSessionStartedMessageComposer>().Compose(requester, requester.Info);
        }
    }
}
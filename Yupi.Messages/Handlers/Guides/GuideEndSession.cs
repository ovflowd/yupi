using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
    // TODO Rename
    public class GuideEndSession : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var requester = session.GuideOtherUser;

            // TODO Test & Fixme !!!

            router.GetComposer<OnGuideSessionDetachedMessageComposer>().Compose(requester, 2);
            router.GetComposer<OnGuideSessionDetachedMessageComposer>().Compose(session, 0);

            requester.GuideOtherUser = null;
            session.GuideOtherUser = null;
        }
    }
}
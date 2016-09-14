using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
    public class GuideToolMessageNew : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var message = request.GetString();

            router.GetComposer<OnGuideSessionMsgMessageComposer>()
                .Compose(session.GuideOtherUser, message, session.Info.Id);
            router.GetComposer<OnGuideSessionMsgMessageComposer>().Compose(session, message, session.Info.Id);
        }
    }
}
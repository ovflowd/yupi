using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
    public class OnGuideMessageEvent : AbstractHandler
    {
        private readonly GuideManager GuideManager;

        public OnGuideMessageEvent()
        {
            GuideManager = DependencyFactory.Resolve<GuideManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            request.GetBool(); // TODO Unused 

            var idAsString = request.GetString();

            int userId;
            int.TryParse(idAsString, out userId);

            if (userId == 0) return;

            var message = request.GetString();

            var guide = GuideManager.GetRandomGuide();

            if (guide == null)
            {
                router.GetComposer<OnGuideSessionErrorComposer>().Compose(session);
                return;
            }

            router.GetComposer<OnGuideSessionAttachedMessageComposer>().Compose(session, false, userId, message, 30);
            router.GetComposer<OnGuideSessionAttachedMessageComposer>().Compose(guide, true, userId, message, 15);

            guide.GuideOtherUser = session;
            session.GuideOtherUser = guide;
        }
    }
}
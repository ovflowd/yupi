namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;

    public class OnGuideMessageEvent : AbstractHandler
    {
        #region Fields

        private GuideManager GuideManager;

        #endregion Fields

        #region Constructors

        public OnGuideMessageEvent()
        {
            GuideManager = DependencyFactory.Resolve<GuideManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            request.GetBool(); // TODO Unused

            string idAsString = request.GetString();

            int userId;
            int.TryParse(idAsString, out userId);

            if (userId == 0)
            {
                return;
            }

            string message = request.GetString();

            Habbo guide = GuideManager.GetRandomGuide();

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

        #endregion Methods
    }
}
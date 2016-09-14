namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Model.Domain;

    public class OnGuideSessionDetachedMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            bool state = message.GetBool();
            // TODO What?!
            if (!state)
                return;

            Habbo requester = session.GuideOtherUser;

            // TODO SessionStarted on Detach???
            router.GetComposer<OnGuideSessionStartedMessageComposer>().Compose(session, requester.Info);
            router.GetComposer<OnGuideSessionStartedMessageComposer>().Compose(requester, requester.Info);
        }

        #endregion Methods
    }
}
using System;


namespace Yupi.Messages.Guides
{
    public class GuideToolMessageNew : AbstractHandler
    {
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string message = request.GetString();

            router.GetComposer<OnGuideSessionMsgMessageComposer>()
                .Compose(session.GuideOtherUser, message, session.Info.Id);
            router.GetComposer<OnGuideSessionMsgMessageComposer>().Compose(session, message, session.Info.Id);
        }
    }
}
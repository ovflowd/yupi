using System;
using Yupi.Model.Domain;


namespace Yupi.Messages.Guides
{
	public class OnGuideSessionDetachedMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			bool state = message.GetBool();
			// TODO What?!
			if (!state)
				return;

			Habbo requester = session.UserData.GuideOtherUser;

			// TODO SessionStarted on Detach???
			router.GetComposer<OnGuideSessionStartedMessageComposer> ().Compose (session, requester.Info);
			router.GetComposer<OnGuideSessionStartedMessageComposer> ().Compose (requester, requester.Info);
		}
	}
}


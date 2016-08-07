using System;


namespace Yupi.Messages.Guides
{
	public class OnGuideSessionDetachedMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			bool state = message.GetBool();

			if (!state)
				return;

			GameClient requester = session.GetHabbo().GuideOtherUser;

			// TODO SessionStarted on Detach???
			router.GetComposer<OnGuideSessionStartedMessageComposer> ().Compose (session, requester.GetHabbo (), requester);
		}
	}
}


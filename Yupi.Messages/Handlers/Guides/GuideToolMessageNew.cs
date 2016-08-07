using System;


namespace Yupi.Messages.Guides
{
	public class GuideToolMessageNew : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string message = request.GetString();
			GameClient requester = session.GetHabbo().GuideOtherUser;

			router.GetComposer<OnGuideSessionMsgMessageComposer> ().Compose (session, requester, message, session.GetHabbo ().Id);
		}
	}
}


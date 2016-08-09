using System;


namespace Yupi.Messages.Guides
{
	public class GuideToolMessageNew : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string message = request.GetString();

			router.GetComposer<OnGuideSessionMsgMessageComposer> ().Compose (session.UserData.GuideOtherUser, message, session.UserData.Info.Id);
			router.GetComposer<OnGuideSessionMsgMessageComposer> ().Compose (session, message, session.UserData.Info.Id);
		}
	}
}


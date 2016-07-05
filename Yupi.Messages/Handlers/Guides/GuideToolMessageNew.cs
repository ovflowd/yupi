using System;


namespace Yupi.Messages.Guides
{
	public class GuideToolMessageNew : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			string message = request.GetString();
			GameClient requester = session.GetHabbo().GuideOtherUser;

			router.GetComposer<OnGuideSessionMsgMessageComposer> ().Compose (session, requester, message, session.GetHabbo ().Id);
		}
	}
}


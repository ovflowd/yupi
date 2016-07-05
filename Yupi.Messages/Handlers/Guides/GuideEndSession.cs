using System;


namespace Yupi.Messages.Guides
{
	// TODO Rename
	public class GuideEndSession : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			GameClient requester = session.GetHabbo().GuideOtherUser;

			// TODO Test & Fixme !!!

			/* guide - close session  */
			router.GetComposer<OnGuideSessionDetachedMessageComposer> ().Compose (requester, 2);

			/* user - close session */
			router.GetComposer<OnGuideSessionDetachedMessageComposer> ().Compose (session, 0);

			requester.GetHabbo().GuideOtherUser = null;
			session.GetHabbo().GuideOtherUser = null;
		}
	}
}


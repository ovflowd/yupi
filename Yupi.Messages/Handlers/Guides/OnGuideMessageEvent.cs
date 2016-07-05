using System;


namespace Yupi.Messages.Guides
{
	public class OnGuideMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			request.GetBool();

			string idAsString = request.GetString ();

			int userId;
			int.TryParse (idAsString, out userId);

			if (userId == 0) {
				return;
			}

			string message = request.GetString();

			GuideManager guideManager = Yupi.GetGame().GetGuideManager();


			if (guideManager.GuidesCount <= 0) {
				router.GetComposer<OnGuideSessionError> ().Compose (session);
				return;
			}

			GameClient guide = guideManager.GetRandomGuide();
			// TODO Refactor
			if (guide == null) {
				router.GetComposer<OnGuideSessionError> ().Compose (session);
				return;
			}

			router.GetComposer<OnGuideSessionAttachedMessageComposer> ().Compose (session, false, userId, message, 30);

			router.GetComposer<OnGuideSessionAttachedMessageComposer> ().Compose (guide, true, userId, message, 15);

			// TODO Refactor
			guide.GetHabbo().GuideOtherUser = session;
			session.GetHabbo().GuideOtherUser = guide;
		}
	}
}


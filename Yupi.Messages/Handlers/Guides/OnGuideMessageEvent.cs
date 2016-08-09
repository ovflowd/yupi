using System;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;


namespace Yupi.Messages.Guides
{
	public class OnGuideMessageEvent : AbstractHandler
	{
		private GuideManager GuideManager;

		public OnGuideMessageEvent ()
		{
			GuideManager = DependencyFactory.Resolve<GuideManager> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			request.GetBool(); // TODO Unused 

			string idAsString = request.GetString ();

			int userId;
			int.TryParse (idAsString, out userId);

			if (userId == 0) {
				return;
			}

			string message = request.GetString();

			Habbo guide = GuideManager.GetRandomGuide ();

			if (guide == null) {
				router.GetComposer<OnGuideSessionErrorComposer> ().Compose (session);
				return;
			}

			router.GetComposer<OnGuideSessionAttachedMessageComposer> ().Compose (session, false, userId, message, 30);
			router.GetComposer<OnGuideSessionAttachedMessageComposer> ().Compose (guide, true, userId, message, 15);

			guide.GuideOtherUser = session.UserData;
			session.UserData.GuideOtherUser = guide;
		}
	}
}


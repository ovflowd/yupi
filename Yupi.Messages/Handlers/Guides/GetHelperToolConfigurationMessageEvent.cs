using System;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.Guides
{
	public class GetHelperToolConfigurationMessageEvent : AbstractHandler
	{
		private GuideManager GuideManager;

		public GetHelperToolConfigurationMessageEvent ()
		{
			GuideManager = DependencyFactory.Resolve<GuideManager> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			bool onDuty = message.GetBool();

			// TODO Use these values
			message.GetBool();
			message.GetBool();
			message.GetBool();

			if (onDuty)
				GuideManager.Add(session);
			else
				GuideManager.Remove(session);

			router.GetComposer<HelperToolConfigurationMessageComposer> ().Compose (session, onDuty,
				GuideManager.Guides.Count, GuideManager.Helpers.Count, GuideManager.Guardians.Count);
		}
	}
}


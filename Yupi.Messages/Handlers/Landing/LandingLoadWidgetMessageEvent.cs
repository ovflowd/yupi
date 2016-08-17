using System;

namespace Yupi.Messages.Landing
{
	public class LandingLoadWidgetMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string text = request.GetString();

			router.GetComposer<LandingWidgetMessageComposer> ().Compose (session, text);
		}
	}
}


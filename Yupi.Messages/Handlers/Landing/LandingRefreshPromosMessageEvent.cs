using System;


namespace Yupi.Messages.Landing
{
	public class LandingRefreshPromosMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			HotelLandingManager hotelView = Yupi.GetGame().GetHotelView();

			router.GetComposer<LandingPromosMessageComposer> ().Compose (session, hotelView.HotelViewPromosIndexers);
		}
	}
}


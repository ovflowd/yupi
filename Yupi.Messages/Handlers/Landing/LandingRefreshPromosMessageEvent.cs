using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Landing
{
    public class LandingRefreshPromosMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            /*HotelLandingManager hotelView = Yupi.GetGame().GetHotelView();

            router.GetComposer<LandingPromosMessageComposer> ().Compose (session, hotelView.HotelViewPromosIndexers);
            */
            throw new NotImplementedException();
        }
    }
}
namespace Yupi.Messages.Landing
{
    using System;

    public class LandingRefreshPromosMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*HotelLandingManager hotelView = Yupi.GetGame().GetHotelView();

            router.GetComposer<LandingPromosMessageComposer> ().Compose (session, hotelView.HotelViewPromosIndexers);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
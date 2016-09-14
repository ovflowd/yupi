using System;
using Yupi.Controller;
using Yupi.Model;


namespace Yupi.Messages.Navigator
{
    public class GoToHotelViewMessageEvent : AbstractHandler
    {
        private RoomManager RoomManager;

        public GoToHotelViewMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (session.Room == null)
                return;

            RoomManager.RemoveUser(session);

            // TODO Implement
            /*
            HotelLandingManager hotelView = Yupi.GetGame().GetHotelView();

            if (hotelView.FurniRewardName != null)
            {
                router.GetComposer<LandingRewardMessageComposer> ().Compose (session, hotelView);
            }*/
        }
    }
}
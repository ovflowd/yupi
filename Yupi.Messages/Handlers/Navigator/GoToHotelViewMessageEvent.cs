using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    public class GoToHotelViewMessageEvent : AbstractHandler
    {
        private readonly RoomManager RoomManager;

        public GoToHotelViewMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
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
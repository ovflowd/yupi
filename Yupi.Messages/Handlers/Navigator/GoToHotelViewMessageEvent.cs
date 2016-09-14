namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;

    public class GoToHotelViewMessageEvent : AbstractHandler
    {
        #region Fields

        private RoomManager RoomManager;

        #endregion Fields

        #region Constructors

        public GoToHotelViewMessageEvent()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

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

        #endregion Methods
    }
}
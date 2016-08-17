using System;



namespace Yupi.Messages.Navigator
{
	public class GoToHotelViewMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (session.Room == null)
				return;

			/*
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			room?.GetRoomUserManager().RemoveUserFromRoom(session, true, false);

			HotelLandingManager hotelView = Yupi.GetGame().GetHotelView();

			if (hotelView.FurniRewardName != null)
			{
				router.GetComposer<LandingRewardMessageComposer> ().Compose (session, hotelView);
			}

			session.CurrentRoomUserId = -1;
			*/
			throw new NotImplementedException ();
		}
	}
}


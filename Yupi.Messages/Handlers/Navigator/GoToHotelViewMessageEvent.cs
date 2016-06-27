using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Browser;

namespace Yupi.Messages.Navigator
{
	public class GoToHotelViewMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			if (!session.GetHabbo().InRoom)
				return;

			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			room?.GetRoomUserManager().RemoveUserFromRoom(session, true, false);

			HotelLandingManager hotelView = Yupi.GetGame().GetHotelView();

			if (hotelView.FurniRewardName != null)
			{
				router.GetComposer<LandingRewardMessageComposer> ().Compose (session, hotelView);
			}

			session.CurrentRoomUserId = -1;
		}
	}
}


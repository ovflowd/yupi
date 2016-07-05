using System;


namespace Yupi.Messages.Items
{
	public class TradeStartMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

			if (room == null)
				return;

			if (room.RoomData.TradeState == 0)
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled"));
				return;
			}

			if (room.RoomData.TradeState == 1 && !room.CheckRights(session))
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled_no_rights"));
				return;
			}

			if (Yupi.GetDbConfig().DbData["trading_enabled"] != "1")
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled_hotel"));
				return;
			}

			if (!session.GetHabbo().CheckTrading())
				session.SendNotif(Yupi.GetLanguage().GetVar("room_trade_disabled_mod"));

			RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
			RoomUser roomUserByVirtualId = room.GetRoomUserManager().GetRoomUserByVirtualId(request.GetInteger());

			if (roomUserByVirtualId?.GetClient() == null || roomUserByVirtualId.GetClient().GetHabbo() == null)
				return;

			room.TryStartTrade(roomUserByHabbo, roomUserByVirtualId);
		}
	}
}


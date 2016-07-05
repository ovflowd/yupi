using System;



namespace Yupi.Messages.Items
{
	public class TradeAddItemOfferMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CanTradeInRoom)
				return;

			Trade userTrade = room.GetUserTrade(session.GetHabbo().Id);
			UserItem item = session.GetHabbo().GetInventoryComponent().GetItem(request.GetUInt32());

			if (userTrade == null || item == null)
				return;

			userTrade.OfferItem(session.GetHabbo().Id, item);
		}
	}
}


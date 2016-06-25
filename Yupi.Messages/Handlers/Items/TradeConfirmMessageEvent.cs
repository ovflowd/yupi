using System;
using Yupi.Emulator.Game.Rooms.User.Trade;

namespace Yupi.Messages.Items
{
	public class TradeConfirmMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CanTradeInRoom)
				return;

			Trade userTrade = room.GetUserTrade(session.GetHabbo().Id);

			userTrade?.CompleteTrade(session.GetHabbo().Id);
		}
	}
}


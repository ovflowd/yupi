using System;


namespace Yupi.Messages.Items
{
	public class TradeAcceptMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CanTradeInRoom)
				return;

			Trade userTrade = room.GetUserTrade(session.GetHabbo().Id);

			userTrade?.Accept(session.GetHabbo().Id);
		}
	}
}


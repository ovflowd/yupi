using System;

namespace Yupi.Messages.Items
{
	public class TradeCancelMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CanTradeInRoom)
				return;

			room.TryStopTrade(session.GetHabbo().Id);
			*/
			throw new NotImplementedException ();
		}
	}
}


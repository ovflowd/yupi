using System;


namespace Yupi.Messages.Items
{
	public class TriggerDiceCloseMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
			RoomItem item = room?.GetRoomItemHandler().GetItem(request.GetUInt32());

			if (item == null)
				return;

			bool hasRights = room.CheckRights(session);

			item.Interactor.OnTrigger(session, item, -1, hasRights);
			item.OnTrigger(room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id));
		}
	}
}


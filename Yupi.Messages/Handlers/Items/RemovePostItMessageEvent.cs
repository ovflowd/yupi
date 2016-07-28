using System;



namespace Yupi.Messages.Items
{
	public class RemovePostItMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true))
				return;

			RoomItem item = room.GetRoomItemHandler().GetItem(request.GetUInt32());

			if (item == null || item.GetBaseItem().InteractionType != Interaction.PostIt)
				return;

			room.GetRoomItemHandler().RemoveFurniture(session, item.Id);
		}
	}
}


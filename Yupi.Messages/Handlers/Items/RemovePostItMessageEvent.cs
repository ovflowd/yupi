using System;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Enums;

namespace Yupi.Messages.Items
{
	public class RemovePostItMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
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


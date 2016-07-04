using System;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Messages.Items
{
	public class DropHanditemMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (roomUserByHabbo?.CarryItemId > 0 && roomUserByHabbo.CarryTimer > 0)
				roomUserByHabbo.CarryItem(0);
		}
	}
}


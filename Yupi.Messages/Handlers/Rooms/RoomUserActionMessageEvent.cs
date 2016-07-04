using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Messages.User;

namespace Yupi.Messages.Rooms
{
	public class RoomUserActionMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
			RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (roomUserByHabbo == null)
				return;

			roomUserByHabbo.UnIdle();

			// TODO Number meaning?
			int num = request.GetInteger();

			roomUserByHabbo.DanceId = 0;

			router.GetComposer<RoomUserActionMessageComposer> ().Compose (room, roomUserByHabbo.VirtualId, num);

			if (num == 5)
			{
				roomUserByHabbo.IsAsleep = true;
				router.GetComposer<RoomUserIdleMessageComposer> ().Compose (room, roomUserByHabbo.VirtualId, roomUserByHabbo.IsAsleep);
			}
		}
	}
}


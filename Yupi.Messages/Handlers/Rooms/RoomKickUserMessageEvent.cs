using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Messages.Rooms
{
	public class RoomKickUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null)
				return;
			// TODO Use permissions
			if (!room.CheckRights(session) && room.RoomData.WhoCanKick != 2 &&
				session.GetHabbo().Rank < Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"]))
				return;

			uint userId = request.GetUInt32();

			RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(userId);

			if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
				return;

			if (room.CheckRights(roomUserByHabbo.GetClient(), true) ||
				roomUserByHabbo.GetClient().GetHabbo().HasFuse("fuse_mod") ||
				roomUserByHabbo.GetClient().GetHabbo().HasFuse("fuse_no_kick")) // TODO Shouldn't we tell the user about this? (Whisper?)
				return;

			room.GetRoomUserManager().RemoveUserFromRoom(roomUserByHabbo.GetClient(), true, true);
			roomUserByHabbo.GetClient().CurrentRoomUserId = -1;
		}
	}
}


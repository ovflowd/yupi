using System;



namespace Yupi.Messages.Rooms
{
	public class RoomBanUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || (room.RoomData.WhoCanBan == 0 && !room.CheckRights(session, true)) ||
				(room.RoomData.WhoCanBan == 1 && !room.CheckRights(session)))
				return;

			uint userId = request.GetUInt32();

			// TODO unused
			request.GetUInt32();

			string text = request.GetString();

			RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(userId);

			if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
				return;

			if (roomUserByHabbo.GetClient().UserData.Info.HasPermission("fuse_mod") ||
				roomUserByHabbo.GetClient().UserData.Info.HasPermission("fuse_no_kick")) // TODO Tell user about this behaviour (Whisper)
				return;

			long time = 0L;
			// TODO improve ban length parsing
			if (text.ToLower().Contains("hour"))
				time = 3600L;
			else if (text.ToLower().Contains("day"))
				time = 86400L;
			else if (text.ToLower().Contains("perm"))
				time = 788922000L;

			room.AddBan(userId, time);
			room.GetRoomUserManager().RemoveUserFromRoom(roomUserByHabbo.GetClient(), true, true);
			session.CurrentRoomUserId = -1;
			*/
			throw new NotImplementedException ();
		}
	}
}


using System;




using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class GiveRespectMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, ClientMessage message, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

			if (room == null || session.UserData.DailyRespectPoints <= 0)
				return;

			RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(message.GetUInt32());

			if (roomUserByHabbo == null || roomUserByHabbo.GetClient().GetHabbo().Id == session.UserData.Id ||
				roomUserByHabbo.IsBot)
				return;

			Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(session, "ACH_RespectGiven", 1, true);
			Yupi.GetGame()
				.GetAchievementManager()
				.ProgressUserAchievement(roomUserByHabbo.GetClient(), "ACH_RespectEarned", 1, true);

			Session.GetHabbo().DailyRespectPoints--;
			roomUserByHabbo.GetClient().GetHabbo().Respect++;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("UPDATE users_stats SET respect = respect + 1 WHERE id = @id");

				// TODO Could this user be offline by now??? (RACE CONDITION !!!)
				queryReactor.AddParameter ("id", roomUserByHabbo.GetClient ().GetHabbo ().Id);
				queryReactor.RunQuery ();
				// TODO These two queries should be run in a transaction!
				queryReactor.SetQuery ("UPDATE users_stats SET daily_respect_points = daily_respect_points - 1 WHERE id = @id");
				queryReactor.AddParameter ("id", session.UserData.Id);
				queryReactor.RunQuery ();
			}

			router.GetComposer<GiveRespectsMessageComposer> ().Compose (room, roomUserByHabbo.GetClient ().GetHabbo ().Id, roomUserByHabbo.GetClient ().GetHabbo ().Respect);
			router.GetComposer<RoomUserActionMessageComposer> ().Compose (room, room.GetRoomUserManager().GetRoomUserByHabbo(session.UserData.GetHabbo().UserName).VirtualId);
		}
	}
}


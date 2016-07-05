using System;



namespace Yupi.Messages.Rooms
{
	public class RateRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || session.GetHabbo().RatedRooms.Contains(room.RoomId) || room.CheckRights(session, true))
				return;


			switch (request.GetInteger())
			{
			case -1:
				room.RoomData.Score -= 2; // TODO Why diff 2?
				break;

			case 1:
				room.RoomData.Score += 2;
				break;

			default:
				return;
			}

			Yupi.GetGame().GetRoomManager().QueueVoteAdd(room.RoomData);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("UPDATE rooms_data SET score = @score WHERE id = @id");
				queryReactor.AddParameter ("score", room.RoomData.Score);
				queryReactor.AddParameter ("id", room.RoomId);
				queryReactor.RunQuery ();
			}
			session.GetHabbo().RatedRooms.Add(room.RoomId);

			// TODO Someone with rights can't vote, right?!
			router.GetComposer<RoomRatingMessageComposer> ().Compose (session, room.RoomData.Score, room.CheckRights (session, true));
		}
	}
}


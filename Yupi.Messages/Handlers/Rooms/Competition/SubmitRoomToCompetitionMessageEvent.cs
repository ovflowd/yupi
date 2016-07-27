using System;




using Yupi.Messages.Rooms;

namespace Yupi.Messages.Competition
{
	public class SubmitRoomToCompetitionMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			// TODO Unused
			request.GetString();

			int code = request.GetInteger();

			Room room = session.GetHabbo().CurrentRoom;
			RoomData roomData = room?.RoomData;

			if (roomData == null)
				return;

			RoomCompetition competition = Yupi.GetGame().GetRoomManager().GetCompetitionManager().Competition;

			if (competition == null)
				return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				// TODO Refactor
				if (code == 2)
				{
					if (competition.Entries.ContainsKey(room.RoomId))
						return;

					queryReactor.SetQuery(
						"INSERT INTO rooms_competitions_entries (competition_id, room_id, status) VALUES (@competition_id, @room_id, @status)");

					queryReactor.AddParameter("competition_id", competition.Id);
					queryReactor.AddParameter("room_id", room.RoomId);
					queryReactor.AddParameter("status", 2);
					queryReactor.RunQuery();
					competition.Entries.Add(room.RoomId, roomData);

					SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer();

					roomData.CompetitionStatus = 2;
					router.GetComposer<CompetitionEntrySubmitResultMessageComposer> ().Compose (session, competition, 3, room);

					session.SendMessage(messageBuffer);
				}
				else if (code == 3)
				{
					if (!competition.Entries.ContainsKey(room.RoomId))
						return;

					RoomData entry = competition.Entries[room.RoomId];

					if (entry == null)
						return;

					queryReactor.SetQuery(
						"UPDATE rooms_competitions_entries SET status = @status WHERE competition_id = @competition_id AND room_id = @roomid");

					queryReactor.AddParameter("status", 3);
					queryReactor.AddParameter("competition_id", competition.Id);
					queryReactor.AddParameter("roomid", room.RoomId);
					queryReactor.RunQuery();
					roomData.CompetitionStatus = 3;


					router.GetComposer<CompetitionEntrySubmitResultMessageComposer> ().Compose (session, competition, 0);
				}
			}
		}
	}
}


using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Rooms.Competitions.Models;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Competition
{
	public class VoteForRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			request.GetString(); // TODO unused

			if (session.GetHabbo().DailyCompetitionVotes <= 0)
				return;

			Room room = Session.GetHabbo().CurrentRoom;

			RoomData roomData = room?.RoomData;

			if (roomData == null)
				return;

			RoomCompetition competition = Yupi.GetGame().GetRoomManager().GetCompetitionManager().Competition;

			if (competition == null)
				return;

			if (!competition.Entries.ContainsKey(room.RoomId))
				return;

			RoomData entry = competition.Entries[room.RoomId];

			// TODO Thread safety?
			entry.CompetitionVotes++;
			session.GetHabbo().DailyCompetitionVotes--;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery(
					"UPDATE rooms_competitions_entries SET votes = @votes WHERE competition_id = @competition_id AND room_id = @roomid");

				queryReactor.AddParameter("votes", entry.CompetitionVotes);
				queryReactor.AddParameter("competition_id", competition.Id);
				queryReactor.AddParameter("roomid", room.RoomId);
				queryReactor.RunQuery();

				queryReactor.SetQuery ("UPDATE users_stats SET daily_competition_votes = @daily_votes WHERE id = @id");
				queryReactor.AddParameter ("daily_votes", session.GetHabbo ().DailyCompetitionVotes);
				queryReactor.AddParameter ("id", session.GetHabbo().Id);

				queryReactor.RunQuery ();
			}

			SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer();
			competition.AppendVoteMessage(messageBuffer, session.GetHabbo());

			Session.SendMessage(messageBuffer);
		}
	}
}


using System;
using Yupi.Messages.Rooms;

namespace Yupi.Messages.Competition
{
    public class VoteForRoomMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            request.GetString(); // TODO unused

            /*
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


            router.GetComposer<CompetitionVotingInfoMessageComposer> ().Compose (session, competition, session.GetHabbo().DailyCompetitionVotes);
*/
            throw new NotImplementedException();
        }
    }
}
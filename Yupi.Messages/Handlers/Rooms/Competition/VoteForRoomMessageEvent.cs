// ---------------------------------------------------------------------------------
// <copyright file="VoteForRoomMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Messages.Competition
{
    using System;

    using Yupi.Messages.Rooms;

    public class VoteForRoomMessageEvent : AbstractHandler
    {
        #region Methods

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

        #endregion Methods
    }
}
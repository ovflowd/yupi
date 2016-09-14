// ---------------------------------------------------------------------------------
// <copyright file="SubmitRoomToCompetitionMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class SubmitRoomToCompetitionMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            // TODO Unused
            request.GetString();

            int code = request.GetInteger();

            /*
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

                    session.Send(messageBuffer);
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
            }*/
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
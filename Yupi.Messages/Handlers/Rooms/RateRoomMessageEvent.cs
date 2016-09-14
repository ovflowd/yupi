// ---------------------------------------------------------------------------------
// <copyright file="RateRoomMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Rooms
{
    using System;

    public class RateRoomMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
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
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
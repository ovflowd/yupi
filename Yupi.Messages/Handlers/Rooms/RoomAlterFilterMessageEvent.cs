// ---------------------------------------------------------------------------------
// <copyright file="RoomAlterFilterMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class RoomAlterFilterMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint roomId = request.GetUInt32();
            bool shouldAdd = request.GetBool();
            string word = request.GetString();

            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true))
                return;

            if (!shouldAdd) {
                if (!room.WordFilter.Contains (word))
                    return;

                room.WordFilter.Remove (word);

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                    queryReactor.SetQuery ("DELETE FROM rooms_wordfilter WHERE room_id = @id AND word = @word");
                    queryReactor.AddParameter ("id", roomId);
                    queryReactor.AddParameter ("word", word);
                    queryReactor.RunQuery ();
                }
            } else {

                if (room.WordFilter.Contains (word))
                    return;

                if (word.Contains ("+")) {
                    session.SendNotif (Yupi.GetLanguage ().GetVar ("character_error_plus"));
                    return;
                }

                room.WordFilter.Add (word);

                using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                    queryreactor2.SetQuery ("INSERT INTO rooms_wordfilter (room_id, word) VALUES (@id, @word);");
                    queryreactor2.AddParameter ("id", roomId);
                    queryreactor2.AddParameter ("word", word);
                    queryreactor2.RunQuery ();
                }
            }*/
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
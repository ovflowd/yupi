// ---------------------------------------------------------------------------------
// <copyright file="RoomRemoveAllRightsMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Data;

    public class RoomRemoveAllRightsMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true))
                return;

            DataTable table;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT user_id FROM rooms_rights WHERE room_id=@room");
                queryReactor.AddParameter("room", room.RoomId);
                table = queryReactor.GetTable();
            }

            RemoveRightsMessageComposer removeRightsComposer = router.GetComposer<RemoveRightsMessageComposer> ();

            foreach (DataRow dataRow in table.Rows)
            {
                uint userId = (uint) dataRow[0];

                RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(userId);

                removeRightsComposer.Compose (session, room.RoomId, userId);

                if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
                    continue;

                router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (roomUserByHabbo.GetClient (), 0);
                roomUserByHabbo.RemoveStatus("flatctrl 1");
                roomUserByHabbo.UpdateNeeded = true;
            }

            using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                // TODO Replace interpreted strings (bad practice for SQL!!!)
                queryreactor2.RunFastQuery ($"DELETE FROM rooms_rights WHERE room_id = {room.RoomId}");
            }

            room.UsersWithRights.Clear();

            UsersWithRights();
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
// ---------------------------------------------------------------------------------
// <copyright file="RoomDeleteMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class RoomDeleteMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint roomId = request.GetUInt32();

            /*
            if (session.GetHabbo().UsersRooms == null)
                return;

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

            if (room == null)
                return;

            if (room.RoomData.Owner != session.GetHabbo().Name && session.GetHabbo().Rank <= 6u)
                return;

            if (session.GetHabbo().GetInventoryComponent() != null)
                session.GetHabbo().GetInventoryComponent().AddItemArray(room.GetRoomItemHandler().RemoveAllFurniture(session));

            RoomData roomData = room.RoomData;

            Yupi.GetGame().GetRoomManager().UnloadRoom(room, "Delete room");
            Yupi.GetGame().GetRoomManager().QueueVoteRemove(roomData);

            if (roomData == null)
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.RunFastQuery($"DELETE FROM rooms_data WHERE id = {roomId}");
                queryReactor.RunFastQuery($"DELETE FROM users_favorites WHERE room_id = {roomId}");
                queryReactor.RunFastQuery($"DELETE FROM items_rooms WHERE room_id = {roomId}");
                queryReactor.RunFastQuery($"DELETE FROM rooms_rights WHERE room_id = {roomId}");
                queryReactor.RunFastQuery($"UPDATE users SET home_room = '0' WHERE home_room = {roomId}");
            }

            // TODO Remove those damn $ strings...
            if (session.GetHabbo().Rank > 5u && session.GetHabbo().Name != roomData.Owner)
                Yupi.GetGame().GetModerationTool().LogStaffEntry(session.GetHabbo().Name, roomData.Name, "Room deletion", $"Deleted room ID {roomData.Id}");

            RoomData roomData2 = (from p in session.GetHabbo().UsersRooms where p.Id == roomId select p).SingleOrDefault();

            if (roomData2 != null)
                session.GetHabbo().UsersRooms.Remove(roomData2);
                */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
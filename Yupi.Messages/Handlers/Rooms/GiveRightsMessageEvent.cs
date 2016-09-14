// ---------------------------------------------------------------------------------
// <copyright file="GiveRightsMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class GiveRightsMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            uint userId = request.GetUInt32();
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null)
                return;

            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(userId);

            if (roomUserByHabbo == null || roomUserByHabbo.IsBot || !room.CheckRights(session, true)) {
                return;
            }

            if (room.UsersWithRights.Contains(userId))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("no_room_rights_error"));
                return;
            }

            room.UsersWithRights.Add(userId);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                queryReactor.SetQuery ("INSERT INTO rooms_rights (room_id,user_id) VALUES (@room, @user)");
                queryReactor.AddParameter ("room", room.RoomId);
                queryReactor.AddParameter ("user", userId);
                queryReactor.RunQuery ();
            }

                router.GetComposer<GiveRoomRightsMessageComposer> ().Compose (session, room.RoomId, roomUserByHabbo.GetClient ().GetHabbo ());
                roomUserByHabbo.UpdateNeeded = true;

                roomUserByHabbo.AddStatus("flatctrl 1", string.Empty);

            router.GetComposer<RoomRightsLevelMessageComposer> ().Compose (roomUserByHabbo.GetClient(), 1);
            UsersWithRights();
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
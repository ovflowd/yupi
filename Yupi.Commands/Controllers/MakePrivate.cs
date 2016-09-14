// ---------------------------------------------------------------------------------
// <copyright file="MakePrivate.cs" company="https://github.com/sant0ro/Yupi">
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
/* copyright */
using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;



namespace Yupi.Emulator.Game.Commands.Controllers
{
     public sealed class MakePrivate : Command
    {
        public MakePrivate()
        {
            MinRank = 7;
            Description = "Haz una sala privada.";
            Usage = ":makeprivate";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(
                    $"UPDATE rooms_data SET roomtype = 'private' WHERE id = {room.RoomId}");

            uint roomId = session.GetHabbo().CurrentRoom.RoomId;
            List<RoomUser> users = new List<RoomUser>(session.GetHabbo().CurrentRoom.GetRoomUserManager().UserList.Values);

            Yupi.GetGame().GetRoomManager().UnloadRoom(session.GetHabbo().CurrentRoom, "Unload command");
            Yupi.GetGame().GetRoomManager().LoadRoom(roomId);

            foreach (RoomUser user in users.Where(user => user != null && user.GetClient() != null))
				user.GetClient().CurrentRoom.Router.GetComposer<RoomForwardMessageComposer> ().Compose (user.GetClient(), roomId);

            return true;
        }
    }
}
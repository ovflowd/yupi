// ---------------------------------------------------------------------------------
// <copyright file="Unload.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms.User;



namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Unload. This class cannot be inherited.
    /// </summary>
     public sealed class Unload : Command
    {
        /// <summary>
        ///     The _re enter
        /// </summary>
        private readonly bool _reEnter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Unload" /> class.
        /// </summary>
        /// <param name="reEnter">if set to <c>true</c> [re enter].</param>
        public Unload(bool reEnter = false)
        {
            MinRank = -1;
            Description = "Unloads the current room!";
            Usage = reEnter ? ":reload" : ":unload";
            MinParams = 0;
            _reEnter = reEnter;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            uint roomId = session.GetHabbo().CurrentRoom.RoomId;
            List<RoomUser> users = new List<RoomUser>(session.GetHabbo().CurrentRoom.GetRoomUserManager().UserList.Values);

            Yupi.GetGame().GetRoomManager().UnloadRoom(session.GetHabbo().CurrentRoom, "Unload command");

            if (!_reEnter)
                return true;

            Yupi.GetGame().GetRoomManager().LoadRoom(roomId);

            SimpleServerMessageBuffer roomFwd = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RoomForwardMessageComposer"));
            roomFwd.AppendInteger(roomId);

            byte[] data = roomFwd.GetReversedBytes();

            foreach (RoomUser user in users.Where(user => user != null && user.GetClient() != null))
                user.GetClient().SendMessage(data);
            return true;
        }
    }
}
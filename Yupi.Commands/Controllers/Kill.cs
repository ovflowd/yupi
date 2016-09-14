// ---------------------------------------------------------------------------------
// <copyright file="Kill.cs" company="https://github.com/sant0ro/Yupi">
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
using System;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Pathfinding;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Kill. This class cannot be inherited.
    /// </summary>
     public sealed class Kill : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Kill" /> class.
        /// </summary>
        public Kill()
        {
            MinRank = -3;
            Description = "Kill someone.";
            Usage = ":kill";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
            if (room == null) return true;

            RoomUser user2 = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().LastSelectedUser);
            if (user2 == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }

            RoomUser user =
                room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().UserName);
            if (PathFinder.GetDistance(user.X, user.Y, user2.X, user2.Y) > 1)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("kil_command_error_1"));

                return true;
            }
            if (user2.IsLyingDown || user2.IsSitting)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("kil_command_error_2"));
                return true;
            }
            if (
                !string.Equals(user2.GetUserName(), session.GetHabbo().UserName,
                    StringComparison.CurrentCultureIgnoreCase))
            {
                user2.Statusses.Add("lay", "0.55");
                user2.IsLyingDown = true;
                user2.UpdateNeeded = true;
                user.Chat(user.GetClient(), Yupi.GetLanguage().GetVar("command.kill.user"), true, 0, 3);
                user2.Chat(user2.GetClient(), Yupi.GetLanguage().GetVar("command.kill.userdeath"), true, 0,
                    3);
                return true;
            }
            user.Chat(session, "I am sad", false, 0);
            return true;
        }
    }
}
// ---------------------------------------------------------------------------------
// <copyright file="RoomBadge.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RoomBadge. This class cannot be inherited.
    /// </summary>
     public sealed class RoomBadge : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomBadge" /> class.
        /// </summary>
        public RoomBadge()
        {
            MinRank = 7;
            Description = "Gives just the whole room a badge.";
            Usage = ":roombadge [badgeCode]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            if (pms[0].Length < 2)
                return true;

            Room room = session.GetHabbo().CurrentRoom;

            foreach (RoomUser current in room.GetRoomUserManager().UserList.Values)
                if (!current.IsBot && current.GetClient() != null && current.GetClient().GetHabbo() != null)
                    current.GetClient().GetHabbo().GetBadgeComponent().GiveBadge(pms[0], true, current.GetClient());

            Yupi.GetGame().GetModerationTool().LogStaffEntry(session.GetHabbo().UserName, string.Empty, "Badge", string.Concat("Roombadge in room [", room.RoomId, "] with badge [", pms[0], "]"));

            return true;
        }
    }
}
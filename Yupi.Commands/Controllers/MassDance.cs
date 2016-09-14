// ---------------------------------------------------------------------------------
// <copyright file="MassDance.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;



namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MassDance. This class cannot be inherited.
    /// </summary>
     public sealed class MassDance : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MassDance" /> class.
        /// </summary>
        public MassDance()
        {
            MinRank = 7;
            Description = "Enable dance Id for the whole room.";
            Usage = ":massdance [danceId(0 - 4)]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            ushort danceId;
            ushort.TryParse(pms[0], out danceId);

            if (danceId > 4)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("command_dance_wrong_syntax"));
                return true;
            }
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
            HashSet<RoomUser> roomUsers = room.GetRoomUserManager().GetRoomUsers();

            foreach (RoomUser roomUser in roomUsers)
            {
                SimpleServerMessageBuffer messageBuffer =
                    new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("DanceStatusMessageComposer"));
                messageBuffer.AppendInteger(roomUser.VirtualId);
                messageBuffer.AppendInteger(danceId);
                room.SendMessage(messageBuffer);
                roomUser.DanceId = danceId;
            }
            return true;
        }
    }
}
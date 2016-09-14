// ---------------------------------------------------------------------------------
// <copyright file="RoomMute.cs" company="https://github.com/sant0ro/Yupi">
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

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RoomMute. This class cannot be inherited.
    /// </summary>
     public sealed class RoomMute : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomMute" /> class.
        /// </summary>
        public RoomMute()
        {
            MinRank = 5;
            Description = "Mutes the whole room.";
            Usage = ":roommute [reason]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;
            if (room.RoomMuted)
            {
                session.SendWhisper("Room is already muted.");
                return true;
            }

            session.GetHabbo().CurrentRoom.RoomMuted = true;

            /*
            var message = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("AlertNotificationMessageComposer"));
            message.AppendString(string.Format("The room was muted due to:\r{0}", string.Join(" ", pms)));
            message.AppendString(string.Empty);
            room.SendMessage(message);*/

			room.Router.GetComposer<SuperNotificationMessageComposer>().Compose(room, "Notice", $"Este quarto foi silenciado pelo motivo:\r{string.Join(" ", pms)}", "", "", "", 4); 

            Yupi.GetGame()
                .GetModerationTool().LogStaffEntry(session.GetHabbo().UserName, string.Empty,
                    "Room Mute", "Room muted");
            return true;
        }
    }
}
// ---------------------------------------------------------------------------------
// <copyright file="Kick.cs" company="https://github.com/sant0ro/Yupi">
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
using System.Linq;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Kick. This class cannot be inherited.
    /// </summary>
     public sealed class Kick : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Kick" /> class.
        /// </summary>
        public Kick()
        {
            MinRank = 5;
            Description = "Kick a selected user from room.";
            Usage = ":kick [USERNAME] [MESSAGE]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string userName = pms[0];
            GameClient userSession = Yupi.GetGame().GetClientManager().GetClientByUserName(userName);
            if (userSession == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (session.GetHabbo().Rank <= userSession.GetHabbo().Rank)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("user_is_higher_rank"));
                return true;
            }
            if (userSession.GetHabbo().CurrentRoomId < 1)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("command_kick_user_not_in_room"));
                return true;
            }
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(userSession.GetHabbo().CurrentRoomId);
            if (room == null) return true;

            room.GetRoomUserManager().RemoveUserFromRoom(userSession, true, false);
            userSession.CurrentRoomUserId = -1;
            if (pms.Length > 1)
            {
                userSession.SendNotif(
                    string.Format(Yupi.GetLanguage().GetVar("command_kick_user_mod_default") + "{0}.",
                        string.Join(" ", pms.Skip(1))));
            }
            else userSession.SendNotif(Yupi.GetLanguage().GetVar("command_kick_user_mod_default"));

            return true;
        }
    }
}
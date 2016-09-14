// ---------------------------------------------------------------------------------
// <copyright file="Summon.cs" company="https://github.com/sant0ro/Yupi">
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

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Summon. This class cannot be inherited.
    /// </summary>
     public sealed class Summon : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Summon" /> class.
        /// </summary>
        public Summon()
        {
            MinRank = 7;
            Description = "Summon the selected user to where you are.";
            Usage = ":summon [USERNAME]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string userName = pms[0];
            if (string.Equals(userName, session.GetHabbo().UserName,
                StringComparison.CurrentCultureIgnoreCase))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("summon_yourself"));
                return true;
            }
            GameClient client = Yupi.GetGame().GetClientManager().GetClientByUserName(userName);
            if (client == null)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (session.GetHabbo().CurrentRoom != null &&
                session.GetHabbo().CurrentRoomId != client.GetHabbo().CurrentRoomId)
                client.PrepareRoomForUser(session.GetHabbo().CurrentRoom.RoomId,
                        session.GetHabbo().CurrentRoom.RoomData.PassWord);
            return true;
        }
    }
}
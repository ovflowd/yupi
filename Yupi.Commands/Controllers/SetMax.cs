// ---------------------------------------------------------------------------------
// <copyright file="SetMax.cs" company="https://github.com/sant0ro/Yupi">
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

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class SetMax. This class cannot be inherited.
    /// </summary>
     public sealed class SetMax : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SetMax" /> class.
        /// </summary>
        public SetMax()
        {
            MinRank = -1;
            Description = "Set max users in a room.";
            Usage = ":setmax [Number from 1 to 200. If 10 < num > 100 requires VIP]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            ushort maxUsers;
            if (!ushort.TryParse(pms[0], out maxUsers) || maxUsers == 0 || maxUsers > 200)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("command_setmax_error_number"));
                return true;
            }

            if (maxUsers > 100 && !(session.GetHabbo().Vip || session.GetHabbo().HasFuse("fuse_vip_commands")))
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("command_setmax_error_max"));
                return true;
            }
            if (maxUsers < 10 && !(session.GetHabbo().Vip || session.GetHabbo().HasFuse("fuse_vip_commands")))
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("command_setmax_error_min"));
                return true;
            }

            session.GetHabbo().CurrentRoom.SetMaxUsers(maxUsers);
            return true;
        }
    }
}
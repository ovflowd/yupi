// ---------------------------------------------------------------------------------
// <copyright file="BanUser.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Emulator.Game.Support;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class BanUser. This class cannot be inherited.
    /// </summary>
     public sealed class BanUser : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BanUser" /> class.
        /// </summary>
        public BanUser()
        {
            MinRank = 4;
            Description = "Ban a user!";
            Usage = ":ban [USERNAME] [TIME] [REASON]";
            MinParams = -2;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            {
                GameClient user = Yupi.GetGame().GetClientManager().GetClientByUserName(pms[0]);

                if (user == null)
                {
                    session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                    return true;
                }
                if (user.GetHabbo().Rank >= session.GetHabbo().Rank)
                {
                    session.SendWhisper(Yupi.GetLanguage().GetVar("user_is_higher_rank"));
                    return true;
                }
                try
                {
                    int length = int.Parse(pms[1]);

                    string message = pms.Length < 3 ? string.Empty : string.Join(" ", pms.Skip(2));
                    if (string.IsNullOrWhiteSpace(message))
                        message = Yupi.GetLanguage().GetVar("command_ban_user_no_reason");

                    ModerationTool.BanUser(session, user.GetHabbo().Id, length, message);
                    Yupi.GetGame()
                        .GetModerationTool()
                        .LogStaffEntry(session.GetHabbo().UserName, user.GetHabbo().UserName, "Ban",
                            $"USER:{pms[0]} TIME:{pms[1]} REASON:{pms[2]}");
                }
                catch
                {
                    // error handle
                }

                return true;
            }
        }
    }
}
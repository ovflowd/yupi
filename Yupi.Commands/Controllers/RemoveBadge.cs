// ---------------------------------------------------------------------------------
// <copyright file="RemoveBadge.cs" company="https://github.com/sant0ro/Yupi">
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
    ///     Class RemoveBadge. This class cannot be inherited.
    /// </summary>
     public sealed class RemoveBadge : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RemoveBadge" /> class.
        /// </summary>
        public RemoveBadge()
        {
            MinRank = 7;
            Description = "Remove the badge from user.";
            Usage = ":removebadge [USERNAME] [badgeId]";
            MinParams = 2;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            GameClient client = Yupi.GetGame().GetClientManager().GetClientByUserName(pms[0]);
            if (client == null)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (!client.GetHabbo().GetBadgeComponent().HasBadge(pms[1]))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("command_badge_remove_error"));
                return true;
            }
            client.GetHabbo().GetBadgeComponent().RemoveBadge(pms[1], client);
            session.SendNotif(Yupi.GetLanguage().GetVar("command_badge_remove_done"));
            Yupi.GetGame()
                .GetModerationTool()
                .LogStaffEntry(session.GetHabbo().UserName, client.GetHabbo().UserName,
                    "Badge Taken", $"Badge taken from user [{pms[1]}]");
            return true;
        }
    }
}
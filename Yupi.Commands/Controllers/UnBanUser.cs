// ---------------------------------------------------------------------------------
// <copyright file="UnBanUser.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Users;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class UnBanUser. This class cannot be inherited.
    /// </summary>
     public sealed class UnBanUser : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnBanUser" /> class.
        /// </summary>
        public UnBanUser()
        {
            MinRank = 4;
            Description = "Unban a user!";
            Usage = ":unban [USERNAME]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Habbo user = Yupi.GetHabboForName(pms[0]);

            if (user == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (user.Rank >= session.GetHabbo().Rank)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_is_higher_rank"));
                return true;
            }
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery("DELETE FROM users_bans WHERE value = @name");
                adapter.AddParameter("name", user.UserName);
                adapter.RunQuery();
                Yupi.GetGame()
                    .GetModerationTool()
                    .LogStaffEntry(session.GetHabbo().UserName, user.UserName, "Unban",
                        $"User has been Unbanned [{pms[0]}]");
                return true;
            }
        }
    }
}
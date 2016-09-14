// ---------------------------------------------------------------------------------
// <copyright file="BanUserIp.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class BanUser. This class cannot be inherited.
    /// </summary>
     public sealed class BanUserIp : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BanUser" /> class.
        /// </summary>
        public BanUserIp()
        {
            MinRank = 4;
            Description = "Ban a user by IP!";
            Usage = ":ipban [USERNAME] [REASON]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
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
                Yupi.GetGame()
                    .GetBanManager()
                    .BanUser(user, session.GetHabbo().UserName, 788922000.0, string.Join(" ", pms.Skip(2)),
                        true, false);
            }
            catch
            {
                YupiLogManager.LogException($"An error occurred when {session.GetHabbo().UserName} tried to ban {user.GetHabbo().UserName}", "Failed Banning User.", "Yupi.User");
            }

            return true;
        }
    }
}
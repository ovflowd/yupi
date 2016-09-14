// ---------------------------------------------------------------------------------
// <copyright file="CommandList.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Emulator.Core.Settings;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class CommandList. This class cannot be inherited.
    /// </summary>
     public sealed class CommandList : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandList" /> class.
        /// </summary>
        public CommandList()
        {
            MinRank = 1;
            Description = "Shows all commands.";
            Usage = ":commands";
            MinParams = -2;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            if (ServerExtraSettings.NewPageCommands)
            {
				session.Router.GetComposer<SuperNotificationMessageComposer>().Compose(session, "${generic.notice}", "Now, the commands page opens in a different way", "event:habbopages/chat/newway", "${mod.alert.link}", "game_promo_small", 4);
                return true;
            }

            string commandList;
            if (pms.Length == 0)
            {
                commandList =
                    CommandsManager.CommandsDictionary.Where(
                        command => CommandsManager.CanUse(command.Value.MinRank, session))
                        .Aggregate(string.Empty,
                            (current, command) =>
                                current + command.Value.Usage + " - " + command.Value.Description + "\n");
            }
            else
            {
                if (pms[0].Length == 1)
                {
                    commandList =
                        CommandsManager.CommandsDictionary.Where(
                            command =>
                                command.Key.StartsWith(pms[0]) && CommandsManager.CanUse(command.Value.MinRank, session))
                            .Aggregate(string.Empty,
                                (current, command) =>
                                    current + command.Value.Usage + " - " + command.Value.Description + "\n");
                }
                else
                {
                    commandList =
                        CommandsManager.CommandsDictionary.Where(
                            command =>
                                command.Key.Contains(pms[0]) && CommandsManager.CanUse(command.Value.MinRank, session))
                            .Aggregate(string.Empty,
                                (current, command) =>
                                    current + command.Value.Usage + " - " + command.Value.Description + "\n");
                }
            }
            session.SendNotifWithScroll(commandList);

            return true;
        }
    }
}
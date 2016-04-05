using System.Linq;
using Yupi.Emulator.Core.Settings;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages.Enums;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class CommandList. This class cannot be inherited.
    /// </summary>
     sealed class CommandList : Command
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
                session.SendMessage(StaticMessage.NewWayToOpenCommandsList);

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
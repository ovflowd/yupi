using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.RoomBots;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshBotCommands. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshBotCommands : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SetVideos" /> class.
        /// </summary>
        public RefreshBotCommands()
        {
            MinRank = 7;
            Description = "Refresh Bot Commands";
            Usage = ":refresh_bot_commands";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Yupi.GetGame().GetBotManager().Initialize();
            return true;
        }
    }
}
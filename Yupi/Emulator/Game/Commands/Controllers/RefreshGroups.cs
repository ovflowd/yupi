using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshGroups. This class cannot be inherited.
    /// </summary>
     sealed class RefreshGroups : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshGroups" /> class.
        /// </summary>
        public RefreshGroups()
        {
            MinRank = 9;
            Description = "Refreshes Groups from Database.";
            Usage = ":refresh_groups";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Yupi.GetGame().GetGroupManager().InitGroups();
            session.SendNotif(Yupi.GetLanguage().GetVar("command_refresh_groups"));
            return true;
        }
    }
}
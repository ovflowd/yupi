using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshRanks. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshRanks : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshRanks" /> class.
        /// </summary>
        public RefreshRanks()
        {
            MinRank = 9;
            Description = "Refreshes Ranks from Database.";
            Usage = ":refresh_ranks";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
                Yupi.GetGame().GetRoleManager().LoadRights(adapter);
            CommandsManager.UpdateInfo();
            session.SendNotif(Yupi.GetLanguage().GetVar("command_refresh_ranks"));
            return true;
        }
    }
}
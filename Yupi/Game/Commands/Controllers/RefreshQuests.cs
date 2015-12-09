using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshQuests. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshQuests : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshQuests" /> class.
        /// </summary>
        public RefreshQuests()
        {
            MinRank = 9;
            Description = "Refreshes navigator from Database.";
            Usage = ":refresh_quests";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Yupi.GetGame().GetQuestManager().Initialize(Yupi.GetDatabaseManager().GetQueryReactor());
            session.SendNotif(Yupi.GetLanguage().GetVar("command_refresh_quests"));
            return true;
        }
    }
}
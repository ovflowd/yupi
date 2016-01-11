using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshPolls. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshPolls : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshPolls" /> class.
        /// </summary>
        public RefreshPolls()
        {
            MinRank = 9;
            Description = "Refreshes Polls from Database.";
            Usage = ":refresh_polls";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
                Yupi.GetGame().GetPollManager().Init(adapter);
            session.SendNotif(Yupi.GetLanguage().GetVar("command_refresh_polls"));
            return true;
        }
    }
}
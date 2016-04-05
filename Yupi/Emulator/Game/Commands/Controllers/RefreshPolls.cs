using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshPolls. This class cannot be inherited.
    /// </summary>
     sealed class RefreshPolls : Command
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
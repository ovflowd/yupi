using Yupi.Core.Settings;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshSettings. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshSettings : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshSettings" /> class.
        /// </summary>
        public RefreshSettings()
        {
            MinRank = 9;
            Description = "Refreshes Settings from Database.";
            Usage = ":refresh_settings";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
                Yupi.ConfigData = new ServerDatabaseSettings(adapter);
            session.SendNotif(Yupi.GetLanguage().GetVar("command_refresh_settings"));
            return true;
        }
    }
}
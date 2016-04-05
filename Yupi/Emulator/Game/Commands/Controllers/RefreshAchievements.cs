using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshAchievements. This class cannot be inherited.
    /// </summary>
     sealed class RefreshAchievements : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshAchievements" /> class.
        /// </summary>
        public RefreshAchievements()
        {
            MinRank = 9;
            Description = "Refreshes Achievements from Database.";
            Usage = ":refresh_achievements";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Yupi.GetGame().GetAchievementManager().LoadAchievements(Yupi.GetDatabaseManager().GetQueryReactor());
            session.SendNotif(Yupi.GetLanguage().GetVar("command_refresh_achievements"));
            return true;
        }
    }
}
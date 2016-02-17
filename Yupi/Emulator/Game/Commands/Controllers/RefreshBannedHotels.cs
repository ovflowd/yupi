using Yupi.Core.Security;
using Yupi.Core.Security.BlackWords;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshBannedHotels. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshBannedHotels : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshBannedHotels" /> class.
        /// </summary>
        public RefreshBannedHotels()
        {
            MinRank = 9;
            Description = "Refreshes BlackWords filter from Database.";
            Usage = ":refresh_banned_hotels";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            UserChatInputFilter.Reload();
            BlackWordsManager.Reload();

            session.SendNotif(Yupi.GetLanguage().GetVar("command_refresh_banned_hotels"));
            return true;
        }
    }
}
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class GiveBadge. This class cannot be inherited.
    /// </summary>
    internal sealed class GiveBadge : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GiveBadge" /> class.
        /// </summary>
        public GiveBadge()
        {
            MinRank = 5;
            Description = "Give user a badge.";
            Usage = ":givebadge [USERNAME] [badgeCode]";
            MinParams = 2;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            GameClient client = Yupi.GetGame().GetClientManager().GetClientByUserName(pms[0]);
            if (client == null)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            client.GetHabbo().GetBadgeComponent().GiveBadge(pms[1], true, client);
            session.SendNotif(Yupi.GetLanguage().GetVar("command_badge_give_done"));
            Yupi.GetGame()
                .GetModerationTool()
                .LogStaffEntry(session.GetHabbo().UserName, client.GetHabbo().UserName,
                    "Badge", $"Badge given to user [{pms[1]}]");
            return true;
        }
    }
}
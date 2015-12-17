using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RemoveBadge. This class cannot be inherited.
    /// </summary>
    internal sealed class RemoveBadge : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RemoveBadge" /> class.
        /// </summary>
        public RemoveBadge()
        {
            MinRank = 7;
            Description = "Remove the badge from user.";
            Usage = ":removebadge [USERNAME] [badgeId]";
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
            if (!client.GetHabbo().GetBadgeComponent().HasBadge(pms[1]))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("command_badge_remove_error"));
                return true;
            }
            client.GetHabbo().GetBadgeComponent().RemoveBadge(pms[1], client);
            session.SendNotif(Yupi.GetLanguage().GetVar("command_badge_remove_done"));
            Yupi.GetGame()
                .GetModerationTool()
                .LogStaffEntry(session.GetHabbo().UserName, client.GetHabbo().UserName,
                    "Badge Taken", $"Badge taken from user [{pms[1]}]");
            return true;
        }
    }
}
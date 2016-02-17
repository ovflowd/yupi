using System.Linq;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class SuperBan. This class cannot be inherited.
    /// </summary>
    internal sealed class SuperBan : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SuperBan" /> class.
        /// </summary>
        public SuperBan()
        {
            MinRank = 5;
            Description = "Super ban a user!";
            Usage = ":superban [USERNAME] [REASON]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            GameClient client = Yupi.GetGame().GetClientManager().GetClientByUserName(pms[0]);
            if (client == null)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }

            if (client.GetHabbo().Rank >= session.GetHabbo().Rank)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("user_is_higher_rank"));
                return true;
            }
            Yupi.GetGame()
                .GetModerationTool()
                .LogStaffEntry(session.GetHabbo().UserName, client.GetHabbo().UserName, "Ban",
                    "User has received a Super ban.");
            Yupi.GetGame()
                .GetBanManager()
                .BanUser(client, session.GetHabbo().UserName, 788922000.0, string.Join(" ", pms.Skip(1)),
                    false, false);
            return true;
        }
    }
}
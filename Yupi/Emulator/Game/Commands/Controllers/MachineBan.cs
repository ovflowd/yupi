using System.Linq;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class SuperBan. This class cannot be inherited.
    /// </summary>
     sealed class MachineBan : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MachineBan" /> class.
        /// </summary>
        public MachineBan()
        {
            MinRank = 8;
            Description = "Machine ban a user!";
            Usage = ":machineban [USERNAME] [REASON]";
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

            Yupi.GetGame().GetModerationTool().LogStaffEntry(session.GetHabbo().UserName, client.GetHabbo().UserName, "Ban", "User has received a Machine ban.");

            Yupi.GetGame().GetBanManager().BanUser(client, session.GetHabbo().UserName, 788922000.0, string.Join(" ", pms.Skip(1)), true, true);
            return true;
        }
    }
}
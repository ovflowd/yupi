using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Mute. This class cannot be inherited.
    /// </summary>
    internal sealed class Mute : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Mute" /> class.
        /// </summary>
        public Mute()
        {
            MinRank = 4;
            Description = "Mute a selected user.";
            Usage = ":mute [USERNAME]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            GameClient client = Yupi.GetGame().GetClientManager().GetClientByUserName(pms[0]);
            if (client == null || client.GetHabbo() == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (client.GetHabbo().Rank >= 4)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("user_is_higher_rank"));
            }
            Yupi.GetGame()
                .GetModerationTool().LogStaffEntry(session.GetHabbo().UserName, client.GetHabbo().UserName,
                    "Mute", "Muted user");
            client.GetHabbo().Mute();
            return true;
        }
    }
}
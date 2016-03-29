using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class GiveCredits. This class cannot be inherited.
    /// </summary>
    internal sealed class GiveCredits : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GiveCredits" /> class.
        /// </summary>
        public GiveCredits()
        {
            MinRank = 5;
            Description = "Gives user credits.";
            Usage = ":credits [USERNAME] [AMOUNT]";
            MinParams = 2;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            GameClient client = Yupi.GetGame().GetClientManager().GetClientByUserName(pms[0]);

            if (client == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));

                return true;
            }

            uint amount;

            if (!uint.TryParse(pms[1], out amount))
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("enter_numbers"));

                return true;
            }

            client.GetHabbo().Credits += amount;

            client.GetHabbo().UpdateCreditsBalance();

            client.SendNotif(string.Format(Yupi.GetLanguage().GetVar("staff_gives_credits"), session.GetHabbo().UserName, amount));

            Yupi.GetGame().GetModerationTool().LogStaffEntry(session.GetHabbo().UserName, string.Empty, "Credits", string.Concat("Give Credits to User [", pms[0], "] with amount [", pms[1], "]"));

            return true;
        }
    }
}
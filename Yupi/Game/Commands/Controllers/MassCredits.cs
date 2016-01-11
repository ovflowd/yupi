using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MassCredits. This class cannot be inherited.
    /// </summary>
    internal sealed class MassCredits : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MassCredits" /> class.
        /// </summary>
        public MassCredits()
        {
            MinRank = 8;
            Description = "Gives all the users online credits.";
            Usage = ":masscredits [AMOUNT]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            uint amount;

            if (!uint.TryParse(pms[0], out amount))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("enter_numbers"));
                return true;
            }

            foreach (GameClient client in Yupi.GetGame().GetClientManager().Clients.Values)
            {
                if (client?.GetHabbo() == null)
                    continue;

                client.GetHabbo().Credits += amount;
                client.GetHabbo().UpdateCreditsBalance();
                client.SendNotif(Yupi.GetLanguage().GetVar("command_mass_credits_one_give") + amount +
                                 Yupi.GetLanguage().GetVar("command_mass_credits_two_give"));
            }

            return true;
        }
    }
}
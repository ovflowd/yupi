using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MassDiamonds. This class cannot be inherited.
    /// </summary>
    internal sealed class MassDiamonds : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MassDiamonds" /> class.
        /// </summary>
        public MassDiamonds()
        {
            MinRank = 8;
            Description = "Gives all the users online Diamonds.";
            Usage = ":massdiamonds [AMOUNT]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            int amount;
            if (!int.TryParse(pms[0], out amount))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("enter_numbers"));
                return true;
            }
            foreach (var client in Yupi.GetGame().GetClientManager().Clients.Values)
            {
                if (client == null || client.GetHabbo() == null) continue;
                var habbo = client.GetHabbo();
                habbo.Diamonds += amount;
                client.GetHabbo().UpdateSeasonalCurrencyBalance();
                client.SendNotif(Yupi.GetLanguage().GetVar("command_diamonds_one_give") + amount +
                                 (Yupi.GetLanguage().GetVar("command_diamonds_two_give")));
            }
            return true;
        }
    }
}
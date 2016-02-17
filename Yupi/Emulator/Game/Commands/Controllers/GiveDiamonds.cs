using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class GiveDiamonds. This class cannot be inherited.
    /// </summary>
    internal sealed class GiveDiamonds : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GiveDiamonds" /> class.
        /// </summary>
        public GiveDiamonds()
        {
            MinRank = 8;
            Description = "Gives user Diamonds.";
            Usage = ":diamonds [USERNAME] [AMOUNT]";
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

            client.GetHabbo().Diamonds += amount;
            client.GetHabbo().UpdateSeasonalCurrencyBalance();

            client.SendNotif(string.Format(Yupi.GetLanguage().GetVar("staff_gives_diamonds"),
                session.GetHabbo().UserName, amount));

            return true;
        }
    }
}
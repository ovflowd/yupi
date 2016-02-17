using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class GiveDuckets. This class cannot be inherited.
    /// </summary>
    internal sealed class GiveDuckets : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GiveDuckets" /> class.
        /// </summary>
        public GiveDuckets()
        {
            MinRank = 5;
            Description = "Gives user Duckets.";
            Usage = ":duckets [USERNAME] [AMOUNT]";
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

            uint amount;

            if (!uint.TryParse(pms[1], out amount))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("enter_numbers"));
                return true;
            }

            client.GetHabbo().Duckets += amount;
            client.GetHabbo().UpdateActivityPointsBalance();

            client.SendNotif(string.Format(Yupi.GetLanguage().GetVar("staff_gives_duckets"), session.GetHabbo().UserName,
                amount));

            return true;
        }
    }
}
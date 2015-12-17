using System.Linq;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class HotelAlert. This class cannot be inherited.
    /// </summary>
    internal sealed class HotelAlertLink : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HotelAlert" /> class.
        /// </summary>
        public HotelAlertLink()
        {
            MinRank = 5;
            Description = "send a message with a link.";
            Usage = ":hal [url] [message]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string messageUrl = pms[0];
            string messageStr = string.Join(" ", pms.Skip(1));

            Yupi.GetGame()
                .GetClientManager()
                .SendSuperNotif("${catalog.alert.external.link.title}", messageStr, "game_promo_small", session,
                    messageUrl, "${facebook.create_link_in_web}", true, false);
            return true;
        }
    }
}
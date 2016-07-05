using System.Linq;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class HotelAlert. This class cannot be inherited.
    /// </summary>
     public sealed class HotelAlertLink : Command
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

			Router.GetComposer<SuperNotificationMessageComposer> ().Compose (Yupi.GetGame ()
				.GetClientManager (), "${catalog.alert.external.link.title}", messageStr, messageUrl, "${facebook.create_link_in_web}", "game_promo_small");
            return true;
        }
    }
}
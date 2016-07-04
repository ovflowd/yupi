using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class HotelAlert. This class cannot be inherited.
    /// </summary>
     public sealed class RefreshPromos : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshPromos" /> class.
        /// </summary>
        public RefreshPromos()
        {
            MinRank = 5;
            Description = "Refresh promos cache.";
            Usage = ":refresh_promos";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Yupi.GetGame().GetHotelView().RefreshPromoList();
            return true;
        }
    }
}
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class HotelAlert. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshExtraThings : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshExtraThings" /> class.
        /// </summary>
        public RefreshExtraThings()
        {
            MinRank = 5;
            Description = "Refresh Extra things cache.";
            Usage = ":refresh_extrathings";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Yupi.GetGame().GetHallOfFame().RefreshHallOfFame();
            Yupi.GetGame().GetRoomManager().GetCompetitionManager().RefreshCompetitions();
            Yupi.GetGame().GetTargetedOfferManager().LoadOffer();
            return true;
        }
    }
}
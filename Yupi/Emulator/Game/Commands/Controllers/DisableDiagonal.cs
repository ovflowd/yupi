using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class DisableDiagonal. This class cannot be inherited.
    /// </summary>
    internal sealed class DisableDiagonal : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DisableDiagonal" /> class.
        /// </summary>
        public DisableDiagonal()
        {
            MinRank = -2;
            Description = "Disable Diagonal walking.";
            Usage = ":disable_diagonal";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;

            room.GetGameMap().DiagonalEnabled = !room.GetGameMap().DiagonalEnabled;
            session.SendNotif(Yupi.GetLanguage().GetVar("command_disable_diagonal"));

            return true;
        }
    }
}
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Empty. This class cannot be inherited.
    /// </summary>
    internal sealed class Empty : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Empty" /> class.
        /// </summary>
        public Empty()
        {
            MinRank = 1;
            Description = "Empty's your Inventory.";
            Usage = ":empty";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            session.GetHabbo().GetInventoryComponent().ClearItems();
            return true;
        }
    }
}
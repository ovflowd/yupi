using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Sit. This class cannot be inherited.
    /// </summary>
    internal sealed class Offline : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Sit" /> class.
        /// </summary>
        public Offline()
        {
            MinRank = 1;
            Description = "Make you Appear Online/Offline";
            Usage = ":status";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            session.GetHabbo().AppearOffline = !session.GetHabbo().AppearOffline;
            return true;
        }
    }
}
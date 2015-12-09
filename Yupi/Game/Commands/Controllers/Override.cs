using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Override. This class cannot be inherited.
    /// </summary>
    internal sealed class Override : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Override" /> class.
        /// </summary>
        public Override()
        {
            MinRank = 7;
            Description = "Makes you can transpase items.";
            Usage = ":override";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            var currentRoom = session.GetHabbo().CurrentRoom;

            var roomUserByHabbo = currentRoom.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            if (roomUserByHabbo == null) return true;
            roomUserByHabbo.AllowOverride = !roomUserByHabbo.AllowOverride;

            return true;
        }
    }
}
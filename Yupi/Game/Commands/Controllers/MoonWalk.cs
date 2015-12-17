using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MoonWalk. This class cannot be inherited.
    /// </summary>
    internal sealed class MoonWalk : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MoonWalk" /> class.
        /// </summary>
        public MoonWalk()
        {
            MinRank = -3;
            Description = "Enable/disable Moonwalk";
            Usage = ":moonwalk";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;

            if (room == null) return true;

            RoomUser user = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            if (user == null) return true;
            user.IsMoonwalking = !user.IsMoonwalking;

            return true;
        }
    }
}
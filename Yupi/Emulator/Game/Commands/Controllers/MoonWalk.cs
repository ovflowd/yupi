using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MoonWalk. This class cannot be inherited.
    /// </summary>
     sealed class MoonWalk : Command
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
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Override. This class cannot be inherited.
    /// </summary>
     public sealed class Override : Command
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
            Room currentRoom = session.GetHabbo().CurrentRoom;

            RoomUser roomUserByHabbo = currentRoom.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            if (roomUserByHabbo == null) return true;
            roomUserByHabbo.AllowOverride = !roomUserByHabbo.AllowOverride;

            return true;
        }
    }
}
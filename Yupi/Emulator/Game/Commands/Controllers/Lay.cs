using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Lay. This class cannot be inherited.
    /// </summary>
     public sealed class Lay : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Lay" /> class.
        /// </summary>
        public Lay()
        {
            MinRank = 1;
            Description = "Makes you lay.";
            Usage = ":lay";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room currentRoom = session.GetHabbo().CurrentRoom;

            RoomUser roomUserByHabbo = currentRoom.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            if (roomUserByHabbo == null) return true;

            if (roomUserByHabbo.IsSitting || roomUserByHabbo.RidingHorse || roomUserByHabbo.IsWalking ||
                roomUserByHabbo.Statusses.ContainsKey("lay"))
                return true;

            if (roomUserByHabbo.RotBody%2 != 0) roomUserByHabbo.RotBody--;
            roomUserByHabbo.Statusses.Add("lay", "0.55");
            roomUserByHabbo.IsLyingDown = true;
            roomUserByHabbo.UpdateNeeded = true;
            return true;
        }
    }
}
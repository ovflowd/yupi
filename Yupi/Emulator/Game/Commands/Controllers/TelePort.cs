using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class TelePort. This class cannot be inherited.
    /// </summary>
     public sealed class TelePort : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TelePort" /> class.
        /// </summary>
        public TelePort()
        {
            MinRank = 7;
            Description = "Teleport around the room, like a kingorooo.";
            Usage = ":teleport";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;
            RoomUser user = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            if (user == null) return true;
            if (!user.RidingHorse)
            {
                user.TeleportEnabled = !user.TeleportEnabled;
                room.GetGameMap().GenerateMaps();
                return true;
            }
            session.SendWhisper(Yupi.GetLanguage().GetVar("command_error_teleport_enable"));
            return true;
        }
    }
}
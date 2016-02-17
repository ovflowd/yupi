using System.Linq;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MassEnable. This class cannot be inherited.
    /// </summary>
    internal sealed class MassEnable : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MassEnable" /> class.
        /// </summary>
        public MassEnable()
        {
            MinRank = 7;
            Description = "Mass enable.";
            Usage = ":massenable [id]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            ushort effectId;
            if (!ushort.TryParse(pms[0], out effectId)) return true;

            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
            room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            foreach (RoomUser user in room.GetRoomUserManager().GetRoomUsers().Where(user => !user.RidingHorse))
                user.ApplyEffect(effectId);
            return true;
        }
    }
}
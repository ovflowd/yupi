using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class HabNam. This class cannot be inherited.
    /// </summary>
    internal sealed class HabNam : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HabNam" /> class.
        /// </summary>
        public HabNam()
        {
            MinRank = -3;
            Description = "Enable/disable habnam";
            Usage = ":habnam";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;

            RoomUser user = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            session.GetHabbo()
                .GetAvatarEffectsInventoryComponent()
                .ActivateCustomEffect(user != null && user.CurrentEffect != 140 ? 140 : 0);

            return true;
        }
    }
}
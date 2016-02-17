using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Commands.Controllers
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
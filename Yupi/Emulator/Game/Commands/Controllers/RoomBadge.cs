using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RoomBadge. This class cannot be inherited.
    /// </summary>
     sealed class RoomBadge : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomBadge" /> class.
        /// </summary>
        public RoomBadge()
        {
            MinRank = 7;
            Description = "Gives just the whole room a badge.";
            Usage = ":roombadge [badgeCode]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            if (pms[0].Length < 2)
                return true;

            Room room = session.GetHabbo().CurrentRoom;

            foreach (RoomUser current in room.GetRoomUserManager().UserList.Values)
                if (!current.IsBot && current.GetClient() != null && current.GetClient().GetHabbo() != null)
                    current.GetClient().GetHabbo().GetBadgeComponent().GiveBadge(pms[0], true, current.GetClient());

            Yupi.GetGame().GetModerationTool().LogStaffEntry(session.GetHabbo().UserName, string.Empty, "Badge", string.Concat("Roombadge in room [", room.RoomId, "] with badge [", pms[0], "]"));

            return true;
        }
    }
}
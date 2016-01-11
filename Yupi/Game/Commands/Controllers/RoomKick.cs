using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.RoomInvokedItems;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RoomKickUsers. This class cannot be inherited.
    /// </summary>
    internal sealed class RoomKickUsers : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomKickUsers" /> class.
        /// </summary>
        public RoomKickUsers()
        {
            MinRank = 5;
            Description = "Mutes the whole room.";
            Usage = ":roomkick [reason]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;

            string alert = string.Join(" ", pms);
            RoomKick kick = new RoomKick(alert, (int) session.GetHabbo().Rank);
            Yupi.GetGame()
                .GetModerationTool().LogStaffEntry(session.GetHabbo().UserName, string.Empty,
                    "Room kick", "Kicked the whole room");
            room.QueueRoomKick(kick);

            return true;
        }
    }
}
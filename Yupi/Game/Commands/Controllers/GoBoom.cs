using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class GoBoom. This class cannot be inherited.
    /// </summary>
    internal sealed class GoBoom : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GoBoom" /> class.
        /// </summary>
        public GoBoom()
        {
            MinRank = 5;
            Description = "BOOMMMMM";
            Usage = ":goboom";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
            room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            foreach (RoomUser user in room.GetRoomUserManager().GetRoomUsers()) user.ApplyEffect(108);
            return true;
        }
    }
}
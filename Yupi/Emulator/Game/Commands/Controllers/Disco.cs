using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MuteBots. This class cannot be inherited.
    /// </summary>
     sealed class Disco : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Disco" /> class.
        /// </summary>
        public Disco()
        {
            MinRank = -3;
            Description = "#@#CXW..2PC200 X.D.R";
            Usage = ":disco";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;
            if (room == null || !room.CheckRights(session, true)) return true;

            room.DiscoMode = !room.DiscoMode;

            return true;
        }
    }
}
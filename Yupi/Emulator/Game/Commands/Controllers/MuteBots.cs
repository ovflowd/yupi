using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MuteBots. This class cannot be inherited.
    /// </summary>
    internal sealed class MuteBots : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MuteBots" /> class.
        /// </summary>
        public MuteBots()
        {
            MinRank = -2;
            Description = "Mute bots in your own room.";
            Usage = ":mutebots";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;
            room.MutedBots = !room.MutedBots;
            session.SendNotif(Yupi.GetLanguage().GetVar("user_room_mute_bots"));

            return true;
        }
    }
}
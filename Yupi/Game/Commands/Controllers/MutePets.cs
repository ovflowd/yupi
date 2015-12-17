using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MuteBots. This class cannot be inherited.
    /// </summary>
    internal sealed class MutePets : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MutePets" /> class.
        /// </summary>
        public MutePets()
        {
            MinRank = -2;
            Description = "Mute pets in your own room.";
            Usage = ":mutepets";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;
            room.MutedPets = !room.MutedPets;
            session.SendNotif(Yupi.GetLanguage().GetVar("user_room_mute_pets"));

            return true;
        }
    }
}
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class HandItem. This class cannot be inherited.
    /// </summary>
    internal sealed class HandItem : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HandItem" /> class.
        /// </summary>
        public HandItem()
        {
            MinRank = -3;
            Description = "Lets you pick a hand item, e.g. A drink";
            Usage = ":handitem [itemId]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            ushort itemId;

            if (!ushort.TryParse(pms[0], out itemId))
                return true;

            RoomUser user =
                session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().UserName);

            if (user.RidingHorse)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("horse_handitem_error"));
                return true;
            }

            if (user.IsLyingDown)
                return true;

            user.CarryItem(itemId);

            return true;
        }
    }
}
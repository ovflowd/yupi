using System.Linq;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Kick. This class cannot be inherited.
    /// </summary>
    internal sealed class Kick : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Kick" /> class.
        /// </summary>
        public Kick()
        {
            MinRank = 5;
            Description = "Kick a selected user from room.";
            Usage = ":kick [USERNAME] [MESSAGE]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string userName = pms[0];
            GameClient userSession = Yupi.GetGame().GetClientManager().GetClientByUserName(userName);
            if (userSession == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (session.GetHabbo().Rank <= userSession.GetHabbo().Rank)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("user_is_higher_rank"));
                return true;
            }
            if (userSession.GetHabbo().CurrentRoomId < 1)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("command_kick_user_not_in_room"));
                return true;
            }
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(userSession.GetHabbo().CurrentRoomId);
            if (room == null) return true;

            room.GetRoomUserManager().RemoveUserFromRoom(userSession, true, false);
            userSession.CurrentRoomUserId = -1;
            if (pms.Length > 1)
            {
                userSession.SendNotif(
                    string.Format(Yupi.GetLanguage().GetVar("command_kick_user_mod_default") + "{0}.",
                        string.Join(" ", pms.Skip(1))));
            }
            else userSession.SendNotif(Yupi.GetLanguage().GetVar("command_kick_user_mod_default"));

            return true;
        }
    }
}
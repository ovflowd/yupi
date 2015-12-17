using System;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Summon. This class cannot be inherited.
    /// </summary>
    internal sealed class Summon : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Summon" /> class.
        /// </summary>
        public Summon()
        {
            MinRank = 7;
            Description = "Summon the selected user to where you are.";
            Usage = ":summon [USERNAME]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string userName = pms[0];
            if (string.Equals(userName, session.GetHabbo().UserName,
                StringComparison.CurrentCultureIgnoreCase))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("summon_yourself"));
                return true;
            }
            GameClient client = Yupi.GetGame().GetClientManager().GetClientByUserName(userName);
            if (client == null)
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (session.GetHabbo().CurrentRoom != null &&
                session.GetHabbo().CurrentRoomId != client.GetHabbo().CurrentRoomId)
                client.GetMessageHandler()
                    .PrepareRoomForUser(session.GetHabbo().CurrentRoom.RoomId,
                        session.GetHabbo().CurrentRoom.RoomData.PassWord);
            return true;
        }
    }
}
using System.Linq;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    internal sealed class MakeSay : Command
    {
        public MakeSay()
        {
            MinRank = 7;
            Description = "Makes a selected user shout.";
            Usage = ":makesay [USERNAME] [MESSAGE]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            var room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
            if (room == null) return true;

            var user = room.GetRoomUserManager().GetRoomUserByHabbo(pms[0]);
            if (user == null) return true;

            var msg = string.Join(" ", pms.Skip(1));
            if (msg.StartsWith(":")) msg = ' ' + msg;

            if (string.IsNullOrEmpty(msg)) return true;

            user.Chat(user.GetClient(), msg, false, 0);
            return true;
        }
    }
}
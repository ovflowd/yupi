using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
     public sealed class RoomAlert : Command
    {
        public RoomAlert()
        {
            MinRank = 5;
            Description = "Alerts the Room.";
            Usage = ":roomalert [MESSAGE]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string alert = string.Join(" ", pms);

			session.Router.GetComposer<SuperNotificationMessageComposer>().Compose(session, "Notice", alert, "", "", "", 4); 

            return true;
        }
    }
}
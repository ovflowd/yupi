using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
     sealed class RoomAlert : Command
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

            /*foreach (
                var user in
                    session.GetHabbo()
                        .CurrentRoom.GetRoomUserManager()
                        .GetRoomUsers()
                        .Where(user => !user.IsBot && user.GetClientByAddress() != null))
                user.GetClientByAddress().SendNotif(alert);*/

            session.GetHabbo().CurrentRoom.SendMessage(GameClient.GetBytesNotif(alert));

            return true;
        }
    }
}
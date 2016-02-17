using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class SummonAll. This class cannot be inherited.
    /// </summary>
    internal sealed class SummonAll : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SummonAll" /> class.
        /// </summary>
        public SummonAll()
        {
            MinRank = 7;
            Description = "Summon all users online to the room you are in.";
            Usage = ":summonall [reason]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string reason = string.Join(" ", pms);

            byte[] messageBytes =
                GameClient.GetBytesNotif($"You have all been summoned by\r- {session.GetHabbo().UserName}:\r\n{reason}");
            foreach (GameClient client in Yupi.GetGame().GetClientManager().Clients.Values)
            {
                if (session.GetHabbo().CurrentRoom == null ||
                    session.GetHabbo().CurrentRoomId == client.GetHabbo().CurrentRoomId)
                    continue;

                client.GetMessageHandler()
                    .PrepareRoomForUser(session.GetHabbo().CurrentRoom.RoomId,
                        session.GetHabbo().CurrentRoom.RoomData.PassWord);
                client.SendMessage(messageBytes);
            }
            return true;
        }
    }
}
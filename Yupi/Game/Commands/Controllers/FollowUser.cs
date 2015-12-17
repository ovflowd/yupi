using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class FollowUser. This class cannot be inherited.
    /// </summary>
    internal sealed class FollowUser : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FollowUser" /> class.
        /// </summary>
        public FollowUser()
        {
            MinRank = 1;
            Description = "Follow the selected user.";
            Usage = ":follow [USERNAME]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            GameClient client = Yupi.GetGame().GetClientManager().GetClientByUserName(pms[0]);
            if (client == null || client.GetHabbo() == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (client.GetHabbo().CurrentRoom == null ||
                client.GetHabbo().CurrentRoom == session.GetHabbo().CurrentRoom)
                return false;
            ServerMessage roomFwd =
                new ServerMessage(LibraryParser.OutgoingRequest("RoomForwardMessageComposer"));
            roomFwd.AppendInteger(client.GetHabbo().CurrentRoom.RoomId);
            session.SendMessage(roomFwd);

            return true;
        }
    }
}